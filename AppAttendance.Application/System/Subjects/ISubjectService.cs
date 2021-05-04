using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.System.Subjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.System.Subjects
{
    public interface ISubjectService
    {
        Task<ApiResult<bool>> CreateSubject(CreateSubjectRequest request);
        Task<ApiResult<bool>> DeleteSubject(int id);
        Task<ApiResult<bool>> UpdateSubject(UpdateSubjectRequest request);
        Task<ApiResult<PagedResult<SubjectVm>>> GetAllPaging(GetSubjectPagingRequest request);
        Task<ApiResult<PagedResult<SubjectVm>>> GetAll();
        Task<ApiResult<SubjectVm>> GetSubjectById(int id);
    }
}
