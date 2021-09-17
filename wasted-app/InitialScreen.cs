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
    public partial class InitialScreen : Form
    {
        public InitialScreen()
        {
            InitializeComponent();
        }

        private void UserButton_Click(object sender, EventArgs e)
        {
            var frm = new RestaurantListScreen();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            //frm.FormClosing += delegate { this.Show(); };
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void restaurantButton_Click(object sender, EventArgs e)
        {
            var restaurantRegistration = new RestaurantRegistrationScreen();
            this.Hide();
            restaurantRegistration.ShowDialog();
            this.Close();
        }
    }
}
