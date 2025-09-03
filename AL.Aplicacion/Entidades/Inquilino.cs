using System.ComponentModel.DataAnnotations.Schema;
namespace AL.Aplicacion.Entidades;

[NotMapped]
public class Inquilino
{
    public string Nombre { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
}