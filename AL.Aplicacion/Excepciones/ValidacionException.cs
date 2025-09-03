using System;

namespace AL.Aplicacion.Excepciones;

public class ValidacionException: Exception
{
   public ValidacionException(string message) : base(message) { }
}
