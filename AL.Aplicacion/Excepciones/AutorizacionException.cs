using System;

namespace AL.Aplicacion.Excepciones;

public class AutorizacionException: Exception
{
public AutorizacionException(): base("El usuario no tiene permiso para realizar esta acción"){}
}
