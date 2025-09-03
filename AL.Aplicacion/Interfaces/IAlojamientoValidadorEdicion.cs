using System;
using AL.Aplicacion.Entidades;
namespace AL.Aplicacion.Interfaces;

public interface IAlojamientoValidadorEdicion
{
    public bool Validar(Alojamiento a, out string mensajeError, String? nombreOriginal);
}
