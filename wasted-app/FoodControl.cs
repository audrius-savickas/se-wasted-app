using System.Windows.Forms;

namespace wasted_app
{
    public partial class FoodControl : UserControl
    {
        public FoodControl()
        {
            InitializeComponent();
        }

        public FoodControl(string name, string type, string price) : this()
        {
            foodNameLabel.Text = name;
            foodTypeLabel.Text = type;
            priceLabel.Text = "$" + price;
        }
    }
}
