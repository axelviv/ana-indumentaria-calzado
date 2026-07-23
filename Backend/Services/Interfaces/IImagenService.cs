using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface IImagenService
    {
        Task<string> SubirImagenAsync(IFormFile imagen);
    }
}