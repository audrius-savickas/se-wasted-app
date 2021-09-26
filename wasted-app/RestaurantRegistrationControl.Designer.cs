
namespace wasted_app
{
    partial class RestaurantRegistrationControl
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
            this.registerButton = new System.Windows.Forms.Button();
            this.latitudeTextBox = new System.Windows.Forms.TextBox();
            this.restaurantNameTextBox = new System.Windows.Forms.TextBox();
            this.passwordError = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.longitudeTextBox = new System.Windows.Forms.TextBox();
            this.mailTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.repeatPasswordTextBox = new System.Windows.Forms.TextBox();
            this.showRepeatPasswordButton = new System.Windows.Forms.Button();
            this.showPasswordButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // registerButton
            // 
            this.registerButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.registerButton.Location = new System.Drawing.Point(294, 228);
            this.registerButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(100, 24);
            this.registerButton.TabIndex = 12;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // latitudeTextBox
            // 
            this.latitudeTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.latitudeTextBox.ForeColor = System.Drawing.Color.Gray;
            this.latitudeTextBox.Location = new System.Drawing.Point(294, 85);
            this.latitudeTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.latitudeTextBox.Name = "latitudeTextBox";
            this.latitudeTextBox.Size = new System.Drawing.Size(100, 23);
            this.latitudeTextBox.TabIndex = 9;
            this.latitudeTextBox.Text = "Latitude";
            this.latitudeTextBox.Enter += new System.EventHandler(this.latitudeTextBox_Enter);
            this.latitudeTextBox.Leave += new System.EventHandler(this.latitudeTextBox_Leave);
            // 
            // restaurantNameTextBox
            // 
            this.restaurantNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.restaurantNameTextBox.ForeColor = System.Drawing.Color.Gray;
            this.restaurantNameTextBox.Location = new System.Drawing.Point(294, 58);
            this.restaurantNameTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.restaurantNameTextBox.Name = "restaurantNameTextBox";
            this.restaurantNameTextBox.Size = new System.Drawing.Size(100, 23);
            this.restaurantNameTextBox.TabIndex = 8;
            this.restaurantNameTextBox.Text = "Restaurant Name";
            this.restaurantNameTextBox.Enter += new System.EventHandler(this.restaurantNameTextBox_Enter);
            this.restaurantNameTextBox.Leave += new System.EventHandler(this.restaurantNameTextBox_Leave);
            // 
            // passwordError
            // 
            this.passwordError.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.passwordError.Location = new System.Drawing.Point(28, 58);
            this.passwordError.Name = "passwordError";
            this.passwordError.Size = new System.Drawing.Size(202, 197);
            this.passwordError.TabIndex = 14;
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.backButton.Location = new System.Drawing.Point(28, 296);
            this.backButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(44, 25);
            this.backButton.TabIndex = 15;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // longitudeTextBox
            // 
            this.longitudeTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.longitudeTextBox.ForeColor = System.Drawing.Color.Gray;
            this.longitudeTextBox.Location = new System.Drawing.Point(294, 113);
            this.longitudeTextBox.Name = "longitudeTextBox";
            this.longitudeTextBox.Size = new System.Drawing.Size(100, 23);
            this.longitudeTextBox.TabIndex = 16;
            this.longitudeTextBox.Text = "Longitude";
            this.longitudeTextBox.Enter += new System.EventHandler(this.longitudeTextBox_Enter);
            this.longitudeTextBox.Leave += new System.EventHandler(this.longitudeTextBox_Leave);
            // 
            // mailTextBox
            // 
            this.mailTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mailTextBox.ForeColor = System.Drawing.Color.Gray;
            this.mailTextBox.Location = new System.Drawing.Point(294, 142);
            this.mailTextBox.Name = "mailTextBox";
            this.mailTextBox.Size = new System.Drawing.Size(100, 23);
            this.mailTextBox.TabIndex = 17;
            this.mailTextBox.Text = "Mail";
            this.mailTextBox.Enter += new System.EventHandler(this.mailTextBox_Enter);
            this.mailTextBox.Leave += new System.EventHandler(this.mailTextBox_Leave);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.passwordTextBox.ForeColor = System.Drawing.Color.Gray;
            this.passwordTextBox.Location = new System.Drawing.Point(294, 171);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(100, 23);
            this.passwordTextBox.TabIndex = 18;
            this.passwordTextBox.Text = "Password";
            this.passwordTextBox.TextChanged += new System.EventHandler(this.passwordTextBox_TextChanged);
            this.passwordTextBox.Enter += new System.EventHandler(this.passwordTextBox_Enter);
            this.passwordTextBox.Leave += new System.EventHandler(this.passwordTextBox_Leave);
            // 
            // repeatPasswordTextBox
            // 
            this.repeatPasswordTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.repeatPasswordTextBox.ForeColor = System.Drawing.Color.Gray;
            this.repeatPasswordTextBox.Location = new System.Drawing.Point(294, 200);
            this.repeatPasswordTextBox.Name = "repeatPasswordTextBox";
            this.repeatPasswordTextBox.Size = new System.Drawing.Size(100, 23);
            this.repeatPasswordTextBox.TabIndex = 19;
            this.repeatPasswordTextBox.Text = "Repeat Password";
            this.repeatPasswordTextBox.TextChanged += new System.EventHandler(this.repeatPasswordTextBox_TextChanged);
            this.repeatPasswordTextBox.Enter += new System.EventHandler(this.repeatPasswordTextBox_Enter);
            this.repeatPasswordTextBox.Leave += new System.EventHandler(this.repeatPasswordTextBox_Leave);
            // 
            // showRepeatPasswordButton
            // 
            this.showRepeatPasswordButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.showRepeatPasswordButton.Location = new System.Drawing.Point(400, 200);
            this.showRepeatPasswordButton.Name = "showRepeatPasswordButton";
            this.showRepeatPasswordButton.Size = new System.Drawing.Size(24, 23);
            this.showRepeatPasswordButton.TabIndex = 20;
            this.showRepeatPasswordButton.UseVisualStyleBackColor = true;
            this.showRepeatPasswordButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.showRepeatPasswordButton_MouseDown);
            this.showRepeatPasswordButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.showRepeatPasswordButton_MouseUp);
            // 
            // showPasswordButton
            // 
            this.showPasswordButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.showPasswordButton.Location = new System.Drawing.Point(400, 171);
            this.showPasswordButton.Name = "showPasswordButton";
            this.showPasswordButton.Size = new System.Drawing.Size(24, 23);
            this.showPasswordButton.TabIndex = 21;
            this.showPasswordButton.UseVisualStyleBackColor = true;
            this.showPasswordButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.showPasswordButton_MouseDown);
            this.showPasswordButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.showPasswordButton_MouseUp);
            // 
            // RestaurantRegistrationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.showPasswordButton);
            this.Controls.Add(this.showRepeatPasswordButton);
            this.Controls.Add(this.repeatPasswordTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.mailTextBox);
            this.Controls.Add(this.longitudeTextBox);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.passwordError);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.latitudeTextBox);
            this.Controls.Add(this.restaurantNameTextBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "RestaurantRegistrationControl";
            this.Size = new System.Drawing.Size(700, 338);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.TextBox latitudeTextBox;
        private System.Windows.Forms.TextBox restaurantNameTextBox;
        private System.Windows.Forms.Label passwordError;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.TextBox longitudeTextBox;
        private System.Windows.Forms.TextBox mailTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox repeatPasswordTextBox;
        private System.Windows.Forms.Button showRepeatPasswordButton;
        private System.Windows.Forms.Button showPasswordButton;
    }
}
