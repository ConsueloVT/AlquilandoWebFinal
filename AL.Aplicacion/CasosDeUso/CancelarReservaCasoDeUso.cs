using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Servicios;

namespace AL.Aplicacion.CasosDeUso;

public class CancelarReservaCasoDeUso
{
    private readonly IServicioReserva _servicioReserva;

    public CancelarReservaCasoDeUso(IServicioReserva servicioReserva)
    {
        _servicioReserva = servicioReserva;
    }

    public List<Reserva> ObtenerMisReservas()
    {
        return _servicioReserva.ObtenerReservasDelUsuario();
    }

    public string CancelarReserva(int idReserva)
    {
        return _servicioReserva.CancelarReservaConDemanda(idReserva);
    }
}
