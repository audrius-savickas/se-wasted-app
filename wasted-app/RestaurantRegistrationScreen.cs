using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace wasted_app
{
    public partial class RestaurantRegistrationScreen : Form
    {
        private int minimumPasswordLength = 8;
        private bool showPassword = false;
        public RestaurantRegistrationScreen()
        {
            InitializeComponent();
        }

        private void usernameInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            var username = usernameInput.Text;
            var password = passwordInput.Text;
            if (validateUsernamePassword(username, password))
            {
                MessageBox.Show("Registered successfully");
            }
        }

        private void passwordInput_TextChanged(object sender, EventArgs e)
        {
            if (passwordInput.Text.Length < minimumPasswordLength)
            {
                passwordInput.ForeColor = Color.Red;
            }
            else
            {
                passwordInput.ForeColor = Color.Black;
            }
        }

        private void showPasswordCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            showPassword = !showPassword;
            if (showPassword)
            {
                passwordInput.PasswordChar = '\0';
            }
            else
            {
                passwordInput.PasswordChar = '*';
            }
        }

        private bool validateUsernamePassword(string username, string password)
        {
            // Some outside validation code
            return true;
        }
    }
}
