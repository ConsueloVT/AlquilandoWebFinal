using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Excepciones;

namespace AL.Aplicacion.CasosDeUso;

public class ArchivarPublicacionCasoDeUso
{
    private readonly IAlojamientoRepositorio _alojamientoRepositorio;
    private readonly IReservasRepositorio _reservasRepositorio;

    public ArchivarPublicacionCasoDeUso(IAlojamientoRepositorio alojamientoRepositorio, IReservasRepositorio reservasRepositorio)
    {
        _alojamientoRepositorio = alojamientoRepositorio;
        _reservasRepositorio = reservasRepositorio;
    }

    public async Task<string> Ejecutar(int alojamientoId)
    {
        var alojamiento = await _alojamientoRepositorio.ObtenerPorId(alojamientoId);

        var hoy = DateTime.Today;
        var reservas = _reservasRepositorio.ObtenerReservasPorAlojamientoId(alojamientoId);

        bool tieneReservaEnCurso = reservas.Any(r => r.FechaInicioEstadia <= hoy && hoy <= r.FechaFinEstadia);
        if (tieneReservaEnCurso)
        {
            throw new AlojamientoConReservaEnCursoException(alojamiento.Nombre);
        }

        bool tieneReservasFuturas = reservas.Any(r => r.FechaInicioEstadia > hoy);
        if (tieneReservasFuturas)
        {
            _reservasRepositorio.CancelarReservasFuturasPorAlojamiento(alojamientoId);
        }

        alojamiento.Estado = Enumerativos.EstadoPublicacion.Archivado;
        _alojamientoRepositorio.Modificar(alojamiento);

        if (tieneReservasFuturas)
        {
            return "Publicación archivada. Las reservas futuras han sido canceladas.";
        }
        else
        {
            return "Publicación archivada exitosamente.";
        }
    }
}
