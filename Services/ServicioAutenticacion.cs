using System.Linq;
using GymBookingApp.Data;
using GymBookingApp.Models;

namespace GymBookingApp.Services
{
    public class ServicioAutenticacion
    {
        public static Usuario? UsuarioLogueado { get; private set; }

        public static bool Login(string correo, string password)
        {
            var usuario = BaseDatosSimulada.Usuarios.FirstOrDefault(u => u.Correo == correo && u.Password == password);
            if (usuario != null)
            {
                UsuarioLogueado = usuario;
                return true;
            }
            return false;
        }

        public static void Logout()
        {
            UsuarioLogueado = null;
        }

        public static bool EstaLogueado()
        {
            return UsuarioLogueado != null;
        }
    }
}
