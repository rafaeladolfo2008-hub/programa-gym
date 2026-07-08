using System;
using System.Drawing;
using System.Windows.Forms;
using GymBookingApp.Services;
using GymBookingApp.Models;

namespace GymBookingApp.UI
{
    public class FrmLogin : Form
    {
        private TableLayoutPanel _mainLayout = null!;
        private Label _lblTitle = null!;
        private Label _lblCorreo = null!;
        private TextBox _txtCorreo = null!;
        private Label _lblPassword = null!;
        private TextBox _txtPassword = null!;
        private Button _btnLogin = null!;
        private Label _lblMessage = null!;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Gym Booking App - Iniciar Sesión";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            _mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 6,
                Padding = new Padding(40),
                BackColor = Color.White
            };

            _lblTitle = new Label
            {
                Text = "ACCESO AL SISTEMA",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Height = 40
            };

            _lblCorreo = new Label { Text = "Correo Electrónico:", Dock = DockStyle.Bottom, Height = 20, Font = new Font("Segoe UI", 10) };
            _txtCorreo = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 12), Height = 30 };

            _lblPassword = new Label { Text = "Contraseña:", Dock = DockStyle.Bottom, Height = 20, Font = new Font("Segoe UI", 10) };
            _txtPassword = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 12), Height = 30, PasswordChar = '*' };

            _btnLogin = new Button
            {
                Text = "Ingresar",
                Dock = DockStyle.Fill,
                Height = 40,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 20, 0, 0)
            };
            _btnLogin.FlatAppearance.BorderSize = 0;
            _btnLogin.Click += BtnLogin_Click;

            _lblMessage = new Label
            {
                Text = "Usa admin@gym.com / entrenador@gym.com / cliente@gym.com | Pass: 123",
                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Bottom,
                ForeColor = Color.Gray,
                Height = 40
            };

            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            _mainLayout.Controls.Add(_lblTitle, 0, 0);
            _mainLayout.Controls.Add(_lblCorreo, 0, 1);
            _mainLayout.Controls.Add(_txtCorreo, 0, 2);
            _mainLayout.Controls.Add(_lblPassword, 0, 3);
            _mainLayout.Controls.Add(_txtPassword, 0, 4);
            _mainLayout.Controls.Add(_btnLogin, 0, 5);

            this.Controls.Add(_mainLayout);
            this.Controls.Add(_lblMessage);
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            string correo = _txtCorreo.Text.Trim();
            string pass = _txtPassword.Text.Trim();

            if (ServicioAutenticacion.Login(correo, pass))
            {
                this.Hide();
                var dashboard = new FrmPanelControl();
                dashboard.FormClosed += (s, args) => this.Close();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Credenciales inválidas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
