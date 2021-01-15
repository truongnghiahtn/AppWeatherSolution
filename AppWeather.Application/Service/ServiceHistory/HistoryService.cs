using AppWeather.Application.ViewModel.Common;
using AppWeather.Application.ViewModel.History;
using AppWeather.Data.EF;
using AppWeather.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AppWeather.Application.Service.ServiceHistory
{
    public class HistoryService : IHistoryService
    {
        private readonly WeatherDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HistoryService(WeatherDbContext context , UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ApiResult<bool>> Create(CreateHistoryRequest request)
        {
            var history = new History()
            {
                IdUser = request.IdUser,
                IdCity = request.IdCity,
                Wind = request.Wind,
                Cloudiness = request.Cloudiness,
                Humidity = request.Humidity,
                Pressure = request.Pressure,
                DateCreate = DateTime.UtcNow.AddHours(7)
            };
            _context.Histories.Add(history);
            await _context.SaveChangesAsync();

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var history = await _context.Histories.FindAsync(id);
            if (history == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy");
            }

            _context.Histories.Remove(history);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PageResult<HistoryDetalVm>>> GetAllHistory()
        {
            var query = from h in _context.Histories
                       join u in _userManager.Users on h.IdUser equals u.Id
                       join c in _context.Cities on h.IdCity equals c.Id
                        select new HistoryDetalVm()
                        {
                            Id = h.Id,
                            UserName = u.UserName,
                            NameCity = c.Name,
                            Cloudiness = h.Cloudiness,
                            Humidity = h.Humidity,
                            Pressure = h.Pressure,
                            Wind = h.Wind,
                            DateCreate = h.DateCreate
                        };

            var datas = query.ToList();

            var result = new PageResult<HistoryDetalVm>()
            {
                Items = datas,
                TotalRecords = datas.Count(),
            };
            return new ApiSuccessResult<PageResult<HistoryDetalVm>>(result);
        }

        public async Task<ApiResult<PageResult<HistoryVm>>> GetbyUser(Guid id)
        {
            var query = from h in _context.Histories
                        join u in _userManager.Users on h.IdUser equals u.Id
                        join c in _context.Cities on h.IdCity equals c.Id
                        where u.Id == id
                        select new { h, u, c };

            var data = query.Select(x => new HistoryVm()
            {
                    Id = x.h.Id,
                    NameCity =x.c.Name,
                    Cloudiness = x.h.Cloudiness,
                    Humidity = x.h.Humidity,
                    Pressure = x.h.Pressure,
                    Wind = x.h.Wind,
                    DateCreate = x.h.DateCreate
            }).ToList();

            var result = new PageResult<HistoryVm>()
            {
                Items = data,
                TotalRecords = data.Count()
            };
            return new ApiSuccessResult<PageResult<HistoryVm>>(result);
        }
    }
}
