using console_wasted_app.Controller;
using console_wasted_app.Controller.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace wasted_app
{
    public partial class RestaurantRegistrationControl : UserControl
    {
        private static RestaurantRegistrationControl _instance;
        private readonly bool showPassword = false;
        public static RestaurantRegistrationControl Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RestaurantRegistrationControl();
                }

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
            else
            {
                return false;
            }
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

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == repeatPasswordTextBox.Text && CheckIfTextBoxesAreFull())
            {
                var mail = mailTextBox.Text;
                var password = passwordTextBox.Text;
                var credentialError = GetValidationError(mail, password);
                if (credentialError == "")
                {
                    passwordError.Text = "";
                    var controller = ServicesController.Instance;
                    var creds = new Credentials(mail, password);
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
            else if (!CheckIfTextBoxesAreFull())
            {
                passwordError.Text = "• All fields must be filled";
            }
            else
            {
                passwordError.Text = "• Passwords don't match";
            }

        }

        private string GetValidationError(string username, string password)
        {
            return Validator.ValidateEmail(username) + Validator.ValidatePassword(password);
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

        private void RestaurantNameTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Restaurant Name", restaurantNameTextBox);
        }

        private void RestaurantNameTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Restaurant Name", restaurantNameTextBox);
        }
        private void LatitudeTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Latitude", latitudeTextBox);
        }

        private void LatitudeTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Latitude", latitudeTextBox);
        }

        private void LongitudeTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Longitude", longitudeTextBox);
        }

        private void LongitudeTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Longitude", longitudeTextBox);
        }

        private void MailTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Mail", mailTextBox);
        }

        private void MailTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Mail", mailTextBox);
        }

        private void PasswordTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Password", passwordTextBox);
        }

        private void PasswordTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Password", passwordTextBox);
        }

        private void RepeatPasswordTextBox_Enter(object sender, EventArgs e)
        {
            TextBoxGotFocus("Repeat Password", repeatPasswordTextBox);
        }

        private void RepeatPasswordTextBox_Leave(object sender, EventArgs e)
        {
            TextBoxLostFocus("Repeat Password", repeatPasswordTextBox);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            GoBack();
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

        private void RepeatPasswordTextBox_TextChanged(object sender, EventArgs e)
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

        private void ShowPasswordButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (passwordTextBox.Text != "Password")
            {
                passwordTextBox.UseSystemPasswordChar = true;
            }
        }

        private void ShowPasswordButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (passwordTextBox.Text != "Password")
            {
                passwordTextBox.UseSystemPasswordChar = false;
            }
        }

        private void ShowRepeatPasswordButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (repeatPasswordTextBox.Text != "Repeat Password")
            {
                repeatPasswordTextBox.UseSystemPasswordChar = true;
            }
        }

        private void ShowRepeatPasswordButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (repeatPasswordTextBox.Text != "Repeat Password")
            {
                repeatPasswordTextBox.UseSystemPasswordChar = false;
            }
        }

        private void GoBack()
        {
            MainForm.mainForm.panel.Controls.Remove(_instance);
        }
    }
}
