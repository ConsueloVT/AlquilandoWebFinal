using System;
using AL.Aplicacion.Entidades;
namespace AL.Aplicacion.Interfaces;

public interface IUsuarioValidador
{
    public bool Validar(Usuario u, out String mensajeError);
}
