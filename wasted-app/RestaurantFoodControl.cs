using console_wasted_app.Controller;
using console_wasted_app.Controller.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            LoggedRestaurant = restaurant;
            InitializeComponent();
            InitializeComboBoxItems();
            GetRestaurantFoodItems();
            ListRestaurantFoodItems(Foods);
        }

        private void GetRestaurantFoodItems()
        {
            Foods = FoodUtilities.GetFoodByRestaurantId(LoggedRestaurant.Id);
        }

        private void ListRestaurantFoodItems(IEnumerable <Food> foods)
        {
            foodPanel.Controls.Clear();
            foreach (var food in Foods)
            {
                var foodItem = new FoodControl(food.Name, FoodUtilities.GetFoodTypeNameById(food.IdTypeOfFood), food.Price.ToString("0.00"));
                foodPanel.Controls.Add(foodItem);
            }
        }

        private void InitializeComboBoxItems()
        {
            var foodTypesNames = FoodUtilities.GetAllFoodTypesNames();
            foreach (var foodTypeName in foodTypesNames)
            {
                typeComboBox.Items.Add(foodTypeName);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainForm.mainForm.panel.Controls.Remove(this);
        }

        private void AddFoodButton_Click(object sender, EventArgs e)
        {
            addFoodPanel.Visible = true;
        }

        private void AddFoodConfirmButton_Click(object sender, EventArgs e)
        {
            var id = FoodUtilities.GetFirstAvailableFoodId();
            var dateTime = DateTime.Now;
            var restaurantId = LoggedRestaurant.Id;
            var name = nameInput.Text;
            var price = decimal.Parse(priceInput.Text);
            var type = FoodUtilities.GetFoodTypeIdByName(typeComboBox.SelectedItem.ToString());
            
            var newFood = new Food(id, name, price, restaurantId, type);
            ServicesController.Instance.FoodService.RegisterFood(newFood);
            GetRestaurantFoodItems();
            ListRestaurantFoodItems(Foods);

            addFoodPanel.Visible = false;
            nameInput.Text = "";
            priceInput.Text = "";
            typeComboBox.SelectedIndex = -1;
        }

        private void PriceInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }
    }
}