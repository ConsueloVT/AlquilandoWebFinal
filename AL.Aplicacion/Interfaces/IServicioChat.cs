using AL.Aplicacion.Entidades;
namespace AL.Aplicacion.Interfaces;

public interface IServicioChat
{
    public Task EnviarMensajeAsync(Mensaje m, Reserva r);
    public Task<List<Mensaje>> ObtenerConversacionAsync(int reservaId);
    public Task MarcarComoLeidosAsync(int reservaId, int usuarioId);
    public Task<int> ObtenerCantidadNoLeidosAsync(int usuarioId, int reservaId);
}
