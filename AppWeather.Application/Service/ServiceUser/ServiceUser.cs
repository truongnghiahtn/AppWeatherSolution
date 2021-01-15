using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AppWeather.Data.Entities;
using AppWeather.Data.EF;
using AppWeather.Application.ViewModel.Common;
using AppWeather.Application.ViewModel.User;
using Microsoft.EntityFrameworkCore;

namespace AppWeather.Application.Service.ServiceUser
{
    public class ServiceUser : IServiceUser
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly IConfiguration _config;
        private readonly WeatherDbContext _context;

        public ServiceUser(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config, WeatherDbContext context)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _config = config;
            _context = context;

        }

        public async Task<ApiResult<PageResult<UserVm>>> GetAllUser()
        {
            var query = _userManager.Users;

            int totalRow = await query.CountAsync();
            var data = await query
            .Select(x => new UserVm()
            {
                Id = x.Id,
                
                FirstName= x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
            }).ToListAsync();

            var pagedResult = new PageResult<UserVm>()
            {
                Items = data,
                TotalRecords = totalRow
            };

            return new ApiSuccessResult<PageResult<UserVm>>(pagedResult);
        }

        public async Task<ApiResult<ResultLogin>> LoginUser(LoginUser request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return new ApiErrorResult<ResultLogin>("Tài Khoản của bạn không tồn tại");
            }
            var result = await _singInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

            if (!result.Succeeded)
            {
                return new ApiErrorResult<ResultLogin>("mật Khẩu không đúng");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.Username)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);


            var data = new ResultLogin()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return new ApiSuccessResult<ResultLogin>(data);
        }
        public async Task<ApiResult<bool>> RegisterUser(RequestUser request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản bạn đã tồn tại");
            }
            user = new AppUser()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new ApiErrorResult<bool>("Đăng ký không thành công");
            }
            return new ApiSuccessResult<bool>();

        }
    }
}
