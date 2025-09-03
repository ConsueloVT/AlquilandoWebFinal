using System;
using Microsoft.VisualBasic;

namespace AL.Aplicacion.Entidades;
//Después podriamos cifrar la información de la tarjeta
public class Tarjeta
{
    public int Id { get; set; }
    public String Numero { get; set; } = String.Empty;
    public String? NombreTitular { get; set; }
    public String? FechaVencimiento { get; set; } 
    public int CodigoSeguridad { get; set; }
}
