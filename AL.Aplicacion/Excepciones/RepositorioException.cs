using System;

namespace AL.Aplicacion.Excepciones;

public class RepositorioException: Exception
{
    //No se so lo vamos a necesitar lo dejo por si acaso, tal vez sirva para los
    //casos de modificar alojamientos
    public RepositorioException(string message) : base(message) { }
}
