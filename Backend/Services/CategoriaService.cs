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

        public Task<ActualizarCategoriaResponseDto> ActualizarCategoriaAsync(ActualizarCategoriaRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<ConsultarCategoriasResponseDto> ConsultarCategoriasAsync(ConsultarCategoriasRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<EliminarCategoriaResponseDto> EliminarCategoriaAsync(EliminarCategoriaRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}