using System;
using AL.Aplicacion.Entidades;

namespace AL.Aplicacion.Interfaces;

public interface ITarjetaRepositorio
{
    public int Agregar(Tarjeta t);
    public String ObtenerPorIdUltimosCuatro(int id);
    public Tarjeta? ObtenerPorId(int id);
    public void Actualizar(Tarjeta t);
}