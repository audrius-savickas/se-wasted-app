
namespace wasted_app
{
    partial class MainForm
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
            this.panel = new System.Windows.Forms.Panel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // userButton
            // 
            this.userButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.userButton.Location = new System.Drawing.Point(286, 149);
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
            this.restaurantButton.Location = new System.Drawing.Point(338, 225);
            this.restaurantButton.Name = "restaurantButton";
            this.restaurantButton.Size = new System.Drawing.Size(94, 29);
            this.restaurantButton.TabIndex = 1;
            this.restaurantButton.Text = "Restaurant";
            this.restaurantButton.UseVisualStyleBackColor = true;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.userButton);
            this.panel.Controls.Add(this.restaurantButton);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(800, 450);
            this.panel.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button userButton;
        private System.Windows.Forms.Button restaurantButton;
        private System.Windows.Forms.Panel panel;
    }
}

