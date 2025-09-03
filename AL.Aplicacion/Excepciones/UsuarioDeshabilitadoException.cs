namespace AL.Aplicacion.Excepciones;

public class UsuarioDeshabilitadoException : Exception
{
    public UsuarioDeshabilitadoException(string email)
        : base($"El usuario con el correo '{email}' ha sido deshabilitado hasta nuevo aviso.")
    { }
}