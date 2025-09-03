using System;
using System.Diagnostics.Contracts;
using AL.Aplicacion.Entidades;

namespace AL.Aplicacion.Interfaces;

public interface IAlojamientoRepositorio
{
    void Agregar(Alojamiento alojamiento);
    void Eliminar(Alojamiento alojamiento);
    void Modificar(Alojamiento alojamiento);
    Alojamiento? ObtenerPorNombre(string nombre);
    List<Alojamiento> ObtenerPorCiudadYDisponibilidad(String ciudad, DateTime fechaDesde, DateTime fechaHasta);
    List<Alojamiento> ObtenerTodos();
    public List<Alojamiento> ListarAlojamientosConSusReservas();
    Task<Alojamiento?> ObtenerPorId(int id);
    public Boolean alojamientoDisponible(int id, DateTime fechaDesde, DateTime fechaHasta);
    void Actualizar(Alojamiento alojamiento);
    public List<Alojamiento> ObtenerPorCiudad(string ciudad);
    public List<Alojamiento> ObtenerPorDisponibilidad(List<Alojamiento> a, DateTime fechaDesde, DateTime fechaHasta);
    public double ObtenerPromedio(Alojamiento a);

}
