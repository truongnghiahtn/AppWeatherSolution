using AppWeather.Application.ViewModel.City;
using AppWeather.Application.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppWeather.Application.Service.ServiceCity
{
    public interface ICityService
    {
        Task<ApiResult<PageResult<CityVm>>> GetAll();
        Task<ApiResult<bool>> Create(string name);
        Task<ApiResult<bool>> Delete(int id);

        Task<ApiResult<PageResult<CityVm>>> Getbykey(string key);

    }
}
