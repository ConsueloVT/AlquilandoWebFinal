using AL.UI.Components;
//agregamos estas directivas using
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using AL.Repositorios;
using AL.Aplicacion.CasosDeUso;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Servicios;
using AL.Aplicacion.Validadores;
using AL.UI.Servicios;


var builder = WebApplication.CreateBuilder(args);
EntidadesSqlite.Inicializar();

//agregamos estos servicios al contenedor DI

builder.Services.AddTransient<UsuarioAlta>();
builder.Services.AddTransient<AlojamientoAlta>();
builder.Services.AddTransient<AlojamientoPuntuar>();
builder.Services.AddTransient<BuscarAlojamientoCasoDeUso>();
builder.Services.AddTransient<FiltrarAlojamientoCasoDeUso>();
builder.Services.AddTransient<AlojamientoEdicion>();
builder.Services.AddTransient<CancelarReservaCasoDeUso>();
builder.Services.AddTransient<ReservaAlta>();
builder.Services.AddTransient<ReservaConsultaTodosPorUsuario>();
builder.Services.AddTransient<ReservaEnviarMensaje>();
builder.Services.AddTransient<ReservaConsultaTodos>();
builder.Services.AddTransient<GestionarReservasCasoDeUso>();
builder.Services.AddScoped<ArchivarPublicacionCasoDeUso>();
builder.Services.AddScoped<DesarchivarPublicacionCasoDeUso>();
builder.Services.AddTransient<EliminarUsuario>();

builder.Services.AddSingleton<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddSingleton<ITarjetaRepositorio, TarjetaRepositorio>();
builder.Services.AddSingleton<IAlojamientoRepositorio, AlojamientoRepositorio>();
builder.Services.AddSingleton<IReservasRepositorio, ReservasRepositorio>();


builder.Services.AddTransient<IUsuarioValidador, UsuarioValidador>();
builder.Services.AddTransient<IAlojamientoValidador, AlojamientoValidador>();
builder.Services.AddTransient<IAlojamientoValidadorEdicion, AlojamientoValidadorEdicion>();

builder.Services.AddScoped<IServicioPago, ServicioPago>();
builder.Services.AddSingleton<ServicioArchivos>();
builder.Services.AddTransient<IHashService, HashService>();
builder.Services.AddScoped<IServicioSesion,ServicioSesion>();
builder.Services.AddScoped<IServicioReserva, ServicioReserva>();
builder.Services.AddScoped<IServicioChat, ServicioChat>();
builder.Services.AddTransient<IServicioActualizacionEstadoReserva, ServicioActualizacionEstadoReserva>();
builder.Services.AddScoped<ProtectedSessionStorage>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
