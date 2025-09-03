using Microsoft.EntityFrameworkCore;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Enumerativos;
namespace AL.Repositorios;

public class EntidadesSqlite
{
    public static void Inicializar()
    {

        using var context = new EntidadesContext();

        if (context.Database.EnsureCreated())
        {
            //context.Usuarios.Add(new Usuario() {Nombre="Admin", Apellido="Admin",,CorreoElectronico="admin@gmail.com",Rol=RolUsuario.Administrador });
            context.Alojamientos.Add(new Alojamiento() { Nombre = "Dpt1", Ciudad = "La Plata", Direccion = "7 y 46", CapacidadMaxima = 4, CantidadDormitorios = 2, CantidadBaños = 1, CantidadCamas = 4, Pais = "Argentina", PrecioPorNoche = 1000, TipoDeReembolso = TipoReembolso.ReembolsoParcial, Servicios = "Wifi, Aire Acondicionado", Estacionamiento = true, Descripcion = "Dpto en pleno centro de la ciudad", Estado = EstadoPublicacion.Publicado });
            context.Alojamientos.Add(new Alojamiento() { Nombre = "Casa de campo", Ciudad = "La Plata", Direccion = "Ruta 2 km 50", CapacidadMaxima = 8, CantidadDormitorios = 4, CantidadBaños = 2, CantidadCamas = 6, Pais = "Argentina", PrecioPorNoche = 2000, TipoDeReembolso = TipoReembolso.ReembolsoTotal, Servicios = "Piscina, Parrilla", Estacionamiento = true, Descripcion = "Casa de campo con piscina y parrilla", Estado = EstadoPublicacion.Publicado });
            context.Alojamientos.Add(new Alojamiento() { Nombre = "Cabaña en la montaña", Ciudad = "Bariloche", Direccion = "Ruta 40 km 2000", CapacidadMaxima = 6, CantidadDormitorios = 3, CantidadBaños = 2, CantidadCamas = 5, Pais = "Argentina", PrecioPorNoche = 3000, TipoDeReembolso = TipoReembolso.NoReembolsable, Servicios = "Vista al lago, Calefacción", Estacionamiento = true, Descripcion = "Cabaña con vista al lago y calefacción", Estado = EstadoPublicacion.Publicado });
            context.Alojamientos.Add(new Alojamiento() { Nombre = "Apartamento en la playa", Ciudad = "Mar del Plata", Direccion = "Avenida Luro 1234", CapacidadMaxima = 4, CantidadDormitorios = 1, CantidadBaños = 1, CantidadCamas = 2, Pais = "Argentina", PrecioPorNoche = 1500, TipoDeReembolso = TipoReembolso.ReembolsoParcial, Servicios = "Vista al mar, Balcón", Estacionamiento = false, Descripcion = "Apartamento con vista al mar y balcón", Estado = EstadoPublicacion.Publicado });
            context.SaveChanges();
        }

        var connection = context.Database.GetDbConnection();
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "PRAGMA journal_mode=DELETE;";
            command.ExecuteNonQuery();
        }
    }
}
