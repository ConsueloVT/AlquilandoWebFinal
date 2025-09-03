using System;
using AL.Aplicacion.Entidades;
namespace AL.Aplicacion.Interfaces;

public interface IReservasRepositorio
{

    void Agregar(Reserva reserva);
    void Eliminar(Reserva reserva);
    void Modificar(Reserva reserva);
    Reserva? ObtenerPorId(int id);
    List<Reserva> ObtenerTodos();
    List<Reserva> ObtenerReservasPorAlojamientoId(int alojamientoId);
    List<Reserva> ObtenerReservasPorUsuarioId(int usuarioId);
    void CancelarReservasFuturas(int usuarioId, DateTime fechaDesde);
    bool TieneReservaEnCurso(int alojamientoId);
    bool TieneReservasFuturas(int alojamientoId);
    void CancelarReservasFuturasPorAlojamiento(int alojamientoId);
    Task<List<Mensaje>> ObtenerConversacionAsync(int reservaId);
    Task EnviarMensajeAsync(Mensaje m, Reserva r);
    Task MarcarComoLeidosAsync(int reservaId, int usuarioId);
    Task<int> ObtenerCantidadNoLeidosAsync(int usuarioId, int reservaId);
    List<Reserva> ObtenerTodas();
    bool TieneReservasFuturasPorUsuario(int usuarioId);
}
