using System;
using AL.Aplicacion.Enumerativos;
namespace AL.Aplicacion.Entidades;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public DateOnly FechaDeNacimiento { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public String CorreoElectronico { get; set; } = "";
    public int TarjetaId { get; set; }
    public RolUsuario Rol { get; set; }
    public string HashContraseña { get; set; } = "";
    public string SalContraseña { get; set; } = "";
    public List<Reserva> ListaReservas { get; set; } = new List<Reserva>();
    public bool EstaHabilitado { get; set; } = true;
    public int DescuentoProximaReserva { get; set; } = 0;
}
