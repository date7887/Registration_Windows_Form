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

    
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            userNameField.Text = "Name";
            userSurnameField.Text = "Surname";
            loginField.Text = "Login";
            passField.Text = "Password";

            userNameField.TextAlign = HorizontalAlignment.Center;
            userSurnameField.TextAlign = HorizontalAlignment.Center;
            loginField.TextAlign = HorizontalAlignment.Center;
            passField.TextAlign = HorizontalAlignment.Center;

            userNameField.ForeColor = Color.Gray;
            userSurnameField.ForeColor = Color.Gray;
            loginField.ForeColor = Color.Gray;
            passField.ForeColor = Color.Gray;
        }

        private void closed_button_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
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

        

        private void closed_button_MouseLeave_1(object sender, EventArgs e)
        {
            closed_button.ForeColor = Color.White;
        }

        private void closed_button_MouseEnter(object sender, EventArgs e)
        {
            closed_button.ForeColor = Color.Green;
        }

        private void userNameField_Enter(object sender, EventArgs e)
        {
            if (userNameField.Text == "Name")
            {
                userNameField.ForeColor = Color.Black;
                userNameField.Text = "";
            }
                
        }

        private void userNameField_Leave(object sender, EventArgs e)
        {
            if (userNameField.Text == "")
            {
                userNameField.Text = "Name";
                userNameField.ForeColor = Color.Gray;
            }
                
        }

        private void userSurnameField_Enter(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "Surname")
            {
                userSurnameField.ForeColor = Color.Black;
                userSurnameField.Text = "";
            }
        }

        private void userSurnameField_Leave(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "")
            {
                userSurnameField.Text = "Surname";
                userSurnameField.ForeColor = Color.Gray;
            }
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Login")
            {
                loginField.ForeColor = Color.Black;
                loginField.Text = "";
            }
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                
                loginField.Text = "Login";
                loginField.ForeColor = Color.Gray;
            }
        }

        private void passField_Enter(object sender, EventArgs e)
        {
            if (passField.Text == "Password")
            {
                passField.ForeColor = Color.Black;
                passField.Text = "";
            }
        }

        private void passField_Leave(object sender, EventArgs e)
        {
            if (passField.Text == "")
            {
                passField.Text = "Password";
                loginField.ForeColor = Color.Gray;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `name`, `surname`) VALUES ('@ul', '@up', '@un', '@usn')", db.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@un", MySqlDbType.VarChar).Value = userNameField.Text;
            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = userSurnameField.Text;

            if (userNameField.Text == "Name" || userNameField.Text == "")
            {
                MessageBox.Show("Please, correct enter name");
                return;
            }

            if (userSurnameField.Text == "Surname" || userSurnameField.Text == "")
            {
                MessageBox.Show("Please, correct enter Surname");
                return;
            }

            if (loginField.Text == "Login" || loginField.Text == "")
            {
                MessageBox.Show("Please, correct enter login");
                return;
            }

            if (isLoginExists())
                return;

            if (loginField.Text == "Password" || loginField.Text == "")
            {
                MessageBox.Show("Please, correct enter password");
                return;
            }
            
            if (loginField.TextLength <= 8)
            {
                MessageBox.Show("Please, correct enter password");
                return;
            }

            db.openConnection();
            
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Account was created");
            }
            else
            {
                MessageBox.Show("Account wasn`t created");
            }

            db.closedConnection();

        }

        public bool isLoginExists ()
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @ul", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginField.Text;
            
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Such login was been created");
                return true;
            }
            else 
            {
                return false;
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm log_form = new LoginForm();
            log_form.Show();
        }
    }
}
