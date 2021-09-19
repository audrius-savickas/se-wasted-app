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
    public partial class RestaurantLogInControl : UserControl
    {
        private static RestaurantLogInControl _instance;
        private static Panel panel;
        public static RestaurantLogInControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RestaurantLogInControl();
                return _instance;
            }
        }
        public RestaurantLogInControl()
        {
            InitializeComponent();
        }

        public static void SetPanel(Panel mainPanel)
        {
            panel = mainPanel;
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            if (!panel.Controls.Contains(RestaurantRegistrationControl.Instance))
            {
                panel.Controls.Add(RestaurantRegistrationControl.Instance);
                RestaurantRegistrationControl.Instance.Dock = DockStyle.Fill;
                RestaurantRegistrationControl.Instance.BringToFront();
            }
            else
            {
                RestaurantRegistrationControl.Instance.BringToFront();
            }
        }
    }
}
