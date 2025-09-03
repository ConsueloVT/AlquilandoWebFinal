using AL.Aplicacion.Enumerativos;
using AL.Aplicacion.Interfaces;

namespace AL.Aplicacion.CasosDeUso;

public class EliminarUsuario
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IReservasRepositorio _reservasRepositorio;

    public EliminarUsuario(IUsuarioRepositorio usuarioRepositorio, IReservasRepositorio reservasRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _reservasRepositorio = reservasRepositorio;
    }

    public bool Ejecutar(int usuarioId, out string mensajeError)
    {
        var usuario = _usuarioRepositorio.ObtenerPorId(usuarioId);

        if (usuario == null || !usuario.EstaHabilitado)
        {
            mensajeError = "Usuario no encontrado o ya deshabilitado.";
            return false;
        }

        var reservas = _reservasRepositorio.ObtenerReservasPorUsuarioId(usuarioId);
        
        var reservasActivas = reservas.Where(r =>
                r.EstadoReserva == EstadoReserva.EnCurso ||
                (
                    r.FechaInicioEstadia >= DateTime.Now &&
                    r.EstadoReserva != EstadoReserva.Rechazada &&
                    r.EstadoReserva != EstadoReserva.Cancelada
                )
            )
            .ToList();

        if (reservasActivas.Any())
        {
            mensajeError = "No se puede eliminar la cuenta con reservas activas o futuras.";
            return false;
        }
        
        // Anonimizar usuario
        usuario.Nombre = "Usuario";
        usuario.Apellido = "No disponible";
        usuario.CorreoElectronico = $"eliminado_{usuario.Id}@anonimo.com";
        usuario.HashContraseña = "";
        usuario.SalContraseña = "";
        usuario.EstaHabilitado = false;

        _usuarioRepositorio.Actualizar(usuario); // actualiza usuario

        foreach (var reserva in reservas)
        {
            _reservasRepositorio.Modificar(reserva); // modifica reservas
        }

        mensajeError = "";
        return true;
    }
}