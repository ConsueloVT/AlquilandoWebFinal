using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Enumerativos;
using AL.Aplicacion.Excepciones;
namespace AL.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{

    //Verificar que el usuario no exista
    public bool BuscarPorCorreoElectronico(String correo)
    {
        using (var db = new EntidadesContext())
        {
            var usuario = db.Usuarios.Where(u => u.CorreoElectronico.Equals(correo)).SingleOrDefault();
            if (usuario != null)
            {
                return true;
            }
            return false;
        }
    }

    //Agregar un usuario
    public int Agregar(Usuario u)
    {
        using (var db = new EntidadesContext())
        {
            db.Usuarios.Add(u);
            db.SaveChanges();
            return u.Id;
        }
    }

    public int AsignarRol(int id, RolUsuario rol)
    {
        using (var db = new EntidadesContext())
        {
            Usuario? usuario = db.Usuarios.Where(x => x.Id == id).SingleOrDefault();
            if (usuario != null)
            {
                usuario.Rol = rol;
                db.Usuarios.Update(usuario);
                db.SaveChanges();
                return usuario.Id;
            }
            return 0;
        }
    }

    //Dar de baja un usuario
    public void Eliminar(Usuario u)
    {
        using (var db = new EntidadesContext())
        {
            db.Usuarios.Remove(u);
            db.SaveChanges();
        }
    }

    public List<Usuario> ListarTodos()
    {
        using (var db = new EntidadesContext())
        {
            return db.Usuarios.ToList();
        }
    }

    public void ModificarTarjeta(Usuario u)
    {
        using (var db = new EntidadesContext())
        {
            db.Usuarios.Update(u);
            db.SaveChanges();
        }
    }

    public Usuario? ObtenerPorId(int idUsuario)
    {
        using (var db = new EntidadesContext())
        {
            Usuario? usuario = db.Usuarios.Where(x => x.Id == idUsuario).SingleOrDefault();
            return usuario;
        }
    }

    //Para iniciar Sesion
    public Usuario? IniciarSesion(String correo)
    {
        using (var db = new EntidadesContext())
        {
            Usuario? usuario = db.Usuarios
                .Where(x => x.CorreoElectronico == correo)
                .SingleOrDefault();

            if (usuario != null && !usuario.EstaHabilitado)
            {
                throw new UsuarioDeshabilitadoException(correo);
            }

            return usuario;
        }
    }

    public bool tieneReservasSolapadas(DateTime fechaDesde, DateTime fechaHasta, int idUsuario)
    {
        using (var db = new EntidadesContext())
        {
            var reservas = db.Reservas
                .Where(r => r.IdUsuario == idUsuario)
                .ToList();

            return reservas.Any(r =>
                r.FechaInicioEstadia.Date <= fechaHasta.Date &&
                r.FechaFinEstadia.Date >= fechaDesde.Date);
        }
    }
    public void Actualizar(Usuario u)
    {
        using (var db = new EntidadesContext())
        {
            db.Usuarios.Update(u);
            db.SaveChanges();
        }
    }
    public Usuario? ObtenerAdministrador()
    {
        using (var db = new EntidadesContext())
        {
            return db.Usuarios.FirstOrDefault(u => u.Rol == RolUsuario.Administrador);
        }
    }
    public Usuario? ObtenerEncargado()
    {
        using (var db = new EntidadesContext())
        {
            return db.Usuarios.FirstOrDefault(u => u.Rol == RolUsuario.Encargado);
        }

    }
    // Listar usuarios con reservas en los últimos meses
    public List<Usuario> ListarUsuariosConReservasEnUltimosMeses(int cantidadMeses)
    {
        using (var db = new EntidadesContext())
        {
            var fechaLimite = DateTime.Now.AddMonths(-cantidadMeses);

            var usuariosConReservas = db.Reservas
                .Where(r => r.FechaInicioEstadia >= fechaLimite)
                .Select(r => r.IdUsuario)
                .Distinct()
                .ToList();

            var usuarios = db.Usuarios
                .Where(u => usuariosConReservas.Contains(u.Id))
                .ToList();

            return usuarios;
        }
    }
    public async Task AsignarDescuento(int idUsuario, int porcentaje)
    {
        using (var db = new EntidadesContext())
        {
            var usuario = db.Usuarios.SingleOrDefault(u => u.Id == idUsuario);
            if (usuario != null)
            {
                usuario.DescuentoProximaReserva = porcentaje;
                db.Usuarios.Update(usuario);
                await db.SaveChangesAsync();
            }
        }
    }

}
