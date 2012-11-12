namespace Backup_V3
{
    partial class Settings
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PluginsBTN = new System.Windows.Forms.Button();
            this.WorldToBox = new System.Windows.Forms.TextBox();
            this.WorldToLabel = new System.Windows.Forms.Label();
            this.WorldFromLabel = new System.Windows.Forms.Label();
            this.WorldFromBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TimeBetweenBox = new System.Windows.Forms.TextBox();
            this.SaveBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PluginsBTN
            // 
            this.PluginsBTN.Location = new System.Drawing.Point(12, 12);
            this.PluginsBTN.Name = "PluginsBTN";
            this.PluginsBTN.Size = new System.Drawing.Size(75, 23);
            this.PluginsBTN.TabIndex = 0;
            this.PluginsBTN.Text = "Plugins";
            this.PluginsBTN.UseVisualStyleBackColor = true;
            this.PluginsBTN.Click += new System.EventHandler(this.button1_Click);
            // 
            // WorldToBox
            // 
            this.WorldToBox.Location = new System.Drawing.Point(15, 134);
            this.WorldToBox.Name = "WorldToBox";
            this.WorldToBox.Size = new System.Drawing.Size(100, 20);
            this.WorldToBox.TabIndex = 1;
            // 
            // WorldToLabel
            // 
            this.WorldToLabel.AutoSize = true;
            this.WorldToLabel.Location = new System.Drawing.Point(12, 118);
            this.WorldToLabel.Name = "WorldToLabel";
            this.WorldToLabel.Size = new System.Drawing.Size(50, 13);
            this.WorldToLabel.TabIndex = 2;
            this.WorldToLabel.Text = "World to:";
            // 
            // WorldFromLabel
            // 
            this.WorldFromLabel.AutoSize = true;
            this.WorldFromLabel.Location = new System.Drawing.Point(12, 65);
            this.WorldFromLabel.Name = "WorldFromLabel";
            this.WorldFromLabel.Size = new System.Drawing.Size(61, 13);
            this.WorldFromLabel.TabIndex = 4;
            this.WorldFromLabel.Text = "World from:";
            // 
            // WorldFromBox
            // 
            this.WorldFromBox.Location = new System.Drawing.Point(15, 81);
            this.WorldFromBox.Name = "WorldFromBox";
            this.WorldFromBox.Size = new System.Drawing.Size(100, 20);
            this.WorldFromBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Time between backups:";
            // 
            // TimeBetweenBox
            // 
            this.TimeBetweenBox.Location = new System.Drawing.Point(15, 183);
            this.TimeBetweenBox.Name = "TimeBetweenBox";
            this.TimeBetweenBox.Size = new System.Drawing.Size(100, 20);
            this.TimeBetweenBox.TabIndex = 5;
            // 
            // SaveBTN
            // 
            this.SaveBTN.Location = new System.Drawing.Point(167, 81);
            this.SaveBTN.Name = "SaveBTN";
            this.SaveBTN.Size = new System.Drawing.Size(75, 23);
            this.SaveBTN.TabIndex = 7;
            this.SaveBTN.Text = "Save";
            this.SaveBTN.UseVisualStyleBackColor = true;
            this.SaveBTN.Click += new System.EventHandler(this.SaveBTN_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 241);
            this.Controls.Add(this.SaveBTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TimeBetweenBox);
            this.Controls.Add(this.WorldFromLabel);
            this.Controls.Add(this.WorldFromBox);
            this.Controls.Add(this.WorldToLabel);
            this.Controls.Add(this.WorldToBox);
            this.Controls.Add(this.PluginsBTN);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button PluginsBTN;
        private System.Windows.Forms.TextBox WorldToBox;
        private System.Windows.Forms.Label WorldToLabel;
        private System.Windows.Forms.Label WorldFromLabel;
        private System.Windows.Forms.TextBox WorldFromBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TimeBetweenBox;
        private System.Windows.Forms.Button SaveBTN;
    }
}