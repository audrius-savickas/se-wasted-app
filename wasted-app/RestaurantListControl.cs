using console_wasted_app.Controller;
using console_wasted_app.Controller.Entities;
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

            AddAllRestaurants();
        }

        public void AddAllRestaurants()
        {
            ServicesController controller = ServicesController.Instance;
            var restaurants = controller.RestaurantService.GetAllRestaurants();

            foreach (var r in restaurants)
            {
                AddRestaurnt(r);
            }
        }

        private void AddRestaurnt(Restaurant restaurant)
        {
            var control = new RestaurantControl(restaurant);
            restaurantPanel.Controls.Add(control);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainForm.mainForm.panel.Controls.Remove(_instance);
        }
    }
}
