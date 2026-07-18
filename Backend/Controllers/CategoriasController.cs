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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost("CrearCategoria")]
        public async Task<IActionResult> CrearCategoria(CrearCategoriaRequestDto request)
        {
            var response = await _categoriaService.CrearCategoriaAsync(request);

            return Ok(response);
        }

        [HttpPut("ActualizarCategoria")]
        public async Task<IActionResult> ActualizarCategoria(ActualizarCategoriaRequestDto request)
        {
            var response = await _categoriaService.ActualizarCategoriaAsync(request);

            return Ok(response);
        }

        [HttpGet("ConsultarCategorias")]
        public async Task<IActionResult> ConsultarCategorias()
        {
            var response = await _categoriaService.ConsultarCategoriasAsync(new ConsultarCategoriasRequestDto());

            return Ok(response);
        }

        [HttpDelete("EliminarCategoria")]
        public async Task<IActionResult> EliminarCategoria(EliminarCategoriaRequestDto request)
        {
            var response = await _categoriaService.EliminarCategoriaAsync(request);

            return Ok(response);
        }
    }
}