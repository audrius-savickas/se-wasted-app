using console_wasted_app.Controller.Entities;
using System;
using System.Windows.Forms;

namespace wasted_app
{
    public partial class RestaurantControl : UserControl
    {
        private readonly Restaurant _restaurant;
        public RestaurantControl()
        {
            InitializeComponent();
        }

        public RestaurantControl(Restaurant restaurant) : this()
        {
            _restaurant = restaurant;
            nameButton.Text = _restaurant.Name;
        }

        private void RestaurantControl_Click(object sender, EventArgs e)
        {
            var restaurantViewFoodScreen = new RestaurantViewFoodControl(_restaurant);
            MainForm.mainForm.panel.Controls.Add(restaurantViewFoodScreen);
            restaurantViewFoodScreen.Dock = DockStyle.Fill;
            restaurantViewFoodScreen.BringToFront();
        }
    }
}
