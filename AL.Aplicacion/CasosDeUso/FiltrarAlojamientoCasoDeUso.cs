using System;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Excepciones;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Enumerativos;

namespace AL.Aplicacion.CasosDeUso;

public class FiltrarAlojamientoCasoDeUso
{
    protected IAlojamientoRepositorio Repositorio { get; private set; }

    public FiltrarAlojamientoCasoDeUso(IAlojamientoRepositorio repositorio)
    {
        this.Repositorio = repositorio;
    }

    public List<Alojamiento> Ejecutar(Filtro filtros,List<Alojamiento> a)
    {
        var alojamientos = a.AsQueryable();

        if (filtros.PrecioMinimo.HasValue)
            alojamientos = alojamientos.Where(a => a.PrecioPorNoche >= filtros.PrecioMinimo.Value);

        if (filtros.PrecioMaximo.HasValue)
            alojamientos = alojamientos.Where(a => a.PrecioPorNoche <= filtros.PrecioMaximo.Value);

        if (filtros.CantidadDormitorios.HasValue)
            alojamientos = alojamientos.Where(a => a.CantidadDormitorios == filtros.CantidadDormitorios.Value);

        if (filtros.Capacidad.HasValue)
            alojamientos = alojamientos.Where(a => a.CapacidadMaxima == filtros.Capacidad.Value);

        if (filtros.Estacionamiento.HasValue)
            alojamientos = alojamientos.Where(a => a.Estacionamiento == filtros.Estacionamiento.Value);

        if (!string.IsNullOrWhiteSpace(filtros.Pais))
            alojamientos = alojamientos.Where(a => !string.IsNullOrWhiteSpace(a.Pais) && a.Pais!.Equals(filtros.Pais, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filtros.Ciudad))
            alojamientos = alojamientos.Where(a => !string.IsNullOrWhiteSpace(a.Ciudad) && a.Ciudad!.Equals(filtros.Ciudad, StringComparison.OrdinalIgnoreCase));

        var resultado = alojamientos.ToList();

        if (!resultado.Any())
            throw new FiltroException();

        return resultado;
    }
}
