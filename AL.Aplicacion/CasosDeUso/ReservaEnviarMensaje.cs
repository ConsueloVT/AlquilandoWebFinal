using System;
using AL.Aplicacion.Entidades;
using AL.Aplicacion.Interfaces;
using AL.Aplicacion.Enumerativos;
namespace AL.Aplicacion.CasosDeUso;

public class ReservaEnviarMensaje(IReservasRepositorio _reservaRepo, IUsuarioRepositorio _usuarioRepo,IServicioChat _chat): ReservaCasoDeUso(_reservaRepo)
{
    public async Task Ejecutar(int reservaId, Mensaje nuevoMensaje)
    {
        Reserva? reserva = Repositorio.ObtenerPorId(reservaId);
        Usuario? usuario = _usuarioRepo.ObtenerPorId(nuevoMensaje.IdEmisor);

        if (reserva != null && DateTime.Now <= reserva.FechaFinEstadia.AddDays(7))
        {
            if (usuario != null && usuario.Rol == RolUsuario.Usuario)
            {
                if (reserva.EstadoReserva == EstadoReserva.Confirmada)
                {
                    Usuario? admin = _usuarioRepo.ObtenerAdministrador();
                    if (admin != null)
                    {
                        nuevoMensaje.IdReceptor = admin.Id;
                        await _chat.EnviarMensajeAsync(nuevoMensaje, reserva);
                    }

                }
                else if (reserva.EstadoReserva == EstadoReserva.EnCurso || reserva.EstadoReserva == EstadoReserva.Finalizada)
                {
                    Usuario? encargado = _usuarioRepo.ObtenerEncargado();
                    if (encargado != null)
                    {
                        nuevoMensaje.IdReceptor = encargado.Id;
                        await _chat.EnviarMensajeAsync(nuevoMensaje, reserva);
                    }

                }
            }
            else
            {
                nuevoMensaje.IdReceptor = reserva.IdUsuario;
                await _chat.EnviarMensajeAsync(nuevoMensaje, reserva);
            }
        }
        else
        {
            throw new Exception("No se puede enviar mensajes 7 días después de terminada la estadía.");
        }

    }
}