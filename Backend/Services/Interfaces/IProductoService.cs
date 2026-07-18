using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;

namespace Backend.Services.Interfaces
{
    public interface IProductoService
    {
        Task<CrearProductoResponseDto> CrearProductoAsync(CrearProductoRequestDto request);
        Task<ConsultarProductosResponseDto> ConsultarProductosAsync(ConsultarProductosRequestDto request);
        Task<ActualizarProductoResponseDto> ActualizarProductoAsync(ActualizarProductoRequestDto request);
        Task<EliminarProductoResponseDto> EliminarProductoAsync(EliminarProductoRequestDto request);
        Task<AsignarTallesProductoResponseDto> AsignarTallesProductoAsync(AsignarTallesProductoRequestDto request);
        
    }
}