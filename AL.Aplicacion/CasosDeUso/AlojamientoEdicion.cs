using System;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Excepciones;
using AL.Aplicacion.Enumerativos;
namespace AL.Aplicacion.CasosDeUso;


public class AlojamientoEdicion(IAlojamientoRepositorio _repo, IAlojamientoValidadorEdicion _validador) : AlojamientoCasoDeUso(_repo)
{
    public void Ejecutar(Alojamiento alojamiento, RolUsuario rol, String nombreOriginal)
    {
        string mensajeError;
        if (rol != RolUsuario.Administrador)
            throw new AutorizacionException();

        if (!_validador.Validar(alojamiento, out mensajeError, nombreOriginal))
            throw new ValidacionException(mensajeError);

        Repositorio.Modificar(alojamiento);
    }
}