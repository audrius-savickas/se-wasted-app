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

        private void backButton_Click(object sender, EventArgs e)
        {
            MainForm.mainForm.panel.Controls.Remove(_instance);
        }

        private void logInButton_Click(object sender, EventArgs e)
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

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
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

        private void showPasswordButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (passwordTextBox.Text != "Password")
            {
                passwordTextBox.UseSystemPasswordChar = false;
            }
        }

        private void showPasswordButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (passwordTextBox.Text != "Password")
            {
                passwordTextBox.UseSystemPasswordChar = true;
            }
        }

        private void textBoxGotFocus(string placeHolderText, TextBox textBox)
        {
            if (textBox.Text == placeHolderText)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void textBoxLostFocus(string placeHolderText, TextBox textBox)
        {
            if (textBox.Text == "")
            {
                textBox.Text = placeHolderText;
                textBox.ForeColor = Color.Gray;
            }
        }

        private void mailTextBox_Leave(object sender, EventArgs e)
        {
            textBoxLostFocus("Mail", mailTextBox);
        }

        private void mailTextBox_Enter(object sender, EventArgs e)
        {
            textBoxGotFocus("Mail", mailTextBox);
        }

        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            textBoxGotFocus("Password", passwordTextBox);
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            textBoxLostFocus("Password", passwordTextBox);
        }
    }
}
