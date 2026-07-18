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
        public async Task<IActionResult> CrearTalle(CrearTalleRequestDto request)
        {
            var response = await _talleService.CrearTalleAsync(request);

            return Ok(response);
        }

        [HttpPut("ActualizarTalle")]
        public async Task<IActionResult> ActualizarTalle(ActualizarTalleRequestDto request)
        {
            var response = await _talleService.ActualizarTalleAsync(request);

            return Ok(response);
        }

        [HttpGet("ConsultarTalles")]
        public async Task<IActionResult> ConsultarTalles()
        {
            var response = await _talleService.ConsultarTallesAsync(new ConsultarTallesRequestDto());

            return Ok(response);
        }

        [HttpDelete("EliminarTalle")]
        public async Task<IActionResult> EliminarTalle(EliminarTalleRequestDto request)
        {
            var response = await _talleService.EliminarTalleAsync(request);

            return Ok(response);
        }
        

    }
}