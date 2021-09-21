using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wasted_app
{
    public partial class RestaurantRegistrationControl : UserControl
    {
        private static RestaurantRegistrationControl _instance;
        private int minimumPasswordLength = 8;
        private bool showPassword = false;
        public static RestaurantRegistrationControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RestaurantRegistrationControl();
                return _instance;
            }
        }
        public RestaurantRegistrationControl()
        {
            InitializeComponent();
        }

        private void usernameInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            var username = usernameInput.Text;
            var password = passwordInput.Text;
            String error = validateUsernamePassword(username, password);
            if (error == "")
            {
                passwordError.Text = "";
                MessageBox.Show("Registered successfully");
            }
            else
            {
                passwordError.Text = error;
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
                passwordInput.UseSystemPasswordChar = false;
            }
            else
            {
                passwordInput.UseSystemPasswordChar = true;
            }
        }

        private String validateUsernamePassword(string username, string password)
        {
            return Validator.validatePassword(password);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainForm.mainForm.panel.Controls.Remove(_instance);
        }
    }
}
