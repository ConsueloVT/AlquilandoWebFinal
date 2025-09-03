using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Enumerativos;
namespace AL.Aplicacion.CasosDeUso;

public class ReservaConsultaTodosPorUsuario(IReservasRepositorio repositorio,IServicioChat chat,IServicioActualizacionEstadoReserva servicioActualizarEstado) : ReservaCasoDeUso(repositorio)
{
    public List<Reserva> Ejecutar(int usuarioId)
    {
        List<Reserva> reservas = Repositorio.ObtenerReservasPorUsuarioId(usuarioId);
        foreach (var reserva in reservas)
        {
            reserva.MensajesNoLeidos = chat.ObtenerCantidadNoLeidosAsync(usuarioId, reserva.Id).Result;
            servicioActualizarEstado.ActualizarEstadoReserva(reserva);      
        }
        return reservas;
    }
}
