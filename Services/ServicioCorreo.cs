using System;
using System.Diagnostics;
using GymBookingApp.Models;

namespace GymBookingApp.Services
{
    public class ServicioCorreo
    {
        public static void EnviarConfirmacionReserva(Reserva reserva)
        {
            // Simulador de envío de correo
            string mensaje = $"Para: {reserva.Usuario.Correo}\n" +
                             $"Asunto: Confirmación de Reserva de Entrenamiento\n\n" +
                             $"Hola {reserva.Usuario.Nombre},\n" +
                             $"Tu reserva para la clase de {reserva.ClaseProgramada.Disciplina.Nombre} ha sido confirmada.\n" +
                             $"Fecha y Hora: {reserva.ClaseProgramada.FechaHoraInicio.ToString("g")}\n" +
                             $"Gimnasio: {reserva.ClaseProgramada.Gimnasio.Nombre} ({reserva.ClaseProgramada.Gimnasio.Direccion})\n" +
                             $"Entrenador: {reserva.ClaseProgramada.Entrenador.Nombre}\n\n" +
                             $"¡Te esperamos!";
            
            Debug.WriteLine("=== SIMULACIÓN DE CORREO ENVIADO ===");
            Debug.WriteLine(mensaje);
            Debug.WriteLine("====================================");
        }
    }
}
