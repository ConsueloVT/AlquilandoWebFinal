using System;
namespace AL.Aplicacion.Excepciones
{
    //filtro de excepciones para el caso de uso de filtrar alojamiento
    //en caso de que no se encuentren alojamientos con el filtro proporcionado
    public class FiltroException : Exception
    {
        public FiltroException() : base("No se encontraron alojamientos con el filtro proporcionado.") { }

    }
}
