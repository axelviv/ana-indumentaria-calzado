using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Requests;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TallesController : ControllerBase
    {
        private readonly ITalleService _talleService;

        public TallesController(ITalleService talleService)
        {
            _talleService = talleService;
        }

        [HttpPost("CrearTalle")]
        public async Task<IActionResult> CrearTalle([FromBody] CrearTalleRequestDto request)
        {
            var response = await _talleService.CrearTalleAsync(request);

            return Ok(response);
        }
    }
}