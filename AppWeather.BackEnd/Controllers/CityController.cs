using AppWeather.Application.Service.ServiceCity;
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
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Create(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cityService.Create(name);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete]
        [AllowAnonymous]

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _cityService.Delete(id);
            if (!result.IsSuccessed)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpGet("All")]
        [AllowAnonymous]

        public async Task<IActionResult> GetAll()
        {
            var data = await _cityService.GetAll();
            return Ok(data);
        }

        [HttpGet("ByKey")]
        [AllowAnonymous]

        public async Task<IActionResult> GetbyKey(string key)
        {
            var data = await _cityService.Getbykey(key);
            return Ok(data);
        }
    }
}
