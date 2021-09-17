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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void UserButton_Click(object sender, EventArgs e)
        {
            if (!panel.Controls.Contains(RestaurantListControl.Instance))
            {
                panel.Controls.Add(RestaurantListControl.Instance);
                RestaurantListControl.Instance.Dock = DockStyle.Fill;
                RestaurantListControl.Instance.BringToFront();
            } 
            else
            {
                RestaurantListControl.Instance.BringToFront();
            }

        }

        private void restaurantButton_Click(object sender, EventArgs e)
        {
            if (!panel.Controls.Contains(RestaurantLogInControl.Instance))
            {
                panel.Controls.Add(RestaurantLogInControl.Instance);
                RestaurantLogInControl.Instance.Dock = DockStyle.Fill;
                RestaurantLogInControl.Instance.BringToFront();
            }
            else
            {
                RestaurantLogInControl.Instance.BringToFront();
            }
        }
    }
}
