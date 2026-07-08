using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;

namespace Backend.Services.Interfaces
{
    public interface ITalleService
    {
       Task<CrearTalleResponseDto> CrearTalleAsync(CrearTalleRequestDto request);
        Task<ConsultarTallesResponseDto> ConsultarTallesAsync(ConsultarTallesRequestDto request);
        Task<ActualizarTalleResponseDto> ActualizarTalleAsync(ActualizarTalleRequestDto request);
        Task<EliminarTalleResponseDto> EliminarTalleAsync(EliminarTalleRequestDto request);
    }
}