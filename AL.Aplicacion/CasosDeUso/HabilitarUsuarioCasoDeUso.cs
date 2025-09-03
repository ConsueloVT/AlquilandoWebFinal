using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Excepciones;
namespace AL.Aplicacion.CasosDeUso;


public class HabilitarUsuarioCasoDeUso
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public HabilitarUsuarioCasoDeUso(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }

    public void Ejecutar(int usuarioId)
    {
        var usuario = _usuarioRepositorio.ObtenerPorId(usuarioId);
        if (usuario == null)
            throw new UsuarioNoEncontradoException();

        usuario.EstaHabilitado = true;
        _usuarioRepositorio.Actualizar(usuario);
    }
}
