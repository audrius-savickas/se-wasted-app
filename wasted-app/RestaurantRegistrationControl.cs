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

        private bool checkIfTextBoxesAreFull()
        {
            if (restaurantNameTextBox.Text != "Restaurant Name" && latitudeTextBox.Text != "Latitude" && longitudeTextBox.Text != "Longitude"
                && mailTextBox.Text != "Mail")
            {
                return true;
            }
            else return false;
        }

        private void resetTextBoxes()
        {
            restaurantNameTextBox.Text = "";
            latitudeTextBox.Text = "";
            longitudeTextBox.Text = "";
            mailTextBox.Text = "";
            passwordTextBox.Text = "";
            repeatPasswordTextBox.Text = "";
            textBoxLostFocus("Restaurant name", restaurantNameTextBox);
            textBoxLostFocus("Latitude", latitudeTextBox);
            textBoxLostFocus("Longitude", longitudeTextBox);
            textBoxLostFocus("Mail", mailTextBox);
            textBoxLostFocus("Password", passwordTextBox);
            textBoxLostFocus("Repeat Password", repeatPasswordTextBox);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if(passwordTextBox.Text == repeatPasswordTextBox.Text && checkIfTextBoxesAreFull())
            {
                ServicesController controller = ServicesController.Instance;
                var mail = mailTextBox.Text;
                var password = passwordTextBox.Text;
                Mail testMail = new Mail(mail);
                Restaurant restaurant = controller.RestaurantService.GetByMail(testMail);
                if(restaurant == null)
                {
                    String credentialError = getValidationError(mail, password);
                    if (credentialError == "")
                    {
                        passwordError.Text = "";
                        Credentials creds = new Credentials(mail, password);
                        controller.RestaurantService.Register(creds, new Restaurant("todo", restaurantNameTextBox.Text, new Coords(Convert.ToDecimal(latitudeTextBox.Text), Convert.ToDecimal(longitudeTextBox.Text)), new Credentials()));
                        MessageBox.Show("Registered successfully");
                        goBack();
                        resetTextBoxes();
                    }
                    else
                    {
                        passwordError.Text = credentialError;
                    }
                }
                else
                {
                    passwordError.Text = "• There is an account already registered on this mail";
                }
            }
            else if(!checkIfTextBoxesAreFull())
            {
                passwordError.Text = "• All fields must be filled";
            }
            else 
            {
                passwordError.Text = "• Passwords don't match";
            }
            
        }

        private String getValidationError(string username, string password)
        {
            return Validator.validateEmail(username) + Validator.validatePassword(password);
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

        private void restaurantNameTextBox_Enter(object sender, EventArgs e)
        {
            textBoxGotFocus("Restaurant Name", restaurantNameTextBox);
        }

        private void restaurantNameTextBox_Leave(object sender, EventArgs e)
        {
            textBoxLostFocus("Restaurant Name", restaurantNameTextBox);
        }
        private void latitudeTextBox_Enter(object sender, EventArgs e)
        {
            textBoxGotFocus("Latitude", latitudeTextBox);
        }

        private void latitudeTextBox_Leave(object sender, EventArgs e)
        {
            textBoxLostFocus("Latitude", latitudeTextBox);
        }

        private void longitudeTextBox_Enter(object sender, EventArgs e)
        {
            textBoxGotFocus("Longitude", longitudeTextBox);
        }

        private void longitudeTextBox_Leave(object sender, EventArgs e)
        {
            textBoxLostFocus("Longitude", longitudeTextBox);
        }

        private void mailTextBox_Enter(object sender, EventArgs e)
        {
            textBoxGotFocus("Mail", mailTextBox);
        }

        private void mailTextBox_Leave(object sender, EventArgs e)
        {
            textBoxLostFocus("Mail", mailTextBox);
        }

        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            textBoxGotFocus("Password", passwordTextBox);
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            textBoxLostFocus("Password", passwordTextBox);
        }

        private void repeatPasswordTextBox_Enter(object sender, EventArgs e)
        {
            textBoxGotFocus("Repeat Password", repeatPasswordTextBox);
        }

        private void repeatPasswordTextBox_Leave(object sender, EventArgs e)
        {
            textBoxLostFocus("Repeat Password", repeatPasswordTextBox);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            goBack();
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

        private void goBack()
        {
            MainForm.mainForm.panel.Controls.Remove(_instance);
        }
    }
}
