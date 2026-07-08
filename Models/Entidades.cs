using System;
using System.Collections.Generic;

namespace GymBookingApp.Models
{
    public enum RolEnum
    {
        Administrador,
        Entrenador,
        Cliente
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public RolEnum Rol { get; set; }
    }

    public class Gimnasio
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }

    public class Disciplina
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public class Entrenador
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; } = null!;
    }

    public class ClaseProgramada
    {
        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; } = null!;
        
        public int EntrenadorId { get; set; }
        public Entrenador Entrenador { get; set; } = null!;
        
        public int GimnasioId { get; set; }
        public Gimnasio Gimnasio { get; set; } = null!;
        
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }
        public int CupoMaximo { get; set; }
        public int CuposReservados { get; set; }
    }

    public class Reserva
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        
        public int ClaseProgramadaId { get; set; }
        public ClaseProgramada ClaseProgramada { get; set; } = null!;
        
        public DateTime FechaConfirmacion { get; set; }
        public string Estado { get; set; } = string.Empty; // "Confirmada", "Cancelada"
    }
}
