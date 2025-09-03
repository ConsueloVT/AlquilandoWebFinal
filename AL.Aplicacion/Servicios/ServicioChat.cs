using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
namespace AL.Aplicacion.Servicios;

public class ServicioChat(IReservasRepositorio reservasRepo):IServicioChat
{

    public async Task EnviarMensajeAsync(Mensaje m, Reserva r)
    {
        await reservasRepo.EnviarMensajeAsync(m,r);
    }

    public async Task<List<Mensaje>> ObtenerConversacionAsync(int reservaId)
    {
        return await reservasRepo.ObtenerConversacionAsync(reservaId);
    }
    public async Task MarcarComoLeidosAsync(int reservaId, int usuarioId)
    {
        await reservasRepo.MarcarComoLeidosAsync(reservaId, usuarioId);
    }
    public async Task<int> ObtenerCantidadNoLeidosAsync(int usuarioId, int reservaId) // reservaId is optional, defaulting to 0
    {
        return await reservasRepo.ObtenerCantidadNoLeidosAsync(usuarioId, reservaId); // Assuming 0 is a placeholder for reservaId
    }
 
}
