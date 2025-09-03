using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;

namespace AL.UI.Servicios;

public class ServicioReserva : IServicioReserva
{
    private readonly IReservasRepositorio _reservasRepositorio;
    private readonly IServicioSesion _sesion;

    public ServicioReserva(IReservasRepositorio reservasRepositorio, IServicioSesion sesion)
    {
        _reservasRepositorio = reservasRepositorio;
        _sesion = sesion;
    }

    public List<Reserva> ObtenerReservasDelUsuario()
    {
        return _reservasRepositorio.ObtenerReservasPorUsuarioId(_sesion.Id);
    }

    public string CancelarReservaConDemanda(int idReserva)
    {
        var reserva = _reservasRepositorio.ObtenerPorId(idReserva);
        if (reserva == null)
            return "Reserva no encontrada.";

        if (DateTime.Now >= reserva.FechaInicioEstadia)
            return "No se puede cancelar una reserva ya iniciada.";

        var reservasAlojamiento = _reservasRepositorio.ObtenerReservasPorAlojamientoId(reserva.IdAlojamiento);

        var reservasEnFecha = reservasAlojamiento
            .Where(r =>
                DateTime.Now < r.FechaFinEstadia &&
                r.FechaInicioEstadia < reserva.FechaFinEstadia &&
                r.FechaFinEstadia > reserva.FechaInicioEstadia)
            .Count();

        double montoReembolso = reservasEnFecha > 5
            ? reserva.MontoEstadia * 0.5
            : reserva.MontoEstadia;

        _reservasRepositorio.Eliminar(reserva);

        return $"Reserva cancelada. Reembolso: ${montoReembolso}";
    }
}