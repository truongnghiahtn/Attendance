using AppAttendance.Data.EF;
using AppAttendance.Data.Entities;
using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.System.Subjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppAttendance.Application.System.Subjects
{
    public class SubjectService : ISubjectService
    {
        private readonly AppAttendanceDbContext _context;

        public SubjectService(AppAttendanceDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> CreateSubject(CreateSubjectRequest request)
        {
            var subject = new Subject()
            {
                Name = request.Name,
                Description = request.Description,
                NumberOfCredits=request.NumberOfCredits,
                Lesson=request.Lesson,
                DateCreate = DateTime.UtcNow.AddHours(7),
                DateUpdate = DateTime.UtcNow.AddHours(7),
            };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>("Tạo thành công");
        }

        public async Task<ApiResult<bool>> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Xóa thành công");
        }

        public async Task<ApiResult<PagedResult<SubjectVm>>> GetAll()
        {
            var query = from s in _context.Subjects
                        select new { s };
            //Paging
            int totalRow = await query.CountAsync();
            var data = await query
                .Select(x => new SubjectVm()
                {
                    Id_Subject = x.s.Id_Subject,
                    Name = x.s.Name,
                    Description = x.s.Description,
                    NumberOfCredits = x.s.NumberOfCredits,
                    Lesson = x.s.Lesson,
                    DateCreate = x.s.DateCreate,
                    DateUpdate = x.s.DateUpdate,

                }).ToListAsync();

            var pagedResult = new PagedResult<SubjectVm>()
            {
                TotalRecords = totalRow,
                PageSize = totalRow,
                PageIndex = 1,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<SubjectVm>>(pagedResult);
        }

        public async Task<ApiResult<PagedResult<SubjectVm>>> GetAllPaging(GetSubjectPagingRequest request)
        {
            var query = from s in _context.Subjects
                        select new { s };
            //filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.s.Name.Contains(request.Keyword) || x.s.Description.Contains(request.Keyword));
            }

            //Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new SubjectVm()
                {
                    Id_Subject = x.s.Id_Subject,
                    Name = x.s.Name,
                    Description = x.s.Description,
                    NumberOfCredits=x.s.NumberOfCredits,
                    Lesson=x.s.Lesson,
                    DateCreate = x.s.DateCreate,
                    DateUpdate = x.s.DateUpdate,
                    
                }).ToListAsync();

            var pagedResult = new PagedResult<SubjectVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<SubjectVm>>(pagedResult);
        }

        public async Task<ApiResult<SubjectVm>> GetSubjectById(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return new ApiErrorResult<SubjectVm>("Không tìm thấy ");
            }
            var data = new SubjectVm()
            {
                Id_Subject = subject.Id_Subject,
                Name = subject.Name,
                NumberOfCredits=subject.NumberOfCredits,
                Lesson=subject.Lesson,
                Description = subject.Description,
                DateCreate = subject.DateCreate,
                DateUpdate = subject.DateUpdate,
            };
            return new ApiSuccessResult<SubjectVm>(data);
        }

        public async Task<ApiResult<bool>> UpdateSubject(UpdateSubjectRequest request)
        {
            var subject = await _context.Subjects.FindAsync(request.Id_Subject);
            if (subject == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            subject.Name = request.Name;
            subject.Description = request.Description;
            subject.NumberOfCredits = request.NumberOfCredits;
            subject.Lesson = request.Lesson;
            subject.DateUpdate = DateTime.UtcNow.AddHours(7);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>("Cập nhật thành công");
        }
    }
}
