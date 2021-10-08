﻿using System;
using System.Windows.Forms;

namespace wasted_app
{
    public partial class MainForm : Form
    {
        public static MainForm mainForm;
        public MainForm()
        {
            InitializeComponent();
            mainForm = this;
        }

        private void UserButton_Click(object sender, EventArgs e)
        {
            if (!panel.Controls.Contains(RestaurantListControl.Instance))
            {
                panel.Controls.Add(RestaurantListControl.Instance);
                RestaurantListControl.Instance.Dock = DockStyle.Fill;
                RestaurantListControl.Instance.BringToFront();
            }
            else
            {
                RestaurantListControl.Instance.BringToFront();
            }

        }

        private void RestaurantButton_Click(object sender, EventArgs e)
        {
            if (!panel.Controls.Contains(RestaurantLogInControl.Instance))
            {
                panel.Controls.Add(RestaurantLogInControl.Instance);
                RestaurantLogInControl.Instance.Dock = DockStyle.Fill;
                RestaurantLogInControl.Instance.BringToFront();
            }
            else
            {
                RestaurantLogInControl.Instance.BringToFront();
            }
        }
    }
}