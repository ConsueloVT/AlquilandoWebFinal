using Microsoft.AspNetCore.Components.Forms;
using System.IO;
using System.Threading.Tasks;
namespace AL.UI.Servicios;
public class ServicioArchivos
    {
        private readonly string _rutaBase;

        public ServicioArchivos()
        {
            // La ruta absoluta donde se guardan los archivos
            _rutaBase = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes", "reservas");
            
            if (!Directory.Exists(_rutaBase))
            {
                Directory.CreateDirectory(_rutaBase);
            }
        }

        public async Task<string> GuardarArchivoAsync(IBrowserFile archivo)
        {
            var nombreUnico = $"{Guid.NewGuid()}_{archivo.Name}";
            var rutaCompleta = Path.Combine(_rutaBase, nombreUnico);

            await using var stream = new FileStream(rutaCompleta, FileMode.Create);
            await archivo.OpenReadStream(10 * 1024 * 1024).CopyToAsync(stream); // max 10MB

            // Retorna la ruta relativa para usar en la UI
            return $"/imagenes/reservas/{nombreUnico}";
        }
}

