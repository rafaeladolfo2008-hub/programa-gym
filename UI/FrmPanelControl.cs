using System;
using System.Drawing;
using System.Windows.Forms;
using GymBookingApp.Services;
using GymBookingApp.Data;

namespace GymBookingApp.UI
{
    public class FrmPanelControl : Form
    {
        private Panel _menuPanel = null!;
        private Panel _contentPanel = null!;

        public FrmPanelControl()
        {
            InitializeComponent();
            CargarVistaCatalogo();
        }

        private void InitializeComponent()
        {
            this.Text = $"Gym Booking App - Bienvenido {ServicioAutenticacion.UsuarioLogueado?.Nombre}";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            _menuPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 200,
                BackColor = Color.FromArgb(45, 45, 48)
            };

            var btnCatalogo = new Button { Text = "Catálogo de Clases", Dock = DockStyle.Top, Height = 50, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCatalogo.FlatAppearance.BorderSize = 0;
            btnCatalogo.Click += (s, e) => CargarVistaCatalogo();

            var btnMisReservas = new Button { Text = "Mis Reservas", Dock = DockStyle.Top, Height = 50, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnMisReservas.FlatAppearance.BorderSize = 0;
            btnMisReservas.Click += (s, e) => CargarVistaReservas();

            var btnEntrenadores = new Button { Text = "Entrenadores", Dock = DockStyle.Top, Height = 50, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnEntrenadores.FlatAppearance.BorderSize = 0;
            btnEntrenadores.Click += (s, e) => CargarVistaEntrenadores();

            var btnAdmin = new Button { Text = "Administración", Dock = DockStyle.Top, Height = 50, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Visible = ServicioAutenticacion.UsuarioLogueado?.Rol == GymBookingApp.Models.RolEnum.Administrador };
            btnAdmin.FlatAppearance.BorderSize = 0;
            btnAdmin.Click += (s, e) => MessageBox.Show("Vista de Administración en construcción. Aquí gestionarías Gimnasios, Entrenadores y Clases.", "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var btnLogout = new Button { Text = "Cerrar Sesión", Dock = DockStyle.Bottom, Height = 50, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(192, 57, 43) };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += BtnLogout_Click;

            _menuPanel.Controls.Add(btnAdmin);
            _menuPanel.Controls.Add(btnEntrenadores);
            _menuPanel.Controls.Add(btnMisReservas);
            _menuPanel.Controls.Add(btnCatalogo);
            _menuPanel.Controls.Add(btnLogout);

            _contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke
            };

            this.Controls.Add(_contentPanel);
            this.Controls.Add(_menuPanel);
        }

        private void CargarVistaCatalogo()
        {
            _contentPanel.Controls.Clear();
            var lblTitle = new Label { Text = "Calendario de Clases Disponibles", Font = new Font("Segoe UI", 16, FontStyle.Bold), Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
            
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                GridColor = Color.LightGray
            };
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grid.DataSource = BaseDatosSimulada.ClasesProgramadas.ConvertAll(c => new
            {
                c.Id,
                Fecha = c.FechaHoraInicio.ToString("dd/MM/yyyy"),
                Horario = $"{c.FechaHoraInicio.ToString("HH:mm")} - {c.FechaHoraFin.ToString("HH:mm")}",
                Disciplina = c.Disciplina.Nombre,
                Entrenador = c.Entrenador.Nombre,
                Gimnasio = c.Gimnasio.Nombre,
                Disponibilidad = (c.CupoMaximo - c.CuposReservados) > 0 ? "Disponible" : "Lleno",
                Cupos = $"{c.CuposReservados} / {c.CupoMaximo}"
            });

            var btnReservar = new Button { Text = "Reservar Clase Seleccionada", Dock = DockStyle.Bottom, Height = 40, BackColor = Color.Green, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnReservar.Click += (s, e) => {
                if (grid.SelectedRows.Count > 0)
                {
                    if (grid.SelectedRows[0].Cells["Id"].Value is int id)
                    {
                        var clase = BaseDatosSimulada.ClasesProgramadas.Find(c => c.Id == id);
                        if (clase != null && ServicioAutenticacion.UsuarioLogueado != null)
                    {
                        if (clase.CuposReservados >= clase.CupoMaximo)
                        {
                            MessageBox.Show("Lo sentimos, esta clase ya no tiene cupos disponibles.", "Cupos Agotados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var reserva = new GymBookingApp.Models.Reserva
                        {
                            Id = BaseDatosSimulada.Reservas.Count + 1,
                            UsuarioId = ServicioAutenticacion.UsuarioLogueado.Id,
                            Usuario = ServicioAutenticacion.UsuarioLogueado!,
                            ClaseProgramadaId = clase.Id,
                            ClaseProgramada = clase,
                            FechaConfirmacion = DateTime.Now,
                            Estado = "Confirmada"
                        };
                        clase.CuposReservados++;
                        BaseDatosSimulada.Reservas.Add(reserva);
                        ServicioCorreo.EnviarConfirmacionReserva(reserva);
                        MessageBox.Show("Reserva realizada con éxito y correo de confirmación enviado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarVistaCatalogo();
                        }
                    }
                }
            };

            _contentPanel.Controls.Add(grid);
            _contentPanel.Controls.Add(btnReservar);
            _contentPanel.Controls.Add(lblTitle);
        }

        private void CargarVistaReservas()
        {
            _contentPanel.Controls.Clear();
            var lblTitle = new Label { Text = "Mi Historial de Reservas", Font = new Font("Segoe UI", 16, FontStyle.Bold), Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
            
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true
            };

            var misReservas = BaseDatosSimulada.Reservas.FindAll(r => r.UsuarioId == ServicioAutenticacion.UsuarioLogueado?.Id);
            grid.DataSource = misReservas.ConvertAll(r => new
            {
                r.Id,
                Disciplina = r.ClaseProgramada.Disciplina.Nombre,
                Fecha = r.ClaseProgramada.FechaHoraInicio.ToString("g"),
                r.Estado,
                r.FechaConfirmacion
            });

            _contentPanel.Controls.Add(grid);
            _contentPanel.Controls.Add(lblTitle);
        }

        private void CargarVistaEntrenadores()
        {
            _contentPanel.Controls.Clear();
            var lblTitle = new Label { Text = "Nuestros Entrenadores", Font = new Font("Segoe UI", 16, FontStyle.Bold), Dock = DockStyle.Top, Height = 40, TextAlign = ContentAlignment.MiddleCenter };
            
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            grid.DataSource = BaseDatosSimulada.Entrenadores.ConvertAll(e => new
            {
                ID = e.Id,
                Nombre = e.Nombre,
                Especialidad = e.Disciplina.Nombre
            });

            _contentPanel.Controls.Add(grid);
            _contentPanel.Controls.Add(lblTitle);
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            ServicioAutenticacion.Logout();
            this.Hide();
            var login = new FrmLogin();
            login.FormClosed += (s, args) => this.Close();
            login.Show();
        }
    }
}
