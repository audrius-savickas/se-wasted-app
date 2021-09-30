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

        private bool CheckIfTextBoxesAreFull()
        {
            if (restaurantNameTextBox.Text != "Restaurant Name" && latitudeTextBox.Text != "Latitude" && longitudeTextBox.Text != "Longitude"
                && mailTextBox.Text != "Mail")
            {
                return true;
            }
            else return false;
        }

        private void ResetTextBoxes()
        {
            restaurantNameTextBox.Text = "";
            latitudeTextBox.Text = "";
            longitudeTextBox.Text = "";
            mailTextBox.Text = "";
            passwordTextBox.Text = "";
            repeatPasswordTextBox.Text = "";
            TextBoxLostFocus("Restaurant name", restaurantNameTextBox);
            TextBoxLostFocus("Latitude", latitudeTextBox);
            TextBoxLostFocus("Longitude", longitudeTextBox);
            TextBoxLostFocus("Mail", mailTextBox);
            TextBoxLostFocus("Password", passwordTextBox);
            TextBoxLostFocus("Repeat Password", repeatPasswordTextBox);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if(passwordTextBox.Text == repeatPasswordTextBox.Text && CheckIfTextBoxesAreFull())
            {
                ServicesController controller = ServicesController.Instance;
                var mail = mailTextBox.Text;
                var password = passwordTextBox.Text;
                var restaurant = controller.RestaurantService.GetByMail(new Mail(mail));
                if (restaurant == null)
                {
                    String credentialError = GetValidationError(mail, password);
                    if (credentialError == "")
                    {
                        passwordError.Text = "";
                        Credentials creds = new Credentials(mail, password);
                        controller.RestaurantService.Register(creds, new Restaurant("todo", restaurantNameTextBox.Text, new Coords(Convert.ToDecimal(latitudeTextBox.Text), Convert.ToDecimal(longitudeTextBox.Text)), new Credentials()));
                        MessageBox.Show("Registered successfully");
                        GoBack();
                        ResetTextBoxes();
                    }
                    else
                    {
                        passwordError.Text = credentialError;
                    }
                }
                else
                {
                    passwordError.Text = "• There is already an account registered on this mail";
                }
            }
            else if(!CheckIfTextBoxesAreFull())
            {
                passwordError.Text = "• All fields must be filled";
            }
            else 
            {
                passwordError.Text = "• Passwords don't match";
            }
            
        }

        private static String GetValidationError(string username, string password)
        {
            return Validator.validateEmail(username) + Validator.validatePassword(password);
        }

        private static void TextBoxGotFocus(string placeHolderText, TextBox textBox)
        {
            if (textBox.Text == placeHolderText)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private static void TextBoxLostFocus(string placeHolderText, TextBox textBox)
        {
            if (textBox.Text == "")
            {
                textBox.Text = placeHolderText;
                textBox.ForeColor = Color.Gray;
            }
        }

        private void restaurantNameTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Restaurant Name", restaurantNameTextBox);
        }

        private void restaurantNameTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Restaurant Name", restaurantNameTextBox);
        }
        private void latitudeTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Latitude", latitudeTextBox);
        }

        private void latitudeTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Latitude", latitudeTextBox);
        }

        private void longitudeTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Longitude", longitudeTextBox);
        }

        private void longitudeTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Longitude", longitudeTextBox);
        }

        private void mailTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Mail", mailTextBox);
        }

        private void mailTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Mail", mailTextBox);
        }

        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Password", passwordTextBox);
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Password", passwordTextBox);
        }

        private void repeatPasswordTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Repeat Password", repeatPasswordTextBox);
        }

        private void repeatPasswordTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Repeat Password", repeatPasswordTextBox);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            GoBack();
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

        private void repeatPasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (repeatPasswordTextBox.Text != "Repeat Password")
            {
                repeatPasswordTextBox.UseSystemPasswordChar = true;
            }
            else
            {
                repeatPasswordTextBox.UseSystemPasswordChar = false;
            }
        }

        private void showPasswordButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (passwordTextBox.Text != "Password")
            {
                passwordTextBox.UseSystemPasswordChar = true;
            }
        }

        private void showPasswordButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (passwordTextBox.Text != "Password")
            {
                passwordTextBox.UseSystemPasswordChar = false;
            }
        }

        private void showRepeatPasswordButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (repeatPasswordTextBox.Text != "Repeat Password")
            {
                repeatPasswordTextBox.UseSystemPasswordChar = true;
            }
        }

        private void showRepeatPasswordButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (repeatPasswordTextBox.Text != "Repeat Password")
            {
                repeatPasswordTextBox.UseSystemPasswordChar = false;
            }
        }

        private void GoBack()
        {
            MainForm.mainForm.panel.Controls.Remove(this);
        }
    }
}
