using AppWeather.Application.ViewModel.Common;
using AppWeather.Application.ViewModel.User;
using System;
using System.Threading.Tasks;

namespace AppWeather.Application.Service.ServiceUser
{
    public interface IServiceUser
    {
        Task<ApiResult<ResultLogin>> LoginUser(LoginUser request);
        Task<ApiResult<bool>> RegisterUser(RequestUser request);
        //Task<ApiResult<UserVm>> GetUserById(Guid id);
        Task<ApiResult<PageResult<UserVm>>> GetAllUser();
    }
}
