
using AppWeather.Application.ViewModel.Common;
using AppWeather.Application.ViewModel.History;
using System;
using System.Threading.Tasks;

namespace AppWeather.Application.Service.ServiceHistory
{
    public interface IHistoryService
    {
        
        Task<ApiResult<bool>> Create(CreateHistoryRequest request);
        Task<ApiResult<bool>> Delete(int id);
        Task<ApiResult<PageResult<HistoryVm>>> GetbyUser(Guid id);
        Task<ApiResult<PageResult<HistoryDetalVm>>> GetAllHistory();

    }
}
