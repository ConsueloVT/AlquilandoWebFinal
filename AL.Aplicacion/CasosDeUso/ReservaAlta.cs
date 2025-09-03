using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Excepciones;
using AL.Aplicacion.Enumerativos;

namespace AL.Aplicacion.CasosDeUso;

public class ReservaAlta
{
    private readonly IReservasRepositorio _reservasRepositorio;
    private readonly ITarjetaRepositorio _tarjetaRepositorio;
    private readonly IServicioSesion _sesion;
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IAlojamientoRepositorio _alojamientoRepositorio;
    private readonly IServicioPago _servicioPago;


    public ReservaAlta(IReservasRepositorio reservasRepo, ITarjetaRepositorio tarjetaRepo, IServicioSesion sesion, IUsuarioRepositorio usuarioRepo, IServicioPago servicioPago, IAlojamientoRepositorio alojamientoRepo)
    {
        _reservasRepositorio = reservasRepo;
        _tarjetaRepositorio = tarjetaRepo;
        _sesion = sesion;
        _usuarioRepositorio = usuarioRepo;
        _servicioPago = servicioPago;
        _alojamientoRepositorio = alojamientoRepo;
    }
    public async Task <string> Ejecutar(Reserva reserva)
    {
        var usuario = _usuarioRepositorio.ObtenerPorId(_sesion.Id);

        if (usuario == null)
            throw new Exception("Inicie Sesion");

        var alojamiento = await _alojamientoRepositorio.ObtenerPorId(reserva.IdAlojamiento);
        if (alojamiento == null)
            throw new ValidacionException("El alojamiento no existe");
        
        int totalPersonas = reserva.CantidadDeAdultos ?? 0 + reserva.CantidadDeNiños ?? 0;
        if (totalPersonas > alojamiento.CapacidadMaxima)
            throw new ValidacionException($"La cantidad de personas ({totalPersonas}) excede la capacidad permitida ({alojamiento.CapacidadMaxima}).");
            
        var tarjeta = _tarjetaRepositorio.ObtenerPorId(usuario.TarjetaId);
        if (tarjeta == null)
            throw new ValidacionException("No se encontró la tarjeta del usuario");

        bool pagoExitoso = _servicioPago.ValidarPago(tarjeta);
        if (!pagoExitoso)
            return "Reserva rechazada por error en el pago";
        if (alojamiento.TieneInformacionAdicional)
        {
            reserva.EstadoReserva = EstadoReserva.Pendiente;
            
            _reservasRepositorio.Agregar(reserva);
            return "La solicitud de reserva fue enviada";
        }
        else
        {
            reserva.EstadoReserva = EstadoReserva.Confirmada;
            _reservasRepositorio.Agregar(reserva);
            return "Reserva exitosa";
        }

    }
}
