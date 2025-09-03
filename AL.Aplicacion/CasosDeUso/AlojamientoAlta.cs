using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Excepciones;
using AL.Aplicacion.Enumerativos;
namespace AL.Aplicacion.CasosDeUso;

public class AlojamientoAlta(IAlojamientoRepositorio _alojamientoRepo, IAlojamientoValidador _validador): AlojamientoCasoDeUso(_alojamientoRepo)
{
    public void Ejecutar(Alojamiento alojamiento, RolUsuario rol){

        if(rol == RolUsuario.Administrador){
            string mensajeError;
            
            if(_validador.Validar(alojamiento, out mensajeError)){
        
                Repositorio.Agregar(alojamiento);
            }
            else{
                throw new ValidacionException(mensajeError);
            }
        }   
        else{
            throw new AutorizacionException();
        }
        
    }
}
