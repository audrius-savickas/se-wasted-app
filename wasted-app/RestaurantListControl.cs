using console_wasted_app.Controller;
using console_wasted_app.Controller.Entities;
using System;
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
                {
                    _instance = new RestaurantListControl();
                }

                return _instance;
            }
        }
        public RestaurantListControl()
        {
            InitializeComponent();

            AddAllRestaurants();
        }

        public void AddAllRestaurants()
        {
            var controller = ServicesController.Instance;
            var restaurants = controller.RestaurantService.GetAllRestaurants();

            foreach (var r in restaurants)
            {
                AddRestaurant(r);
            }
        }

        private void AddRestaurant(Restaurant restaurant)
        {
            var control = new RestaurantControl(restaurant);
            restaurantPanel.Controls.Add(control);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainForm.mainForm.panel.Controls.Remove(_instance);
        }
    }
}
