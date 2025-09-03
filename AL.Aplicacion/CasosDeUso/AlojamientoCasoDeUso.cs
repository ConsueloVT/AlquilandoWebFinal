using System;
using AL.Aplicacion.Interfaces;
namespace AL.Aplicacion.CasosDeUso;

public abstract class AlojamientoCasoDeUso
{
    protected IAlojamientoRepositorio Repositorio { get;private set;  } 
    public AlojamientoCasoDeUso(IAlojamientoRepositorio repositorio)
   {
       this.Repositorio = repositorio;
   }
}
