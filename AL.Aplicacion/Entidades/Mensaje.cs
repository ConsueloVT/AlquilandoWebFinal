using System;

namespace AL.Aplicacion.Entidades;

public class Mensaje
{
    public int Id { get; set; }
    public int IdReserva { get; set; }
    public int IdEmisor { get; set; }
    public int IdReceptor { get; set; }
    public DateTime FechaEnvio { get; set; }
    public String Contenido { get; set; } = string.Empty;
    public bool Leido { get; set; } = false;

}