using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Productos.Requests;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }


        [HttpPost("CrearProducto")]
        public async Task<IActionResult> CrearProducto(CrearProductoRequestDto request)
        {
            var response = await _productoService.CrearProductoAsync(request);

            return Ok(response);
        }

        [HttpPut("ActualizarProducto")]
        public async Task<IActionResult> ActualizarProducto(ActualizarProductoRequestDto request)
        {
            var response = await _productoService.ActualizarProductoAsync(request);

            return Ok(response);
        }

        [HttpGet("ConsultarProductos")]
        public async Task<IActionResult> ConsultarProductos()
        {
            var response = await _productoService.ConsultarProductosAsync(new ConsultarProductosRequestDto());

            return Ok(response);
        }

        [HttpDelete("EliminarProducto")]
        public async Task<IActionResult> EliminarProducto(EliminarProductoRequestDto request)
        {
            var response = await _productoService.EliminarProductoAsync(request);

            return Ok(response);
        }

        [HttpPost("AsignarTallesProducto")]
        public async Task<IActionResult> AsignarTallesProducto(AsignarTallesProductoRequestDto request)
        {
            var response = await _productoService.AsignarTallesProductoAsync(request);

            return Ok(response);
        }

        [HttpGet("ConsultarTallesProducto")]
        public async Task<IActionResult> ConsultarTallesProducto(ConsultarTallesProductoRequestDto request)
        {
            var response = await _productoService.ConsultarTallesProductoAsync(request);

            return Ok(response);
        }

        [HttpPut("ActualizarStockProducto")]
        public async Task<IActionResult> ActualizarStockProducto(ActualizarStockProductoRequestDto request)
        {
            var response = await _productoService.ActualizarStockProductoAsync(request);

            return Ok(response);
        }

        [HttpDelete("QuitarTalleProducto")]
        public async Task<IActionResult> QuitarTalleProducto(QuitarTalleProductoRequestDto request)
        {
            var response = await _productoService.QuitarTalleProductoAsync(request);

            return Ok(response);
        }
    }
}