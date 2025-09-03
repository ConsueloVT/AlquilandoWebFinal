using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;

namespace AL.Aplicacion.Validadores;

public static class PuntuarValidador
{
    public static bool Validar(Reserva r,out String mensajeError)
    {
        mensajeError = "";
        if (r.Puntuacion > 0 && r.Puntuacion <= 5)
        {
            mensajeError += "No se puede modificar la puntuación previa del alojamiento\n";
        }
        if (r.FechaInicioEstadia > DateTime.Now)
        {
            mensajeError += "El usuario debe haberse alojado previamente\n";
        }
        //Para poder probar lo voy a dejar en 1 dia despues
        if ((DateTime.Now - r.FechaFinEstadia).TotalDays > 15)
        {
            mensajeError += "Plazo para puntuar el alojamiento vencido\n";
        }
        return (mensajeError == "");
    }
}
