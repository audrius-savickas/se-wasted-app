using console_wasted_app.Controller;
using console_wasted_app.Controller.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace wasted_app
{
    public partial class RestaurantLogInControl : UserControl
    {
        private static RestaurantLogInControl _instance;

        public static RestaurantLogInControl Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RestaurantLogInControl();
                }

                return _instance;
            }
        }
        public RestaurantLogInControl()
        {
            InitializeComponent();
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            if (!MainForm.mainForm.panel.Controls.Contains(RestaurantRegistrationControl.Instance))
            {
                MainForm.mainForm.panel.Controls.Add(RestaurantRegistrationControl.Instance);
                RestaurantRegistrationControl.Instance.Dock = DockStyle.Fill;
                RestaurantRegistrationControl.Instance.BringToFront();
            }
            else
            {
                RestaurantRegistrationControl.Instance.BringToFront();
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainForm.mainForm.panel.Controls.Remove(_instance);
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            var controller = ServicesController.Instance;
            var creds = new Credentials(mailTextBox.Text, passwordTextBox.Text);
            if (controller.RestaurantService.Login(creds))
            {
                var restaurant = ServicesController.Instance.RestaurantService.GetByMail(new Mail(mailTextBox.Text));
                var restaurantFoodScreen = new RestaurantFoodControl(restaurant);
                MainForm.mainForm.panel.Controls.Add(restaurantFoodScreen);
                restaurantFoodScreen.Dock = DockStyle.Fill;
                restaurantFoodScreen.BringToFront();
            }
            else
            {
                MessageBox.Show("Wrong username or password!");
            }
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (passwordTextBox.Text != "Password")
            {
                passwordTextBox.UseSystemPasswordChar = true;
            }
            else
            {
                passwordTextBox.UseSystemPasswordChar = false;
            }
        }

        private void ShowPasswordButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (passwordTextBox.Text != "Password")
            {
                passwordTextBox.UseSystemPasswordChar = false;
            }
        }

        private void ShowPasswordButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (passwordTextBox.Text != "Password")
            {
                passwordTextBox.UseSystemPasswordChar = true;
            }
        }

        private void TextBoxGotFocus(string placeHolderText, TextBox textBox)
        {
            if (textBox.Text == placeHolderText)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void TextBoxLostFocus(string placeHolderText, TextBox textBox)
        {
            if (textBox.Text == "")
            {
                textBox.Text = placeHolderText;
                textBox.ForeColor = Color.Gray;
            }
        }

        private void MailTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Mail", mailTextBox);
        }

        private void MailTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Mail", mailTextBox);
        }

        private void PasswordTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Password", passwordTextBox);
        }

        private void PasswordTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Password", passwordTextBox);
        }
    }
}
