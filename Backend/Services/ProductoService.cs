using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.DTOs.Productos.Requests;
using Backend.DTOs.Productos.Responses;
using Backend.Entities;
using Backend.Enums;
using Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CrearProductoResponseDto> CrearProductoAsync(CrearProductoRequestDto request)
        {
            CrearProductoResponseDto response = new();

            try
            {
                //Validar nombre
                if (string.IsNullOrWhiteSpace(request.Nombre))
                {
                    response.Exito = false;
                    response.Mensaje = "El nombre del producto es obligatorio.";
                    return response;
                }

                //Validar precio
                if (request.Precio <= 0)
                {
                    response.Exito = false;
                    response.Mensaje = "El precio debe ser mayor a cero.";
                    return response;
                }

                //Validar genero
                if (!Enum.IsDefined(typeof(Genero), request.Genero))
                {
                    response.Exito = false;
                    response.Mensaje = "El género seleccionado no es válido.";
                    return response;
                }

                //Validar que exista la categoria
                bool existeCategoria = await _context.Categorias
                    .AnyAsync(c => c.Id == request.CategoriaId);

                if (!existeCategoria)
                {
                    response.Exito = false;
                    response.Mensaje = "La categoría seleccionada no existe.";
                    return response;
                }

                string nombreProducto = request.Nombre.Trim().ToUpper();

                //Validar duplicado
                bool existeProducto = await _context.Productos
                    .AnyAsync(p => p.Nombre.ToUpper() == nombreProducto &&
                                p.CategoriaId == request.CategoriaId);

                if (existeProducto)
                {
                    response.Exito = false;
                    response.Mensaje = "Ya existe un producto con ese nombre en la categoría seleccionada.";
                    return response;
                }

                Producto producto = new()
                {
                    Nombre = request.Nombre.Trim(),
                    Descripcion = request.Descripcion?.Trim(),
                    Precio = request.Precio,
                    Genero = request.Genero,
                    CategoriaId = request.CategoriaId,
                    RutaImagen = request.RutaImagen
                };

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Producto creado correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al crear el producto. {ex.Message}";
            }

            return response;
        }

        public async Task<ActualizarProductoResponseDto> ActualizarProductoAsync(ActualizarProductoRequestDto request)
        {
            ActualizarProductoResponseDto response = new();

            try
            {
                //Validar nombre
                if (string.IsNullOrWhiteSpace(request.Nombre))
                {
                    response.Exito = false;
                    response.Mensaje = "El nombre del producto es obligatorio.";
                    return response;
                }

                //Validar precio
                if (request.Precio <= 0)
                {
                    response.Exito = false;
                    response.Mensaje = "El precio debe ser mayor a cero.";
                    return response;
                }

                //Validar genero
                if (!Enum.IsDefined(typeof(Genero), request.Genero))
                {
                    response.Exito = false;
                    response.Mensaje = "El género seleccionado no es válido.";
                    return response;
                }

                //Buscar el producto
                Producto? producto = await _context.Productos
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                if (producto == null)
                {
                    response.Exito = false;
                    response.Mensaje = "El producto no existe.";
                    return response;
                }

                //Validar que exista la categoria
                bool existeCategoria = await _context.Categorias
                    .AnyAsync(c => c.Id == request.CategoriaId);

                if (!existeCategoria)
                {
                    response.Exito = false;
                    response.Mensaje = "La categoría seleccionada no existe.";
                    return response;
                }

                string nombreProducto = request.Nombre.Trim().ToUpper();

                //Validar duplicado
                bool existeProducto = await _context.Productos
                    .AnyAsync(p => p.Id != request.Id &&
                                p.Nombre.ToUpper() == nombreProducto &&
                                p.CategoriaId == request.CategoriaId);

                if (existeProducto)
                {
                    response.Exito = false;
                    response.Mensaje = "Ya existe un producto con ese nombre en la categoría seleccionada.";
                    return response;
                }

                //Actualizar datos
                producto.Nombre = request.Nombre.Trim();
                producto.Descripcion = request.Descripcion?.Trim();
                producto.Precio = request.Precio;
                producto.Genero = request.Genero;
                producto.RutaImagen = request.RutaImagen;
                producto.CategoriaId = request.CategoriaId;

                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Producto actualizado correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al actualizar el producto. {ex.Message}";
            }

            return response;
        }

        public async Task<ConsultarProductosResponseDto> ConsultarProductosAsync(ConsultarProductosRequestDto request)
        {
            ConsultarProductosResponseDto response = new();

            try
            {
                response.Productos = await _context.Productos
                    .Include(p => p.Categoria)
                    .OrderBy(p => p.Nombre)
                    .Select(p => new ProductoResponseDto
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        Descripcion = p.Descripcion,
                        Precio = p.Precio,
                        Genero = p.Genero,
                        RutaImagen = p.RutaImagen,
                        CategoriaId = p.CategoriaId,
                        Categoria = p.Categoria.Nombre,
                        Activo = p.Activo,
                        FechaAlta = p.FechaAlta
                    })
                    .ToListAsync();

                response.Exito = true;
                response.Mensaje = "Productos consultados correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al consultar los productos. {ex.Message}";
            }

            return response;
        }



        public async Task<EliminarProductoResponseDto> EliminarProductoAsync(EliminarProductoRequestDto request)
        {
            EliminarProductoResponseDto response = new();

            try
            {
                //Buscar el producto
                Producto? producto = await _context.Productos
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                if (producto == null)
                {
                    response.Exito = false;
                    response.Mensaje = "El producto no existe.";
                    return response;
                }

                //Cambiar estado
                producto.Activo = false;

                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Producto eliminado correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al eliminar el producto. {ex.Message}";
            }

            return response;
        }

        public async Task<AsignarTallesProductoResponseDto> AsignarTallesProductoAsync(AsignarTallesProductoRequestDto request)
        {
            AsignarTallesProductoResponseDto response = new();

            try
            {
                Producto? producto = await _context.Productos
                    .Include(p => p.Categoria)
                    .FirstOrDefaultAsync(p => p.Id == request.ProductoId);

                if (producto == null)
                {
                    response.Exito = false;
                    response.Mensaje = "El producto no existe.";
                    return response;
                }

                if (request.Talles == null || !request.Talles.Any())
                {
                    response.Exito = false;
                    response.Mensaje = "Debe ingresar al menos un talle.";
                    return response;
                }

                foreach (var item in request.Talles)
                {
                    Talle? talle = await _context.Talles
                        .FirstOrDefaultAsync(t => t.Id == item.TalleId);

                    if (talle == null)
                    {
                        response.Exito = false;
                        response.Mensaje = $"El talle con Id {item.TalleId} no existe.";
                        return response;
                    }

                    if (talle.TipoTalle != producto.Categoria.TipoTalle)
                    {
                        response.Exito = false;
                        response.Mensaje = $"El talle '{talle.Nombre}' no corresponde al tipo de talle de la categoría.";
                        return response;
                    }

                    bool existe = await _context.ProductoTalles
                        .AnyAsync(pt => pt.ProductoId == request.ProductoId &&
                                        pt.TalleId == item.TalleId);

                    if (existe)
                    {
                        response.Exito = false;
                        response.Mensaje = $"El talle '{talle.Nombre}' ya fue asignado al producto.";
                        return response;
                    }

                    _context.ProductoTalles.Add(new ProductoTalle
                    {
                        ProductoId = request.ProductoId,
                        TalleId = item.TalleId,
                        Stock = item.Stock
                    });
                }

                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Talles asignados correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al asignar los talles. {ex.Message}";
            }

            return response;
        }

        public async Task<ConsultarTallesProductoResponseDto> ConsultarTallesProductoAsync(ConsultarTallesProductoRequestDto request)
        {
            ConsultarTallesProductoResponseDto response = new();

            try
            {
                bool existeProducto = await _context.Productos
                    .AnyAsync(p => p.Id == request.ProductoId);

                if (!existeProducto)
                {
                    response.Exito = false;
                    response.Mensaje = "El producto no existe.";
                    return response;
                }

                response.Talles = await _context.ProductoTalles
                    .Where(pt => pt.ProductoId == request.ProductoId)
                    .Include(pt => pt.Talle)
                    .OrderBy(pt => pt.Talle.Nombre)
                    .Select(pt => new ProductoTalleResponseDto
                    {
                        ProductoTalleId = pt.Id,
                        TalleId = pt.TalleId,
                        Talle = pt.Talle.Nombre,
                        Stock = pt.Stock
                    })
                    .ToListAsync();

                response.Exito = true;
                response.Mensaje = "Talles consultados correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al consultar los talles del producto. {ex.Message}";
            }

            return response;
        }

        public async Task<ActualizarStockProductoResponseDto> ActualizarStockProductoAsync(ActualizarStockProductoRequestDto request)
        {
            ActualizarStockProductoResponseDto response = new();

            try
            {
                if (request.Stock < 0)
                {
                    response.Exito = false;
                    response.Mensaje = "El stock no puede ser negativo.";
                    return response;
                }

                ProductoTalle? productoTalle = await _context.ProductoTalles
                    .FirstOrDefaultAsync(pt => pt.Id == request.ProductoTalleId);

                if (productoTalle == null)
                {
                    response.Exito = false;
                    response.Mensaje = "El talle del producto no existe.";
                    return response;
                }

                productoTalle.Stock = request.Stock;

                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Stock actualizado correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al actualizar el stock. {ex.Message}";
            }

            return response;
        }

        public async Task<QuitarTalleProductoResponseDto> QuitarTalleProductoAsync(QuitarTalleProductoRequestDto request)
        {
            QuitarTalleProductoResponseDto response = new();

            try
            {
                ProductoTalle? productoTalle = await _context.ProductoTalles
                    .FirstOrDefaultAsync(pt => pt.Id == request.ProductoTalleId);

                if (productoTalle == null)
                {
                    response.Exito = false;
                    response.Mensaje = "El talle del producto no existe.";
                    return response;
                }

                _context.ProductoTalles.Remove(productoTalle);

                await _context.SaveChangesAsync();

                response.Exito = true;
                response.Mensaje = "Talle quitado del producto correctamente.";
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Mensaje = $"Ocurrió un error al quitar el talle del producto. {ex.Message}";
            }

            return response;
        }
    }
}