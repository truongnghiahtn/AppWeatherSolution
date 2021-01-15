using AppWeather.Application.Service.ServiceHistory;
using AppWeather.Application.ViewModel.History;
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
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;
        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Create(CreateHistoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _historyService.Create(request);
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

            var result = await _historyService.Delete(id);
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
            var data = await _historyService.GetAllHistory();
            return Ok(data);
        }

        [HttpGet("ByUser")]
        [AllowAnonymous]

        public async Task<IActionResult> GetByUser(Guid id)
        {
            var data = await _historyService.GetbyUser(id);
            return Ok(data);
        }
    }
}
