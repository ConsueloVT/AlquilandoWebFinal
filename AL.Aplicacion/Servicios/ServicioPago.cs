using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;

namespace AL.Aplicacion.Servicios
{
    public class ServicioPago : IServicioPago
    {
        public bool ValidarPago(Tarjeta tarjeta)
        {
            if (tarjeta == null || string.IsNullOrWhiteSpace(tarjeta.Numero))
            {
                return false;
            }
            
            //Verifica fecha de vencimiento

            int auxM = int.Parse(tarjeta.FechaVencimiento.Substring(0, 2));
            int auxY = int.Parse(tarjeta.FechaVencimiento.Substring(3, 2));
            if (auxY <= DateTime.Now.Year % 100 && auxM <= DateTime.Now.Month)
            {
                return false;
            }
            else if (auxY < DateTime.Now.Year % 100 || auxM < 1 || auxM > 12)
            {
               return false;
            }
            //Simulación de saldo suficiente: último dígito par -> con saldo)
            char ultimoCaracter = tarjeta.Numero.Last();
            if (char.IsDigit(ultimoCaracter))
            {
                int ultimoDigito = int.Parse(ultimoCaracter.ToString());
                return ultimoDigito % 2 == 0;
            }
            return false;
        }
    }
}