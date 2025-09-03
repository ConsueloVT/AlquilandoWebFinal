namespace AL.Aplicacion.Entidades;

public class Filtro
{
    public double? PrecioMinimo { get; set; }
    public double? PrecioMaximo { get; set; }
    public int? CantidadDormitorios { get; set; }
    public int? Capacidad { get; set; }
    public bool? Estacionamiento { get; set; }
    public string? Pais { get; set; }
    public string? Ciudad { get; set; }
}