using System;
using AL.Aplicacion.Entidades;
namespace AL.Aplicacion.Validadores;

public static class TarjetaValidador
{
    public static bool Validar(Tarjeta t, out String mensajeError)
    {
        mensajeError = "";
        if (string.IsNullOrWhiteSpace(t.Numero) || t.Numero.Length != 16 || t.Numero.Any(c => !char.IsDigit(c)))
        {
            mensajeError += "El número de tarjeta debe tener 16 dígitos y solo contener números.\n";
        }
        if (String.IsNullOrWhiteSpace(t.NombreTitular))
        {
            mensajeError += "El nombre del titular no puede estar vacío.\n";
        }
        int auxM= 0;
        int auxY = 0;

        if (String.IsNullOrWhiteSpace(t.FechaVencimiento))
        {
            mensajeError += "La fecha de vencimiento debe ser una fecha futura válida.\n";
        }
        else
        {
            auxM = int.Parse(t.FechaVencimiento.Substring(0, 2));
            auxY = int.Parse(t.FechaVencimiento.Substring(3, 2));
            if (auxY <= DateTime.Now.Year % 100 && auxM <= DateTime.Now.Month)
            {
                mensajeError += "La fecha de vencimiento debe ser una fecha futura válida.\n";
            }
            else if (auxY < DateTime.Now.Year % 100 || auxM < 1 || auxM > 12)
            {
                mensajeError += "La fecha de vencimiento debe ser una fecha futura válida.\n";
            }
        }
        
        if (t.CodigoSeguridad.ToString().Length != 3 || t.CodigoSeguridad < 0)
        {
            mensajeError += "El código de seguridad debe tener 3 dígitos.\n";
        }
        return (mensajeError == "");    
    }
    

}
