
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
            this.showPasswordCheckbox = new System.Windows.Forms.CheckBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordInput = new System.Windows.Forms.TextBox();
            this.usernameInput = new System.Windows.Forms.TextBox();
            this.passwordError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // showPasswordCheckbox
            // 
            this.showPasswordCheckbox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.showPasswordCheckbox.AutoSize = true;
            this.showPasswordCheckbox.Location = new System.Drawing.Point(650, 325);
            this.showPasswordCheckbox.Name = "showPasswordCheckbox";
            this.showPasswordCheckbox.Size = new System.Drawing.Size(134, 24);
            this.showPasswordCheckbox.TabIndex = 13;
            this.showPasswordCheckbox.Text = "Show password";
            this.showPasswordCheckbox.UseVisualStyleBackColor = true;
            this.showPasswordCheckbox.CheckedChanged += new System.EventHandler(this.showPasswordCheckbox_CheckedChanged);
            // 
            // registerButton
            // 
            this.registerButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.registerButton.Location = new System.Drawing.Point(454, 384);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(94, 29);
            this.registerButton.TabIndex = 12;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(378, 302);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(73, 20);
            this.passwordLabel.TabIndex = 11;
            this.passwordLabel.Text = "Password:";
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(378, 228);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(78, 20);
            this.usernameLabel.TabIndex = 10;
            this.usernameLabel.Text = "Username:";
            // 
            // passwordInput
            // 
            this.passwordInput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.passwordInput.Location = new System.Drawing.Point(378, 325);
            this.passwordInput.Name = "passwordInput";
            this.passwordInput.Size = new System.Drawing.Size(266, 27);
            this.passwordInput.TabIndex = 9;
            this.passwordInput.UseSystemPasswordChar = true;
            this.passwordInput.TextChanged += new System.EventHandler(this.passwordInput_TextChanged);
            // 
            // usernameInput
            // 
            this.usernameInput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.usernameInput.Location = new System.Drawing.Point(378, 251);
            this.usernameInput.Name = "usernameInput";
            this.usernameInput.Size = new System.Drawing.Size(266, 27);
            this.usernameInput.TabIndex = 8;
            this.usernameInput.TextChanged += new System.EventHandler(this.usernameInput_TextChanged);
            // 
            // passwordError
            // 
            this.passwordError.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.passwordError.Location = new System.Drawing.Point(139, 329);
            this.passwordError.Name = "passwordError";
            this.passwordError.Size = new System.Drawing.Size(231, 263);
            this.passwordError.TabIndex = 14;
            // 
            // RestaurantRegistrationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.passwordError);
            this.Controls.Add(this.showPasswordCheckbox);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.passwordInput);
            this.Controls.Add(this.usernameInput);
            this.Name = "RestaurantRegistrationControl";
            this.Size = new System.Drawing.Size(1061, 635);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox showPasswordCheckbox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox passwordInput;
        private System.Windows.Forms.TextBox usernameInput;
        private System.Windows.Forms.Label passwordError;
    }
}
