using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Entities;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class TalleService : ITalleService
    {
        private readonly AppDbContext _context;

        public TalleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CrearTalleResponseDto> CrearTalleAsync(CrearTalleRequestDto request)
        {
            CrearTalleResponseDto response = new();

            try
            {
                //Validar que el nombre no este vacio
                if (string.IsNullOrWhiteSpace(request.Nombre))
                {
                    response.Exito = false;
                    response.Mensaje = "El nombre del talle es obligatorio.";
                    return response;
                }

                string nombreTalle = request.Nombre.Trim().ToUpper();

                // Validar que no exista otro talle con el mismo nombre y tipo.
                bool existeTalle = await _context.Talles
                    .AnyAsync(t => t.Nombre.ToUpper() == nombreTalle &&
                                t.TipoTalle == request.TipoTalle);

                if (existeTalle)
                {
                    response.Exito = false;
                    response.Mensaje = "Ya existe un talle con ese nombre para ese tipo.";
                    return response;
                }

                //Crear la entidad
                Talle talle = new()
                {
                    Nombre = request.Nombre.Trim(),
                    TipoTalle = request.TipoTalle
                };

                // Guardar en la base de datos.
                _context.Talles.Add(talle);
                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Talle creado correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al crear el talle. {ex.Message}";
            }

            return response;
        }

        public Task<ActualizarTalleResponseDto> ActualizarTalleAsync(ActualizarTalleRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<ConsultarTallesResponseDto> ConsultarTallesAsync(ConsultarTallesRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<EliminarTalleResponseDto> EliminarTalleAsync(EliminarTalleRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}