using AL.Aplicacion.Entidades;
namespace AL.Aplicacion.Interfaces;

public interface IServicioPago
{
    bool ValidarPago(Tarjeta tarjeta);
}
