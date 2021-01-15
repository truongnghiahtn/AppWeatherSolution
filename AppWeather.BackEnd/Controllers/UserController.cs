using AppWeather.Application.Service.ServiceUser;
using AppWeather.Application.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWeather.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceUser _serviceUser;
        public UserController(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
        }

        [HttpPost("Login")]
        [AllowAnonymous]

        public async Task<IActionResult> Login([FromBody] LoginUser request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _serviceUser.LoginUser(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]

        public async Task<IActionResult> Register([FromBody] RequestUser request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceUser.RegisterUser(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> GetAll()
        {
            var data = await _serviceUser.GetAllUser();
            return Ok(data);
        }
    }
}
