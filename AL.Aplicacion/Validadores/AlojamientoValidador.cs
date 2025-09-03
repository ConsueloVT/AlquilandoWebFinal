using System;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Enumerativos;
namespace AL.Aplicacion.Validadores;

public class AlojamientoValidador(IAlojamientoRepositorio _alojamientoRepo):IAlojamientoValidador
{
    public bool Validar(Alojamiento a, out string mensajeError){
        mensajeError = "";
        if (_alojamientoRepo.ObtenerPorNombre(a.Nombre) != null)
        {
            mensajeError += "El alojamiento ingresado ya se encuentra registrado.<br />";
        }
        if (a.TipoDeReembolso == null || (a.TipoDeReembolso != TipoReembolso.NoReembolsable && 
            a.TipoDeReembolso != TipoReembolso.ReembolsoTotal && 
            a.TipoDeReembolso != TipoReembolso.ReembolsoParcial))
        {
            mensajeError += "Debe seleccionar un tipo de reembolso.<br />"; 
        }
        return (mensajeError == "");
    }
}
