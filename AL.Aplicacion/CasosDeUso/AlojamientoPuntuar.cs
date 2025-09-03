using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Validadores;
using AL.Aplicacion.Excepciones;
using AL.Aplicacion.Entidades;
namespace AL.Aplicacion.CasosDeUso;

public class AlojamientoPuntuar(IAlojamientoRepositorio _alojamientoRepo,IReservasRepositorio _reservaRepo):AlojamientoCasoDeUso(_alojamientoRepo)
{
    public async Task Ejecutar(int IdReserva, int puntuación)
    {
        var reserva = _reservaRepo.ObtenerPorId(IdReserva);
        if (reserva != null)
        {
            if (PuntuarValidador.Validar(reserva, out string mensajeError))
            {
                reserva.Puntuacion = puntuación;
                _reservaRepo.Modificar(reserva);
                Alojamiento? alojamiento = await Repositorio.ObtenerPorId(reserva.IdAlojamiento);
                if (alojamiento != null)
                {
                    alojamiento.Puntuaciones.Add(puntuación);
                    Repositorio.Modificar(alojamiento);
                }
            }
            else
            {
                throw new ValidacionException(mensajeError);
            }    
        }

    }
}
