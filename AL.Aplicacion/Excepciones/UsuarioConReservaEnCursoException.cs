using System;

namespace AL.Aplicacion.Excepciones;

public class UsuarioConReservaEnCursoException : Exception
{
    public UsuarioConReservaEnCursoException()
        : base("No se puede deshabilitar al usuario porque tiene una reserva en curso.")
    {
    }
}
// Esta excepción se lanza cuando se intenta deshabilitar un usuario que tiene una reserva activa.
