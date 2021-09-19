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

        private void signUpButton_Click(object sender, EventArgs e)
        {

        }
    }
}
