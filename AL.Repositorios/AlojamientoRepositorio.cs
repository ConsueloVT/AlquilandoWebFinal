using System;
using Microsoft.EntityFrameworkCore;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Enumerativos;
namespace AL.Repositorios;

public class AlojamientoRepositorio : IAlojamientoRepositorio
{
    //Caso de uso alojamiento ALTA
    public void Agregar(Alojamiento e)
    {

        using (var db = new EntidadesContext())
        {
            db.Add(e);
            db.SaveChanges();
        }
    }

    //Caso de uso alojamiento BAJA
    public void Eliminar(Alojamiento e)
    {
        using (var db = new EntidadesContext())
        {
            var expediente = db.Alojamientos.Where(t => t.Id == e.Id).SingleOrDefault();
            if (expediente != null)
            {
                db.Remove(expediente);
                db.SaveChanges();
            }
        }
    }
    //Consulta por Id, para que el administrador pueda ver los detalles de un alojamiento
    public async Task<Alojamiento?> ObtenerPorId(int id)
    {
        using (var db = new EntidadesContext())
        {
            return await db.Alojamientos
            .Include(a => a.Reservas)
            .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
    public Alojamiento? ObtenerPorNombre(string nombre)
    {
        using (var db = new EntidadesContext())
        {
            var alojamiento = db.Alojamientos.Where(a => a.Nombre != null && a.Nombre.Equals(nombre)).SingleOrDefault();
            return alojamiento;
        }
    }

    public Boolean alojamientoDisponible(int id, DateTime fechaDesde, DateTime fechaHasta)
    {
        using (var db = new EntidadesContext())
        {
            var reservas = db.Reservas
                .Where(r => r.IdAlojamiento == id &&
                            r.FechaInicioEstadia.Date <= fechaHasta.Date &&
                            r.FechaFinEstadia.Date >= fechaDesde.Date)
                .ToList();

            return !reservas.Any();
        }
    }
    public List<Alojamiento> ObtenerPorCiudad(String ciudad)
    {
        using (var db = new EntidadesContext())
        {
            return db.Alojamientos.Include(a => a.Reservas)
                .Where(a => a.Ciudad != null && a.Ciudad.ToLower() == ciudad.ToLower())
                .ToList();
        }
    }
    public List<Alojamiento> ObtenerPorDisponibilidad(List<Alojamiento> alojamientos, DateTime fechaDesde, DateTime fechaHasta)
    {
        return alojamientos
            .Where(a => a.Reservas == null || !a.Reservas.Any(r =>
                r.FechaInicioEstadia.Date <= fechaHasta.Date &&
                r.FechaFinEstadia.Date >= fechaDesde.Date))
            .ToList();
    }

    //Creo que este método va a servir para "Buscar Alojamiento" en la UI, depués podríamos configurar para que ignore las tildes
    public List<Alojamiento> ObtenerPorCiudadYDisponibilidad(String ciudad, DateTime fechaDesde, DateTime fechaHasta)
    {
        using (var db = new EntidadesContext())
        {
            var alojamientos = db.Alojamientos
                .Where(a => a.Ciudad != null && a.Ciudad.ToLower() == ciudad.ToLower())
                .ToList();

            var alojamientoIds = alojamientos.Select(a => a.Id).ToList();

            var reservas = db.Reservas
                .Where(r => alojamientoIds.Contains(r.IdAlojamiento) &&
                            r.FechaInicioEstadia <= fechaHasta &&
                            r.FechaFinEstadia >= fechaDesde)
                .ToList();

            var idsNoDisponibles = reservas.Select(r => r.IdAlojamiento).Distinct().ToHashSet();

            return alojamientos.Where(a => !idsNoDisponibles.Contains(a.Id)).ToList();
        }
    }
    public List<Alojamiento> ListarAlojamientosConSusReservas()
    {
        using (var db = new EntidadesContext())
        {
            return db.Alojamientos.Include(a => a.Reservas).ToList();
        }
    }


    //Caso de uso consulta TODOS
    public List<Alojamiento> ObtenerTodos()
    {
        using (var db = new EntidadesContext())
        {
            List<Alojamiento> resultado = db.Alojamientos.ToList();
            return resultado;
        }
    }
    public double ObtenerPromedio(Alojamiento a)
    {
        return a.Puntuaciones.Count > 0 ? a.Puntuaciones.Average() : 0.0;
    }

    //Caso de uso alojamiento MODIFICACION
    public void Modificar(Alojamiento e)
    {
        using (var db = new EntidadesContext())
        {
            db.Alojamientos.Update(e);
            db.SaveChanges();
        }
    }
    public void Actualizar(Alojamiento alojamiento)
    {
        using (var db = new EntidadesContext())
        {
            db.Alojamientos.Update(alojamiento);
            db.SaveChanges();
        }
    }
    public List<Alojamiento> ObtenerPublicados()
    {
        using (var db = new EntidadesContext())
        {
            return db.Alojamientos
                .Where(a => a.Estado == EstadoPublicacion.Publicado)
                .ToList();
        }
    }
}
