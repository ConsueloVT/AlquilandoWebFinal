using System;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
namespace AL.Aplicacion.Validadores;

public class UsuarioValidador(IUsuarioRepositorio _usuarioRepo): IUsuarioValidador
{
    public bool Validar(Usuario u, out String mensajeError)
    {
        mensajeError = "";
        if(_usuarioRepo.BuscarPorCorreoElectronico(u.CorreoElectronico)){ 
            mensajeError+="El correo electronico se encuentra en uso.<br />";
        }
        if (u.FechaDeNacimiento.Year > DateTime.Now.Year - 18)
        {
            mensajeError +="El usuario debe ser mayor de edad.<br />";
        }
        if (string.IsNullOrWhiteSpace(u.Nombre))
        {
            mensajeError += "La casilla de Nombre no puede estar vacía.<br />";
        }
        if (string.IsNullOrWhiteSpace(u.Apellido)){
            mensajeError += "La casilla de Apellido no puede estar vacía.<br />";
        }
        if (string.IsNullOrWhiteSpace(u.CorreoElectronico)){
            mensajeError += "El correo electrónico no puede estar vacío.<br />";
        }
        if (!u.CorreoElectronico.Contains("@"))
        {
            mensajeError += "El correo electrónico debe contener un '@'.<br />";
        }
        return (mensajeError == "");
    }   
}
