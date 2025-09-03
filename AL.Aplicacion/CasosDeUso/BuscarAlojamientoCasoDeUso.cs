using System;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
namespace AL.Aplicacion.CasosDeUso;

public class BuscarAlojamientoCasoDeUso
{
    protected IAlojamientoRepositorio Repositorio { get; private set; }

    public BuscarAlojamientoCasoDeUso(IAlojamientoRepositorio repositorio)
    {
        this.Repositorio = repositorio;
    }

    public List<Alojamiento> Ejecutar(string ciudad, DateTime fechaDesde, DateTime fechaHasta){
    
        if (fechaDesde > fechaHasta) throw new ArgumentException("La fecha de inicio no puede ser posterior a la de fin.");

        List<Alojamiento> resultados = null!;
        resultados = Repositorio.ObtenerPorCiudadYDisponibilidad(ciudad, fechaDesde, fechaHasta);

        /*if (resultados == null || resultados.Count == 0)
            throw new BusquedaException(ciudad, fechaDesde, fechaHasta);*/

        return resultados;
    }
        
}
