using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.Notifications
{
    public interface INotificationService
    {
        Task<ApiResult<PagedResult<NotificationVm>>> GetAllPaging(GetNotificationRequest request);
        Task<ApiResult<bool>> Create(CreateNotificationRequest request);
        Task<ApiResult<bool>> Delete(int id);
        Task<ApiResult<bool>> Prosess(ProsessRequest request);
    }
}
