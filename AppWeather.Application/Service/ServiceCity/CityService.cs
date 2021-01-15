using AppWeather.Application.ViewModel.City;
using AppWeather.Application.ViewModel.Common;
using AppWeather.Data.EF;
using AppWeather.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWeather.Application.Service.ServiceCity
{
    public class CityService : ICityService
    {
        private readonly WeatherDbContext _context;
        public CityService(WeatherDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> Create(string name)
        {
            var city = await _context.Cities.Where(x => x.Name == name).ToListAsync();
            if (city.Count>0)
            {
                return new ApiErrorResult<bool>("Đã Tồn tại thành phố này ");
            }
            var nCity = new City()
            {
                Name = name
            };
            _context.Cities.Add(nCity);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy ");
            }
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return new ApiResult<bool>();
        }

        public async Task<ApiResult<PageResult<CityVm>>> GetAll()
        {
            var query = _context.Cities;

            int totalRow = await query.CountAsync();
            var data = await query
            .Select(x => new CityVm()
            {
                Id = x.Id,
                Name=x.Name,
            }).ToListAsync();

            var pagedResult = new PageResult<CityVm>()
            {
                Items = data,
                TotalRecords = totalRow
            };

            return new ApiSuccessResult<PageResult<CityVm>>(pagedResult);
        }

        public async Task<ApiResult<PageResult<CityVm>>> Getbykey(string key)
        {
            var query = from c in _context.Cities
                        select new { c };

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.c.Name.Contains(key));
            }

            int totalRow = await query.CountAsync();
            var data = await query
            .Select(x => new CityVm()
            {
                Id = x.c.Id,
                Name = x.c.Name,
            }).ToListAsync();

            var pagedResult = new PageResult<CityVm>()
            {
                Items = data,
                TotalRecords = totalRow
            };

            return new ApiSuccessResult<PageResult<CityVm>>(pagedResult);
        }
    }
}
