using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using AL.Aplicacion.Enumerativos;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Entidades;
namespace AL.UI.Servicios;

public class ServicioSesion : IServicioSesion
{
    private readonly IUsuarioRepositorio _repo;
    private readonly IHashService _hash;
    private readonly ProtectedSessionStorage _sessionStorage;



    public ServicioSesion(ProtectedSessionStorage sessionStorage, IUsuarioRepositorio repo, IHashService hash)
    {
        _repo = repo;
        _hash = hash;
        _sessionStorage = sessionStorage;

        Rol = RolUsuario.Invitado;
    }

    public int Id { get; set; }

    public RolUsuario Rol { get; set; }
    public Usuario? Usuario { get; set; } = null!;// Agregado para mantener una referencia al usuario actual
    public event Action? OnChange;
    public bool SesionIniciada { get; set; } = false;

    public async Task<bool> Loggin(String email, String contraseña)
    {
        var result = Validar(email, contraseña);
        if (result)
        {
            await GuardarUsuarioAsync();
        }
        return result;
    }

    private bool Validar(string email, string contraseña)
    {
        var usuario = _repo.IniciarSesion(email);
        if (usuario == null)
        {
            throw new Exception("No hay un usuario registrado con ese correo.");
        }
        //Esto lo agrego para poder demostrar el escenario de usuario deshabilitado(provisional hasta realizar el sprint 2)
        if (usuario.CorreoElectronico.Equals("usuarioD@gmail.com"))
        {
            throw new Exception("El usuario ha sido deshabilitado hasta nuevo aviso.");
        }
        if (_hash.VerifyHash(contraseña, usuario.HashContraseña, usuario.SalContraseña))
        {
            Id = usuario.Id;
            Rol = usuario.Rol;
            Usuario = usuario; // Asigna el usuario actual
            return true;
        }

        throw new Exception("La contraseña es incorrecta.");
    }

    public void IniciarSesion()
    {
        SesionIniciada = true;
        OnChange?.Invoke();
    }
    public async Task GuardarUsuarioAsync()
    {
        await _sessionStorage.SetAsync("rol", Rol.ToString());
        await _sessionStorage.SetAsync("id", Id);
        if (Usuario != null) { await _sessionStorage.SetAsync("usuarioLogueado", Usuario); }
    }
    public async Task InicializarUsuarioAsync()
    {
        var result = await _sessionStorage.GetAsync<string>("rol");
        if (result.Success && Enum.TryParse(result.Value, out RolUsuario rol))
            Rol = rol;
        else
            Rol = RolUsuario.Invitado;
        var resultId = await _sessionStorage.GetAsync<int>("id");
        if (resultId.Success)
            Id = resultId.Value;
        var resultUsuario = await _sessionStorage.GetAsync<Usuario>("usuarioLogueado");
        if (resultUsuario.Success && resultUsuario.Value != null)
            Usuario = resultUsuario.Value;
        else
            Usuario = null!; // Inicializa un nuevo usuario si no hay datos guardados
                             // ACTUALIZA EL ESTADO DE SESIÓN
        SesionIniciada = Rol != RolUsuario.Invitado && Usuario != null;
        OnChange?.Invoke();
    }

    public async Task LogoutAsync()
    {
        Rol = RolUsuario.Invitado;
        Id = 0;
        await _sessionStorage.DeleteAsync("id");
        await _sessionStorage.DeleteAsync("rol");
        await _sessionStorage.DeleteAsync("usuarioLogueado");
        OnChange?.Invoke();

    }
    public async Task RefrescarUsuarioAsync()
    {
        if (SesionIniciada && Id > 0)
        {
            var usuarioActualizado = _repo.ObtenerPorId(Id);
            if (usuarioActualizado != null)
            {
                Usuario = usuarioActualizado;
                await GuardarUsuarioAsync(); // <-- Actualiza el almacenamiento protegido
            }
        }
    }

    public Task<bool> ExisteUsuarioConEmail(string email)
    {
        var usuario = _repo.IniciarSesion(email);
        return Task.FromResult(usuario != null);
    }

}