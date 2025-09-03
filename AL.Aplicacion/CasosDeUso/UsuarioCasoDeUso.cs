using System;
using AL.Aplicacion.Interfaces;
namespace AL.Aplicacion.CasosDeUso;

public abstract class UsuarioCasoDeUso
{
    protected IUsuarioRepositorio Repositorio { get;private set;  } 
    public UsuarioCasoDeUso(IUsuarioRepositorio repositorio)
   {
       this.Repositorio = repositorio;
   }
}
