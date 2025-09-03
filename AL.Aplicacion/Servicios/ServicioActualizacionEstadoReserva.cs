using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Enumerativos;
namespace AL.Aplicacion.Servicios;

public class ServicioActualizacionEstadoReserva(IReservasRepositorio repositorio) : IServicioActualizacionEstadoReserva
{
    public void ActualizarEstadoReserva(Reserva reserva)
    {
        if (reserva.EstadoReserva== EstadoReserva.Confirmada && reserva.FechaInicioEstadia <= DateTime.Now)
        {
            reserva.EstadoReserva = EstadoReserva.EnCurso;
        }
        else if (reserva.EstadoReserva== EstadoReserva.EnCurso && reserva.FechaFinEstadia <= DateTime.Now)
        {
            reserva.EstadoReserva = EstadoReserva.Finalizada;
        }
        repositorio.Modificar(reserva);   
    }
}
