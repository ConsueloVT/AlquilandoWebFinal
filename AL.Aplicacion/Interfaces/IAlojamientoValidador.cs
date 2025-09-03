using System;
using AL.Aplicacion.Entidades;

namespace AL.Aplicacion.Interfaces;

public interface IAlojamientoValidador
{
 public bool Validar(Alojamiento a, out String mensajeError);
}
