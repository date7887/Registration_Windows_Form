using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleSQL_App
{
    public partial class LoginForm : MaterialSkin.Controls.MaterialForm
    {
        public LoginForm()
        {
            InitializeComponent();

            this.passField.AutoSize = false;
            this.passField.Size = new Size(this.passField.Size.Width, loginField.Size.Height);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void closed_button_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
        }

        private void closed_button_MouseEnter(object sender, EventArgs e)
        {
            closed_button.ForeColor = Color.Green;
        }

        private void closed_button_MouseLeave(object sender, EventArgs e)
        {
            closed_button.ForeColor = Color.White;
        }

        Point lastPoint;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // received data from loginField and passField, when user pressed the button
            string loginUser = loginField.Text;
            string passUser  = passField.Text;
            
            // create some new objects
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @ul AND `password` = @up", db.getConnection());              // object for sampling data

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                this.Hide();
                MainForm main_screen = new MainForm();
                main_screen.Show();
            }
            else 
            {
                MessageBox.Show("No");
            }

        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm reg_form = new RegisterForm();
            reg_form.Show();
        }
    }
}
