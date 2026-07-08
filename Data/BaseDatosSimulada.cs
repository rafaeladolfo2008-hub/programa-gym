using System;
using System.Collections.Generic;
using System.Linq;
using GymBookingApp.Models;

namespace GymBookingApp.Data
{
    public static class BaseDatosSimulada
    {
        public static List<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public static List<Gimnasio> Gimnasios { get; set; } = new List<Gimnasio>();
        public static List<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
        public static List<Entrenador> Entrenadores { get; set; } = new List<Entrenador>();
        public static List<ClaseProgramada> ClasesProgramadas { get; set; } = new List<ClaseProgramada>();
        public static List<Reserva> Reservas { get; set; } = new List<Reserva>();

        static BaseDatosSimulada()
        {
            SeedData();
        }

        private static void SeedData()
        {
            // 1. Usuarios (Login universal: pass "123" para todos)
            Usuarios.Add(new Usuario { Id = 1, Nombre = "Admin Master", Correo = "admin@gym.com", Password = "123", Rol = RolEnum.Administrador });
            Usuarios.Add(new Usuario { Id = 2, Nombre = "Juan Perez", Correo = "entrenador@gym.com", Password = "123", Rol = RolEnum.Entrenador });
            Usuarios.Add(new Usuario { Id = 3, Nombre = "Maria Lopez", Correo = "cliente@gym.com", Password = "123", Rol = RolEnum.Cliente });

            // 2. Gimnasios
            Gimnasios.Add(new Gimnasio { Id = 1, Nombre = "FitCenter Metrocentro", Direccion = "Managua, Metrocentro", Estado = true });
            Gimnasios.Add(new Gimnasio { Id = 2, Nombre = "PowerGym Galerias", Direccion = "Managua, Galerias Santo Domingo", Estado = true });

            // 3. Disciplinas
            var dZumba = new Disciplina { Id = 1, Nombre = "Zumba" };
            var dCardio = new Disciplina { Id = 2, Nombre = "Cardio" };
            var dYoga = new Disciplina { Id = 3, Nombre = "Yoga" };
            var dCrossFit = new Disciplina { Id = 4, Nombre = "CrossFit" };
            var dPesas = new Disciplina { Id = 5, Nombre = "Pesas" };
            var dFuncional = new Disciplina { Id = 6, Nombre = "Funcional" };
            var dSpinning = new Disciplina { Id = 7, Nombre = "Spinning" };
            var dPersonalizado = new Disciplina { Id = 8, Nombre = "Entrenamiento personalizado" };

            Disciplinas.AddRange(new[] { dZumba, dCardio, dYoga, dCrossFit, dPesas, dFuncional, dSpinning, dPersonalizado });

            // 4. Entrenadores
            var eJuan = new Entrenador { Id = 1, Nombre = "Juan Perez", DisciplinaId = 4, Disciplina = dCrossFit };
            var eAna = new Entrenador { Id = 2, Nombre = "Ana Martinez", DisciplinaId = 1, Disciplina = dZumba };
            var eCarlos = new Entrenador { Id = 3, Nombre = "Carlos Rivera", DisciplinaId = 5, Disciplina = dPesas };

            Entrenadores.AddRange(new[] { eJuan, eAna, eCarlos });

            // 5. Clases Programadas
            ClasesProgramadas.Add(new ClaseProgramada
            {
                Id = 1,
                DisciplinaId = dCrossFit.Id,
                Disciplina = dCrossFit,
                EntrenadorId = eJuan.Id,
                Entrenador = eJuan,
                GimnasioId = 1,
                Gimnasio = Gimnasios[0],
                FechaHoraInicio = DateTime.Now.AddDays(1).Date.AddHours(8), // Mañana a las 8 AM
                FechaHoraFin = DateTime.Now.AddDays(1).Date.AddHours(9),
                CupoMaximo = 15,
                CuposReservados = 0
            });

            ClasesProgramadas.Add(new ClaseProgramada
            {
                Id = 2,
                DisciplinaId = dZumba.Id,
                Disciplina = dZumba,
                EntrenadorId = eAna.Id,
                Entrenador = eAna,
                GimnasioId = 2,
                Gimnasio = Gimnasios[1],
                FechaHoraInicio = DateTime.Now.AddDays(1).Date.AddHours(18), // Mañana a las 6 PM
                FechaHoraFin = DateTime.Now.AddDays(1).Date.AddHours(19),
                CupoMaximo = 20,
                CuposReservados = 1
            });

            ClasesProgramadas.Add(new ClaseProgramada
            {
                Id = 3,
                DisciplinaId = dPesas.Id,
                Disciplina = dPesas,
                EntrenadorId = eCarlos.Id,
                Entrenador = eCarlos,
                GimnasioId = 1,
                Gimnasio = Gimnasios[0],
                FechaHoraInicio = DateTime.Now.AddDays(2).Date.AddHours(10),
                FechaHoraFin = DateTime.Now.AddDays(2).Date.AddHours(11),
                CupoMaximo = 10,
                CuposReservados = 0
            });

            ClasesProgramadas.Add(new ClaseProgramada
            {
                Id = 4,
                DisciplinaId = dYoga.Id,
                Disciplina = dYoga,
                EntrenadorId = eAna.Id, // Ana también da Yoga
                Entrenador = eAna,
                GimnasioId = 2,
                Gimnasio = Gimnasios[1],
                FechaHoraInicio = DateTime.Now.AddDays(2).Date.AddHours(7),
                FechaHoraFin = DateTime.Now.AddDays(2).Date.AddHours(8),
                CupoMaximo = 12,
                CuposReservados = 0
            });
            
            // 6. Reservas
            Reservas.Add(new Reserva
            {
                Id = 1,
                UsuarioId = 3,
                Usuario = Usuarios[2],
                ClaseProgramadaId = 2,
                ClaseProgramada = ClasesProgramadas[1],
                FechaConfirmacion = DateTime.Now,
                Estado = "Confirmada"
            });
        }
    }
}
