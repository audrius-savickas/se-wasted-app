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
    public partial class FoodControl : UserControl
    {
        public FoodControl()
        {
            InitializeComponent();
        }

        public FoodControl(String name, String type) : this ()
        {
            foodNameLabel.Text = name;
            foodTypeLabel.Text = type;
        }
    }
}
