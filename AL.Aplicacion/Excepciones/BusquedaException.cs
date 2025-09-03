using System;

namespace AL.Aplicacion.Excepciones;

public class BusquedaException : Exception
{
    public BusquedaException(string ciudad, DateTime desde, DateTime hasta)
    : base($"No hay alojamientos disponibles en {ciudad} entre {desde:dd/MM/yyyy} y {hasta:dd/MM/yyyy}."){}
}
