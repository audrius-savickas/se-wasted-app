using System.Windows.Forms;

namespace wasted_app
{
    public partial class FoodControl : UserControl
    {
        public FoodControl()
        {
            InitializeComponent();
        }

        public FoodControl(FoodListItem item) : this()
        {
            foodNameLabel.Text = item.Name;
            foodTypeLabel.Text = item.Type;
            priceLabel.Text = "$" + item.Price;
        }
    }

    public class FoodListItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
        public FoodListItem(string name, string type, string price)
        {
            Name = name;
            Type = type;
            Price = price;
        }
    }
}
