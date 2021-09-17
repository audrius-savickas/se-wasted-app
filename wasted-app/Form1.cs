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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void logInButton_Click(object sender, EventArgs e)
        {

            //Place holder until validation functions are made
            if (userNameTextBox.Text == "admin" && passwordTextBox.Text == "admin")
            {
                MessageBox.Show("Successfuly loged in!");
                
            }
            else
            {
                MessageBox.Show("Wrong username or password!");
            }

        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            //Place holder until scene changing is working
            MessageBox.Show("Went back!");
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            passwordTextBox.PasswordChar = '*';
        }

        private void showPasswordButton_MouseDown(object sender, MouseEventArgs e)
        {
            passwordTextBox.PasswordChar = '\0';
        }

        private void showPasswordButton_MouseUp(object sender, MouseEventArgs e)
        {
            passwordTextBox.PasswordChar = '*';
        }
    }
}
