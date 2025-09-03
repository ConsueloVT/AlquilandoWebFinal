using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Enumerativos;
namespace AL.Aplicacion.CasosDeUso;

public class ReservaConsultaTodos(IReservasRepositorio reservasRepo,IServicioChat ServicioChat,IServicioActualizacionEstadoReserva servicioActualizarEstado) : ReservaCasoDeUso(reservasRepo)
{
    public List<Reserva> Ejecutar(int idUsuario)
    {
        List<Reserva> reservas = Repositorio.ObtenerTodos();
        foreach (var reserva in reservas)
        {
            reserva.MensajesNoLeidos = ServicioChat.ObtenerCantidadNoLeidosAsync(idUsuario, reserva.Id).Result;

            servicioActualizarEstado.ActualizarEstadoReserva(reserva);   
        }
        return reservas;
    }
}
