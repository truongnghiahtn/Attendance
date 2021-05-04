using AppAttendance.ViewModel.Common;
using AppAttendance.ViewModel.CountData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppAttendance.Application.CountData
{
    public interface ICountDataService
    {
        Task<ApiResult<CountDataVm>> GetCountDataVm();
    }
}
