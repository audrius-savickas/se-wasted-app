
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
            this.userButton.Location = new System.Drawing.Point(250, 112);
            this.userButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.userButton.Name = "userButton";
            this.userButton.Size = new System.Drawing.Size(180, 52);
            this.userButton.TabIndex = 0;
            this.userButton.Text = "User";
            this.userButton.UseVisualStyleBackColor = true;
            this.userButton.Click += new System.EventHandler(this.UserButton_Click);
            // 
            // restaurantButton
            // 
            this.restaurantButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.restaurantButton.Location = new System.Drawing.Point(296, 169);
            this.restaurantButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.restaurantButton.Name = "restaurantButton";
            this.restaurantButton.Size = new System.Drawing.Size(82, 22);
            this.restaurantButton.TabIndex = 1;
            this.restaurantButton.Text = "Restaurant";
            this.restaurantButton.UseVisualStyleBackColor = true;
            this.restaurantButton.Click += new System.EventHandler(this.restaurantButton_Click);
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Controls.Add(this.userButton);
            this.panel.Controls.Add(this.restaurantButton);
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(700, 338);
            this.panel.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 338);
            this.Controls.Add(this.panel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button userButton;
        private System.Windows.Forms.Button restaurantButton;
        public System.Windows.Forms.Panel panel;
    }
}

