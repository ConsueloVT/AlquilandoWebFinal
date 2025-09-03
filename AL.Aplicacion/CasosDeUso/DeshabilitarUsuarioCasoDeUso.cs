using AL.Aplicacion.Entidades;
using AL.Aplicacion.Excepciones;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Enumerativos;
namespace AL.Aplicacion.CasosDeUso;

public class DeshabilitarUsuarioCasoDeUso
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IReservasRepositorio _reservaRepositorio;

    public DeshabilitarUsuarioCasoDeUso(
        IUsuarioRepositorio usuarioRepositorio,
        IReservasRepositorio reservaRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _reservaRepositorio = reservaRepositorio;
    }

    public void Ejecutar(int usuarioId)
    {
        var usuario = _usuarioRepositorio.ObtenerPorId(usuarioId);
        if (usuario == null)
            throw new UsuarioNoEncontradoException();

        var hoy = DateTime.Today;

        var reservas = _reservaRepositorio.ObtenerReservasPorUsuarioId(usuarioId);

        bool tieneEnCurso = reservas.Any(r =>
            r.FechaInicioEstadia.Date <= hoy && hoy <= r.FechaFinEstadia.Date
            && r.EstadoReserva != EstadoReserva.Finalizada);

        if (tieneEnCurso)
            throw new UsuarioConReservaEnCursoException();

        bool tieneFuturas = reservas.Any(r => r.FechaInicioEstadia > hoy);

        if (tieneFuturas) //escenario2
            _reservaRepositorio.CancelarReservasFuturas(usuarioId, hoy);

        usuario.EstaHabilitado = false;
        _usuarioRepositorio.Actualizar(usuario);
    }
}