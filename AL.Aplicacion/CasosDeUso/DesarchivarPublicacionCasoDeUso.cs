using AL.Aplicacion.Interfaces;
namespace AL.Aplicacion.CasosDeUso;

public class DesarchivarPublicacionCasoDeUso
{
    private readonly IAlojamientoRepositorio _alojamientoRepositorio;

    public DesarchivarPublicacionCasoDeUso(IAlojamientoRepositorio alojamientoRepositorio)
    {
        _alojamientoRepositorio = alojamientoRepositorio;
    }

    public async Task Ejecutar(int alojamientoId)
    {
        var alojamiento = await _alojamientoRepositorio.ObtenerPorId(alojamientoId);

        if (alojamiento != null)
        {
            alojamiento.Estado = Enumerativos.EstadoPublicacion.Publicado;
            _alojamientoRepositorio.Modificar(alojamiento);
        }
        else
        {
            // Manejar el caso cuando alojamiento es null, por ejemplo lanzar una excepción o registrar un error
            throw new InvalidOperationException($"No se encontró alojamiento con ID {alojamientoId}");
        }
    }
}
