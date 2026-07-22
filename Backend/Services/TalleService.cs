using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.DTOs.Talles.Requests;
using Backend.DTOs.Talles.Responses;
using Backend.Entities;
using Backend.Enums;
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

        public async Task<ActualizarTalleResponseDto> ActualizarTalleAsync(ActualizarTalleRequestDto request)
        {
            ActualizarTalleResponseDto response = new();

            try
            {
                //Validar que el nombre no este vacio
                if (string.IsNullOrWhiteSpace(request.Nombre))
                {
                    response.Exito = false;
                    response.Mensaje = "El nombre del talle es obligatorio.";
                    return response;
                }

                //Validar que el tipo de talle se correcto
                if (!Enum.IsDefined(typeof(TipoTalle), request.TipoTalle))
                {
                    response.Exito = false;
                    response.Mensaje = "El tipo de talle seleccionado no es válido.";
                    return response;
                }

                //Buscar el talle
                Talle? talle = await _context.Talles
                    .FirstOrDefaultAsync(t => t.Id == request.Id);

                if (talle == null)
                {
                    response.Exito = false;
                    response.Mensaje = "El talle no existe.";
                    return response;
                }

                string nombreTalle = request.Nombre.Trim().ToUpper();

                //Validar que no exista otro talle con el mismo nombre y tipo
                bool existeTalle = await _context.Talles
                    .AnyAsync(t => t.Id != request.Id &&
                                t.Nombre.ToUpper() == nombreTalle &&
                                t.TipoTalle == request.TipoTalle);

                if (existeTalle)
                {
                    response.Exito = false;
                    response.Mensaje = "Ya existe un talle con ese nombre para ese tipo.";
                    return response;
                }

                //Actualizar datos
                talle.Nombre = request.Nombre.Trim();
                talle.TipoTalle = request.TipoTalle;

                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Talle actualizado correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al actualizar el talle. {ex.Message}";
            }

            return response;
        }

        public async Task<ConsultarTallesResponseDto> ConsultarTallesAsync(ConsultarTallesRequestDto request)
        {
            ConsultarTallesResponseDto response = new();

            try
            {
                response.Talles = await _context.Talles
                    .OrderBy(t => t.TipoTalle)
                    .ThenBy(t => t.Nombre)
                    .Select(t => new TalleResponseDto
                    {
                        Id = t.Id,
                        Nombre = t.Nombre,
                        TipoTalle = t.TipoTalle,
                        Activo = t.Activo
                    })
                    .ToListAsync();

                response.Exito = true;
                response.Mensaje = "Talles consultados correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al consultar los talles. {ex.Message}";
            }

            return response;
        }

        public async Task<EliminarTalleResponseDto> EliminarTalleAsync(EliminarTalleRequestDto request)
        {
            EliminarTalleResponseDto response = new();

            try
            {
                Talle? talle = await _context.Talles
                    .FirstOrDefaultAsync(t => t.Id == request.Id);

                if (talle == null)
                {
                    response.Exito = false;
                    response.Mensaje = "El talle no existe.";
                    return response;
                }

                talle.Activo = false;

                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Talle eliminado correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al eliminar el talle. {ex.Message}";
            }

            return response;
        }
        
    }
}