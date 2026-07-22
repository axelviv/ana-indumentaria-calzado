using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Productos.Requests;
using Backend.DTOs.Productos.Responses;

namespace Backend.Services.Interfaces
{
    public interface IProductoService
    {
        Task<CrearProductoResponseDto> CrearProductoAsync(CrearProductoRequestDto request);
        Task<ConsultarProductosResponseDto> ConsultarProductosAsync(ConsultarProductosRequestDto request);
        Task<ActualizarProductoResponseDto> ActualizarProductoAsync(ActualizarProductoRequestDto request);
        Task<EliminarProductoResponseDto> EliminarProductoAsync(EliminarProductoRequestDto request);
        Task<AsignarTallesProductoResponseDto> AsignarTallesProductoAsync(AsignarTallesProductoRequestDto request);
        Task<ConsultarTallesProductoResponseDto> ConsultarTallesProductoAsync(ConsultarTallesProductoRequestDto request);
        Task<ActualizarStockProductoResponseDto> ActualizarStockProductoAsync(ActualizarStockProductoRequestDto request);
        Task<QuitarTalleProductoResponseDto> QuitarTalleProductoAsync(QuitarTalleProductoRequestDto request);
    }
}