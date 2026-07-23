using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Services.Interfaces;

namespace Backend.Services
{
    public class ImagenLocalService : IImagenService
    {
        private readonly IWebHostEnvironment _environment;

        public ImagenLocalService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SubirImagenAsync(IFormFile imagen)
        {
            //Carpeta donde se guardan las imagenes
            var rutaCarpeta = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);

            var extension = Path.GetExtension(imagen.FileName);
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";

            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            using var stream = new FileStream(rutaCompleta, FileMode.Create);
            await imagen.CopyToAsync(stream);

            //Ruta de la imagen que se guarda en la base de datos
            return $"/uploads/{nombreArchivo}";
        }
    }
}