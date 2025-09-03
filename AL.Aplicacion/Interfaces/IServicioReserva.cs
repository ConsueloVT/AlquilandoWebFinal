using AL.Aplicacion.Entidades;
namespace AL.Aplicacion.Interfaces;

public interface IServicioReserva
{
    public List<Reserva> ObtenerReservasDelUsuario();
    public string CancelarReservaConDemanda(int idReserva);
}
