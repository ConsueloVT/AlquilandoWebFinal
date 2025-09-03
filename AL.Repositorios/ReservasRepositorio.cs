using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
namespace AL.Repositorios;

public class ReservasRepositorio : IReservasRepositorio
{
    //Caso de uso reserva ALTA
    public void Agregar(Reserva r)
    {

        using (var db = new EntidadesContext())
        {
            db.Add(r);
            db.SaveChanges();
        }
    }

    //Caso de uso reserva BAJA
    public void Eliminar(Reserva r)
    {
        using (var db = new EntidadesContext())
        {
            var reserva = db.Reservas.Where(re => re.Id == r.Id).SingleOrDefault();
            if (reserva != null)
            {
                db.Remove(reserva);
                db.SaveChanges();
            }
        }
    }

    //Caso de uso Consulta por Id
    public Reserva? ObtenerPorId(int id)
    {
        using (var db = new EntidadesContext())
        {
            var reservas = db.Reservas.Where(r => r.Id == id).Include(r => r.Chat).SingleOrDefault();
            return reservas;
        }
    }

    //Caso de uso consulta TODOS
    public List<Reserva> ObtenerTodos()
    {
        using (var db = new EntidadesContext())
        {
            List<Reserva> resultado = db.Reservas.ToList();
            return resultado;
        }
    }
    public List<Reserva> ObtenerReservasPorAlojamientoId(int alojamientoId)
    {
        using (var db = new EntidadesContext())
        {
            return db.Reservas.Where(r => r.IdAlojamiento == alojamientoId).ToList();
        }
    }
    public List<Reserva> ObtenerReservasPorUsuarioId(int usuarioId)
    {
        using (var db = new EntidadesContext())
        {
            List<Reserva> reservas = db.Reservas
                .Include(r => r.Chat) // Incluye el chat si es necesario
                .Where(r => r.IdUsuario == usuarioId)
                .ToList();
            /*foreach (var reserva in reservas)
            {
                reserva.MensajesNoLeidos = ObtenerCantidadNoLeidosAsync(usuarioId, reserva.Id).Result;
            }*/
            return reservas;
        }
    }

    //Caso de uso reserva MODIFICACION (puede que no se use)
    public void Modificar(Reserva r)
    {
        using (var db = new EntidadesContext())
        {
            db.Reservas.Update(r);
            db.SaveChanges();
        }
    }
    public void CancelarReservasFuturas(int usuarioId, DateTime fechaDesde)
    {
        using (var db = new EntidadesContext())
        {
            var futuras = db.Reservas
                .Where(r => r.IdUsuario == usuarioId && r.FechaInicioEstadia > fechaDesde)
                .ToList();

            db.Reservas.RemoveRange(futuras);
            db.SaveChanges();
        }
    }
    public bool TieneReservaEnCurso(int alojamientoId)
    {
        using (var db = new EntidadesContext())
        {
            var hoy = DateTime.Today;
            return db.Reservas.Any(r =>
                r.IdAlojamiento == alojamientoId &&
                r.FechaInicioEstadia <= hoy &&
                r.FechaFinEstadia >= hoy);
        }
    }
    public bool TieneReservasFuturas(int alojamientoId)
    {
        using (var db = new EntidadesContext())
        {
            var hoy = DateTime.Today;
            return db.Reservas.Any(r =>
                r.IdAlojamiento == alojamientoId &&
                r.FechaInicioEstadia > hoy);
        }
    }
    public void CancelarReservasFuturasPorAlojamiento(int alojamientoId)
    {
        using (var db = new EntidadesContext())
        {
            var hoy = DateTime.Today;
            var futuras = db.Reservas
                .Where(r => r.IdAlojamiento == alojamientoId && r.FechaInicioEstadia > hoy)
                .ToList();

            db.Reservas.RemoveRange(futuras);
            db.SaveChanges();
        }
    }

    public async Task<List<Mensaje>> ObtenerConversacionAsync(int reservaId)
    {
        using (var db = new EntidadesContext())
        {
            return await db.Mensajes
                .Where(m => m.IdReserva == reservaId)
                .OrderBy(m => m.FechaEnvio)
                .ToListAsync();
        }
    }
    public async Task EnviarMensajeAsync(Mensaje m, Reserva r)
    {
        using (var db = new EntidadesContext())
        {
            db.Mensajes.Add(m); // Update the reservation with the new message
            await db.SaveChangesAsync();
        }
    }

    public async Task MarcarComoLeidosAsync(int reservaId, int usuarioId)
    {
        using (var db = new EntidadesContext())
        {
            var mensajes = await db.Mensajes
            .Where(m => m.IdReserva == reservaId && m.IdReceptor == usuarioId && !m.Leido)
            .ToListAsync();

            if (mensajes.Any())
            {
                foreach (var m in mensajes)
                {
                    m.Leido = true;
                    db.Mensajes.Update(m);
                }
                await db.SaveChangesAsync();
            }
        }
    }
    public async Task<int> ObtenerCantidadNoLeidosAsync(int usuarioId, int reservaId)
    {
        using (var db = new EntidadesContext())
        {
            return await db.Mensajes
            .Where(m => m.IdReceptor == usuarioId && m.IdReserva == reservaId && !m.Leido)
            .CountAsync();
        }
    }
    public List<Reserva> ObtenerTodas()
    {
        using (var db = new EntidadesContext())
        {
            List<Reserva> resultado = db.Reservas.ToList();
            foreach (var res in resultado)
            {
                if (!string.IsNullOrWhiteSpace(res.InformacionInquilinos))
                {
                    try
                    {
                        res.Inquilinos = JsonSerializer.Deserialize<List<Inquilino>>(res.InformacionInquilinos) ?? new();
                    }
                    catch
                    {
                        res.Inquilinos = new();
                    }
                }
            }
            return resultado;
        }
    }
    public bool TieneReservasFuturasPorUsuario(int usuarioId)
    {
        using (var db = new EntidadesContext())
        {
            var hoy = DateTime.Today;
            return db.Reservas.Any(r =>
                r.IdUsuario == usuarioId &&
                r.FechaInicioEstadia > hoy);
        }
    }
    
}
