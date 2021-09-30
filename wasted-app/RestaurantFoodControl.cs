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
        private IEnumerable<Food> foods { get; set; }
        private readonly ServicesController services = ServicesController.Instance;
        public RestaurantFoodControl(Restaurant restaurant)
        {
            InitializeComponent();
            LoggedRestaurant = restaurant;
            getRestaurantFoodItems();
            listRestaurantFoodItems();
        }

        private void getRestaurantFoodItems()
        {
            foods = FoodUtilities.GetFoodByRestaurantId(LoggedRestaurant.Id);
        }

        private void listRestaurantFoodItems()
        {
            foreach (var food in foods)
            {
                var foodItem = new FoodControl(food.Name, FoodUtilities.GetFoodTypeName(food.IdTypeOfFood), food.Price.ToString("0.00"));
                foodPanel.Controls.Add(foodItem);
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainForm.mainForm.panel.Controls.Remove(this);
        }
    }
}