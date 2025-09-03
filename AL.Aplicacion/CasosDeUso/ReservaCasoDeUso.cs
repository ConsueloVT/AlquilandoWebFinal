using System;
using AL.Aplicacion.Interfaces;

namespace AL.Aplicacion.CasosDeUso;

public abstract class ReservaCasoDeUso
{
    protected IReservasRepositorio Repositorio { get;private set;  } 
    public ReservaCasoDeUso(IReservasRepositorio repositorio)
   {
       this.Repositorio = repositorio;
   }
}
