using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Categorias.Requests;
using Backend.DTOs.Categorias.Responses;

namespace Backend.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<CrearCategoriaResponseDto> CrearCategoriaAsync(CrearCategoriaRequestDto request);
        Task<ConsultarCategoriasResponseDto> ConsultarCategoriasAsync(ConsultarCategoriasRequestDto request);
        Task<ActualizarCategoriaResponseDto> ActualizarCategoriaAsync(ActualizarCategoriaRequestDto request);
        Task<EliminarCategoriaResponseDto> EliminarCategoriaAsync(EliminarCategoriaRequestDto request);
    }
}