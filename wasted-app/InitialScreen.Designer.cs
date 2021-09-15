
namespace wasted_app
{
    partial class InitialScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.userButton = new System.Windows.Forms.Button();
            this.restaurantButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userButton
            // 
            this.userButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.userButton.Location = new System.Drawing.Point(274, 181);
            this.userButton.Name = "userButton";
            this.userButton.Size = new System.Drawing.Size(206, 70);
            this.userButton.TabIndex = 0;
            this.userButton.Text = "User";
            this.userButton.UseVisualStyleBackColor = true;
            this.userButton.Click += new System.EventHandler(this.UserButton_Click);
            // 
            // restaurantButton
            // 
            this.restaurantButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.restaurantButton.Location = new System.Drawing.Point(328, 257);
            this.restaurantButton.Name = "restaurantButton";
            this.restaurantButton.Size = new System.Drawing.Size(94, 29);
            this.restaurantButton.TabIndex = 1;
            this.restaurantButton.Text = "Restaurant";
            this.restaurantButton.UseVisualStyleBackColor = true;
            // 
            // InitialScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.restaurantButton);
            this.Controls.Add(this.userButton);
            this.Name = "InitialScreen";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button userButton;
        private System.Windows.Forms.Button restaurantButton;
    }
}

