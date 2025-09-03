using System;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Excepciones; 
using AL.Aplicacion.Enumerativos;
using AL.Aplicacion.Validadores;
namespace AL.Aplicacion.CasosDeUso;

public class UsuarioAlta(IUsuarioRepositorio _repositorio, ITarjetaRepositorio _repoTarjeta,IUsuarioValidador _validador): UsuarioCasoDeUso(_repositorio)
{

    public void Ejecutar(Usuario usuario, Tarjeta t)
    {
        String mensajeError;
        
        if (_validador.Validar(usuario, out mensajeError) && TarjetaValidador.Validar(t, out mensajeError))
        {
            usuario.TarjetaId = _repoTarjeta.Agregar(t);
            int id = Repositorio.Agregar(usuario);

            if (id == 1) {
                Repositorio.AsignarRol(usuario.Id, RolUsuario.Administrador);
            }
            else if(usuario.Nombre.ToLower().Contains("encargado")) //Si queremos registrar un encargado poner de nombre "Encargado 1" o "Encargada 1", "Encargada 2", etc.
            {
                Repositorio.AsignarRol(usuario.Id, RolUsuario.Encargado);
            }
            else
            {
                Repositorio.AsignarRol(usuario.Id, RolUsuario.Usuario);
            }    
        }
        else
        {
            throw new ValidacionException(mensajeError);
        }
    }
}
