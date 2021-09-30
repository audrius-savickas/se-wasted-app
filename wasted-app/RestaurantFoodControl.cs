using console_wasted_app.Controller;
using console_wasted_app.Controller.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using wasted_app.Utilities;

namespace wasted_app
{
    public partial class RestaurantFoodControl : UserControl
    {
        private Restaurant LoggedRestaurant { get; set; }
        private IEnumerable<Food> Foods { get; set; }
        private readonly ServicesController services = ServicesController.Instance;
        public RestaurantFoodControl(Restaurant restaurant)
        {
            InitializeComponent();
            LoggedRestaurant = restaurant;
            GetRestaurantFoodItems();
            ListRestaurantFoodItems();
        }

        private void GetRestaurantFoodItems()
        {
            Foods = FoodUtilities.GetFoodByRestaurantId(LoggedRestaurant.Id);
        }

        private void ListRestaurantFoodItems()
        {
            foreach (var food in Foods)
            {
                var foodItem = new FoodControl(food.Name, FoodUtilities.GetFoodTypeName(food.IdTypeOfFood), food.Price.ToString("0.00"));
                foodPanel.Controls.Add(foodItem);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainForm.mainForm.panel.Controls.Remove(this);
        }
    }
}