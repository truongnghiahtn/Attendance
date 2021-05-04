using AppAttendance.Data.EF;
using AppAttendance.Data.Entities;
using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppAttendance.Application.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly AppAttendanceDbContext _context;

        public NotificationService(AppAttendanceDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> Create(CreateNotificationRequest request)
        {
            var query = from n in _context.Notifications
                        where n.Id_BLE == request.Id_BLE && n.Status == true
                        select new { n };
            if(query.Count() >0)
            {
                return new ApiSuccessResult<bool>("thiết bị đang được xử lý");
            }
            var equipment = await _context.Equipment.FindAsync(request.Id_BLE);
            if (equipment == null || equipment.Id_Student != request.Id_User)
            {
                return new ApiErrorResult<bool>("Không tìm thấy thiết bị");
            }
            var notification = new Notification()
            {
                Id_BLE = request.Id_BLE,
                Id_User = request.Id_User,
                Reason = request.Reason,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate = DateTime.UtcNow.AddHours(7),
                Status = true
            };
            equipment.Status = false;

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>("Tạo thành thành công");
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null||notification.Status==true)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Xóa thành công");
        }

        public async Task<ApiResult<PagedResult<NotificationVm>>> GetAllPaging(GetNotificationRequest request)
        {
            var query = from n in _context.Notifications
                        join st in _context.Students on n.Id_User equals st.Id
                        join e in _context.Equipment on n.Id_BLE equals e.Id_BLE into ne
                        from e in ne.DefaultIfEmpty()
                        where n.Status == request.Status
                        select new { n,st,e };
            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x=>x.st.FullName.Contains(request.Keyword));
            }

            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new NotificationVm()
                {
                    Id=x.n.Id,
                    NameUser=x.st.FullName,
                    NameEquipment=x.e.Name !=null?x.e.Name:x.n.Id_BLE,
                    Reason=x.n.Reason,
                    DateCreate=x.n.DateCreate,
                    DateUpdate=x.n.DateUpdate
                    
                }).ToListAsync();

            var pagedResult = new PagedResult<NotificationVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<NotificationVm>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Prosess(ProsessRequest request)
        {
            var notification = await _context.Notifications.FindAsync(request.Id);
            if (notification == null||notification.Status==false)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            var equipment = await _context.Equipment.FindAsync(notification.Id_BLE);
            if (equipment.Status == true)
            {
                return new ApiErrorResult<bool>("Thiết bị không khả dụng ");
            }
            if(request.Status=="agree")
            {
                _context.Equipment.Remove(equipment);
            }
            else if (request.Status == "cancle")
            {
                equipment.Status = true;
            }
            else
            {
                return new ApiErrorResult<bool>("Lỗi xử lý ");
            }
            notification.Status = false;
            notification.DateUpdate = DateTime.UtcNow.AddHours(7);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Cập nhật thành công");
        }
    }
}
