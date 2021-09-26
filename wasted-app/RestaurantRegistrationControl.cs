using console_wasted_app.Controller;
using console_wasted_app.Controller.Entities;
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
            String credentialError = getValidationError(username, password);

            if (credentialError == "")
            {
                this.passwordError.Text = "";
                ServicesController controller = ServicesController.Instance;
                Credentials creds = new Credentials(username, password);
                controller.RestaurantService.Register(creds, new Restaurant("aa", "Hello World", new Coords(10, 20), new Credentials()));
                MessageBox.Show("Registered successfully");
                goBack();
            }
            else
            {
                this.passwordError.Text = credentialError;
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

        private String getValidationError(string username, string password)
        {
            return Validator.validateEmail(username) + Validator.validatePassword(password);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            goBack();        
        }

        private void goBack()
        {
            MainForm.mainForm.panel.Controls.Remove(_instance);
        }
    }
}
