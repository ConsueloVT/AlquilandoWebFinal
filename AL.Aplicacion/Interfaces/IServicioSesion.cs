using System;
using AL.Aplicacion.Enumerativos;
using AL.Aplicacion.Entidades;
namespace AL.Aplicacion.Interfaces;

public interface IServicioSesion
{
    public int Id { get; set; }
    public RolUsuario Rol { get; set; }
    public event Action? OnChange;
    public bool SesionIniciada { get; set; }
    public Usuario? Usuario { get; set; }
    public Task<bool> Loggin(string email, string contraseña);
    public Task LogoutAsync();
    public Task InicializarUsuarioAsync();
    public void IniciarSesion();
    public Task RefrescarUsuarioAsync();
    Task<bool> ExisteUsuarioConEmail(string email);

}