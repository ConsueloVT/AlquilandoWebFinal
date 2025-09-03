using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Excepciones;
using AL.Aplicacion.Enumerativos;
namespace AL.Aplicacion.CasosDeUso;
public class GestionarReservasCasoDeUso
{
    private readonly IReservasRepositorio _reservasRepositorio;

    public GestionarReservasCasoDeUso(IReservasRepositorio reservasRepositorio)
    {
        _reservasRepositorio = reservasRepositorio;
    }

    public List<Reserva> ObtenerTodas()
    {
        return _reservasRepositorio.ObtenerTodas();
    }

    public void AceptarReserva(int idReserva)
    {
        var reserva = _reservasRepositorio.ObtenerPorId(idReserva);
        if (reserva != null && reserva.EstadoReserva == EstadoReserva.Pendiente)
        {
            reserva.EstadoReserva = EstadoReserva.Confirmada;
            _reservasRepositorio.Modificar(reserva);
        }
    }

    public void RechazarReserva(int idReserva)
    {
        var reserva = _reservasRepositorio.ObtenerPorId(idReserva);
        if (reserva != null && reserva.EstadoReserva == EstadoReserva.Pendiente)
        {
            reserva.EstadoReserva = EstadoReserva.Rechazada;
            _reservasRepositorio.Modificar(reserva);
        }
    }
}