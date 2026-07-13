using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Interfaces
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CrearCategoriaResponseDto> CrearCategoriaAsync(CrearCategoriaRequestDto request)
        {
            CrearCategoriaResponseDto response = new();

            try
            {
                //Validar que el nombre no este vacio
                if (string.IsNullOrWhiteSpace(request.Nombre))
                {
                    response.Exito = false;
                    response.Mensaje = "El nombre de la categoría es obligatorio.";
                    return response;
                }

                string nombreCategoria = request.Nombre.Trim().ToUpper();

                //Validar que no exista otra categoria con el mismo nombre
                bool existeCategoria = await _context.Categorias
                    .AnyAsync(c => c.Nombre.ToUpper() == nombreCategoria);

                if (existeCategoria)
                {
                    response.Exito = false;
                    response.Mensaje = "Ya existe una categoría con ese nombre.";
                    return response;
                }

                //Crear la entidad
                Categoria categoria = new()
                {
                    Nombre = request.Nombre.Trim(),
                    TipoTalle = request.TipoTalle
                };

                //Guardar en la base de datos
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Categoría creada correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al crear la categoría. {ex.Message}";
            }

            return response;
        }

        public async Task<ActualizarCategoriaResponseDto> ActualizarCategoriaAsync(ActualizarCategoriaRequestDto request)
        {
            ActualizarCategoriaResponseDto response = new();

            try
            {
                //Validar que el nombre no este vacio
                if (string.IsNullOrWhiteSpace(request.Nombre))
                {
                    response.Exito = false;
                    response.Mensaje = "El nombre de la categoría es obligatorio.";
                    return response;
                }

                //Buscar la categoria
                Categoria? categoria = await _context.Categorias
                    .FirstOrDefaultAsync(c => c.Id == request.Id);

                if (categoria == null)
                {
                    response.Exito = false;
                    response.Mensaje = "La categoría no existe.";
                    return response;
                }

                string nombreCategoria = request.Nombre.Trim().ToUpper();

                //Validar que no exista otra categoria con el mismo nombre
                bool existeCategoria = await _context.Categorias
                    .AnyAsync(c => c.Id != request.Id &&
                                c.Nombre.ToUpper() == nombreCategoria);

                if (existeCategoria)
                {
                    response.Exito = false;
                    response.Mensaje = "Ya existe otra categoría con ese nombre.";
                    return response;
                }

                //Actualizar los datos
                categoria.Nombre = request.Nombre.Trim();
                categoria.TipoTalle = request.TipoTalle;

                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Categoría actualizada correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al actualizar la categoría. {ex.Message}";
            }

            return response;
        }

        public async Task<ConsultarCategoriasResponseDto> ConsultarCategoriasAsync(ConsultarCategoriasRequestDto request)
        {
            ConsultarCategoriasResponseDto response = new();

            try
            {
                response.Categorias = await _context.Categorias
                    .OrderBy(c => c.Nombre)
                    .Select(c => new CategoriaResponseDto
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        TipoTalle = c.TipoTalle
                    })
                    .ToListAsync();

                response.Exito = true;
                response.Mensaje = "Categorías consultadas correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al consultar las categorías. {ex.Message}";
            }

            return response;
        }

        public async Task<EliminarCategoriaResponseDto> EliminarCategoriaAsync(EliminarCategoriaRequestDto request)
        {
            EliminarCategoriaResponseDto response = new();

            try
            {
                //Buscar la categoria
                Categoria? categoria = await _context.Categorias
                    .FirstOrDefaultAsync(c => c.Id == request.Id);

                if (categoria == null)
                {
                    response.Exito = false;
                    response.Mensaje = "La categoría no existe.";
                    return response;
                }

                //Verificar si tiene productos asociados
                bool tieneProductos = await _context.Productos
                    .AnyAsync(p => p.CategoriaId == request.Id);

                if (tieneProductos)
                {
                    response.Exito = false;
                    response.Mensaje = "No se puede eliminar la categoría porque tiene productos asociados.";
                    return response;
                }

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Categoría eliminada correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al eliminar la categoría. {ex.Message}";
            }

            return response;
        }
    }
}