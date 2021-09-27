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
    public partial class RestaurntControl : UserControl
    {
        public RestaurntControl()
        {
            InitializeComponent();
        }

        public RestaurntControl(String name) : this ()
        {
            nameButton.Text = name;
        }
    }
}
