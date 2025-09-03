using System;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
namespace AL.Aplicacion.Validadores;

public  class AlojamientoValidadorEdicion(IAlojamientoRepositorio _alojamientoRepo):IAlojamientoValidadorEdicion
{

    public  bool Validar(Alojamiento a, out string mensajeError, String? nombreOriginal)
    {
        mensajeError = "";
        var alojamientoExistente = _alojamientoRepo.ObtenerPorNombre(a.Nombre);
        if (alojamientoExistente != null && !alojamientoExistente.Nombre.Equals(nombreOriginal))
        {
            mensajeError += "No ha sido posible guardar los cambios porque el alojamiento ya se encuentra registrado.<br />";
        }
        if (string.IsNullOrWhiteSpace(a.Nombre))
        {
            mensajeError += "El nombre no puede estar vacio.\n";
        }
        if (string.IsNullOrWhiteSpace(a.Ciudad))
        {
            mensajeError += "La ciudad no puede estar vacia.\n";
        }
        if (string.IsNullOrWhiteSpace(a.Direccion))
        {
            mensajeError += "Debe ingresar la dirección.\n";
        }
        if (a.CapacidadMaxima <= 0)
        {
            mensajeError += "Debe ingresar la capacidad máxima.\n";
        }
        if (a.CantidadDormitorios <= 0)
        {
            mensajeError += "Debe ingresar la cantidad de dormitorios.\n";
        }
        if (a.CantidadBaños <= 0)
        {
            mensajeError += "Debe ingresar la cantidad de baños.\n";
        }
        if (a.CantidadCamas <= 0)
        {
            mensajeError += "Debe ingresar la cantidad de camas.\n";
        }
        if (a.PrecioPorNoche < 0)
        {
            mensajeError += "Debe ingresar el precio por noche del alojamiento.\n";
        }
        return (mensajeError == "");
    }
}
