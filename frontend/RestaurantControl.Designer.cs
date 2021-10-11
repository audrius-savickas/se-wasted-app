
namespace wasted_app
{
    partial class RestaurantControl
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
            this.nameButton = new System.Windows.Forms.Label();
            this.addressButton = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameButton
            // 
            this.nameButton.AutoSize = true;
            this.nameButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nameButton.Location = new System.Drawing.Point(13, 9);
            this.nameButton.Name = "nameButton";
            this.nameButton.Size = new System.Drawing.Size(245, 41);
            this.nameButton.TabIndex = 0;
            this.nameButton.Text = "Restaurant Name";
            // 
            // addressButton
            // 
            this.addressButton.AutoSize = true;
            this.addressButton.Location = new System.Drawing.Point(13, 50);
            this.addressButton.Name = "addressButton";
            this.addressButton.Size = new System.Drawing.Size(62, 20);
            this.addressButton.TabIndex = 1;
            this.addressButton.Text = "Address";
            // 
            // RestaurantControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Controls.Add(this.addressButton);
            this.Controls.Add(this.nameButton);
            this.Name = "RestaurantControl";
            this.Size = new System.Drawing.Size(433, 97);
            this.Click += new System.EventHandler(this.RestaurantControl_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameButton;
        private System.Windows.Forms.Label addressButton;
    }
}
