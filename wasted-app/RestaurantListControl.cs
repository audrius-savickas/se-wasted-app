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
    public partial class RestaurantListControl : UserControl
    {
        private static RestaurantListControl _instance;
        public static RestaurantListControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RestaurantListControl();
                return _instance;
            }
        }
        public RestaurantListControl()
        {
            InitializeComponent();
        }
    }
}
