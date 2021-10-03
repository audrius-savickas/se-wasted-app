
namespace wasted_app
{
    partial class RestaurantFoodControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.addFoodPanel = new System.Windows.Forms.Panel();
            this.dollarSignLabel = new System.Windows.Forms.Label();
            this.foodPriceLabel = new System.Windows.Forms.Label();
            this.addFoodConfirmButton = new System.Windows.Forms.Button();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.priceInput = new System.Windows.Forms.TextBox();
            this.nameInput = new System.Windows.Forms.TextBox();
            this.foodTypeLabel = new System.Windows.Forms.Label();
            this.foodNameLabel = new System.Windows.Forms.Label();
            this.addFoodButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.sortButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.foodPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsPanel.SuspendLayout();
            this.addFoodPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsPanel
            // 
            this.settingsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.settingsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.settingsPanel.Controls.Add(this.addFoodPanel);
            this.settingsPanel.Controls.Add(this.addFoodButton);
            this.settingsPanel.Controls.Add(this.filterButton);
            this.settingsPanel.Controls.Add(this.sortButton);
            this.settingsPanel.Controls.Add(this.backButton);
            this.settingsPanel.Location = new System.Drawing.Point(0, 0);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(263, 451);
            this.settingsPanel.TabIndex = 0;
            // 
            // addFoodPanel
            // 
            this.addFoodPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addFoodPanel.Controls.Add(this.dollarSignLabel);
            this.addFoodPanel.Controls.Add(this.foodPriceLabel);
            this.addFoodPanel.Controls.Add(this.addFoodConfirmButton);
            this.addFoodPanel.Controls.Add(this.typeComboBox);
            this.addFoodPanel.Controls.Add(this.priceInput);
            this.addFoodPanel.Controls.Add(this.nameInput);
            this.addFoodPanel.Controls.Add(this.foodTypeLabel);
            this.addFoodPanel.Controls.Add(this.foodNameLabel);
            this.addFoodPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addFoodPanel.Location = new System.Drawing.Point(17, 160);
            this.addFoodPanel.Name = "addFoodPanel";
            this.addFoodPanel.Size = new System.Drawing.Size(230, 241);
            this.addFoodPanel.TabIndex = 4;
            this.addFoodPanel.Visible = false;
            // 
            // dollarSignLabel
            // 
            this.dollarSignLabel.AutoSize = true;
            this.dollarSignLabel.Font = new System.Drawing.Font("Segoe UI", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dollarSignLabel.Location = new System.Drawing.Point(101, 83);
            this.dollarSignLabel.Name = "dollarSignLabel";
            this.dollarSignLabel.Size = new System.Drawing.Size(19, 23);
            this.dollarSignLabel.TabIndex = 7;
            this.dollarSignLabel.Text = "$";
            this.dollarSignLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // foodPriceLabel
            // 
            this.foodPriceLabel.AutoSize = true;
            this.foodPriceLabel.Font = new System.Drawing.Font("Segoe UI", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.foodPriceLabel.Location = new System.Drawing.Point(10, 56);
            this.foodPriceLabel.Name = "foodPriceLabel";
            this.foodPriceLabel.Size = new System.Drawing.Size(47, 23);
            this.foodPriceLabel.TabIndex = 5;
            this.foodPriceLabel.Text = "Price";
            // 
            // addFoodConfirmButton
            // 
            this.addFoodConfirmButton.Location = new System.Drawing.Point(60, 188);
            this.addFoodConfirmButton.Name = "addFoodConfirmButton";
            this.addFoodConfirmButton.Size = new System.Drawing.Size(94, 29);
            this.addFoodConfirmButton.TabIndex = 5;
            this.addFoodConfirmButton.Text = "Confirm";
            this.addFoodConfirmButton.UseVisualStyleBackColor = true;
            this.addFoodConfirmButton.Click += new System.EventHandler(this.AddFoodConfirmButton_Click);
            // 
            // typeComboBox
            // 
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(12, 138);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(196, 28);
            this.typeComboBox.TabIndex = 4;
            // 
            // priceInput
            // 
            this.priceInput.Location = new System.Drawing.Point(12, 82);
            this.priceInput.Name = "priceInput";
            this.priceInput.Size = new System.Drawing.Size(88, 27);
            this.priceInput.TabIndex = 3;
            this.priceInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PriceInput_KeyPress);
            // 
            // nameInput
            // 
            this.nameInput.Location = new System.Drawing.Point(12, 26);
            this.nameInput.Name = "nameInput";
            this.nameInput.Size = new System.Drawing.Size(196, 27);
            this.nameInput.TabIndex = 2;
            // 
            // foodTypeLabel
            // 
            this.foodTypeLabel.AutoSize = true;
            this.foodTypeLabel.Font = new System.Drawing.Font("Segoe UI", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.foodTypeLabel.Location = new System.Drawing.Point(12, 112);
            this.foodTypeLabel.Name = "foodTypeLabel";
            this.foodTypeLabel.Size = new System.Drawing.Size(45, 23);
            this.foodTypeLabel.TabIndex = 1;
            this.foodTypeLabel.Text = "Type";
            // 
            // foodNameLabel
            // 
            this.foodNameLabel.AutoSize = true;
            this.foodNameLabel.Font = new System.Drawing.Font("Segoe UI", 9.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.foodNameLabel.Location = new System.Drawing.Point(12, 0);
            this.foodNameLabel.Name = "foodNameLabel";
            this.foodNameLabel.Size = new System.Drawing.Size(56, 23);
            this.foodNameLabel.TabIndex = 0;
            this.foodNameLabel.Text = "Name";
            // 
            // addFoodButton
            // 
            this.addFoodButton.Location = new System.Drawing.Point(63, 176);
            this.addFoodButton.Name = "addFoodButton";
            this.addFoodButton.Size = new System.Drawing.Size(147, 40);
            this.addFoodButton.TabIndex = 3;
            this.addFoodButton.Text = "Add new food";
            this.addFoodButton.UseVisualStyleBackColor = true;
            this.addFoodButton.Click += new System.EventHandler(this.AddFoodButton_Click);
            // 
            // filterButton
            // 
            this.filterButton.Location = new System.Drawing.Point(90, 71);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(94, 29);
            this.filterButton.TabIndex = 2;
            this.filterButton.Text = "Filter";
            this.filterButton.UseVisualStyleBackColor = true;
            // 
            // sortButton
            // 
            this.sortButton.Location = new System.Drawing.Point(90, 36);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(94, 29);
            this.sortButton.TabIndex = 1;
            this.sortButton.Text = "Sort";
            this.sortButton.UseVisualStyleBackColor = true;
            // 
            // backButton
            // 
            this.backButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.backButton.Location = new System.Drawing.Point(153, 407);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(94, 29);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // foodPanel
            // 
            this.foodPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.foodPanel.AutoScroll = true;
            this.foodPanel.Location = new System.Drawing.Point(266, 0);
            this.foodPanel.Name = "foodPanel";
            this.foodPanel.Size = new System.Drawing.Size(534, 451);
            this.foodPanel.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // RestaurantFoodControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.foodPanel);
            this.Controls.Add(this.settingsPanel);
            this.Name = "RestaurantFoodControl";
            this.Size = new System.Drawing.Size(800, 451);
            this.settingsPanel.ResumeLayout(false);
            this.addFoodPanel.ResumeLayout(false);
            this.addFoodPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button sortButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.FlowLayoutPanel foodPanel;
        private System.Windows.Forms.Button addFoodButton;
        private System.Windows.Forms.Panel addFoodPanel;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.TextBox nameInput;
        private System.Windows.Forms.Label foodPriceLabel;
        private System.Windows.Forms.Label dollarSignLabel;
        private System.Windows.Forms.TextBox priceInput;
        private System.Windows.Forms.Label foodTypeLabel;
        private System.Windows.Forms.Label foodNameLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button addFoodConfirmButton;
    }
}
