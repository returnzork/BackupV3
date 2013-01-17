namespace returnzork.Backup_V3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.PluginsBTN = new System.Windows.Forms.Button();
            this.WorldToBox = new System.Windows.Forms.TextBox();
            this.WorldToLabel = new System.Windows.Forms.Label();
            this.WorldFromLabel = new System.Windows.Forms.Label();
            this.WorldFromBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TimeBetweenBox = new System.Windows.Forms.TextBox();
            this.SaveBTN = new System.Windows.Forms.Button();
            this.ExcludeBox1 = new System.Windows.Forms.TextBox();
            this.ExcludeBox2 = new System.Windows.Forms.TextBox();
            this.ExcludeBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FinishedSoundCheckBox = new System.Windows.Forms.CheckBox();
            this.WorldOnlyCheckBox = new System.Windows.Forms.CheckBox();
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
            this.SaveBTN.Location = new System.Drawing.Point(194, 12);
            this.SaveBTN.Name = "SaveBTN";
            this.SaveBTN.Size = new System.Drawing.Size(75, 23);
            this.SaveBTN.TabIndex = 7;
            this.SaveBTN.Text = "Save";
            this.SaveBTN.UseVisualStyleBackColor = true;
            this.SaveBTN.Click += new System.EventHandler(this.SaveBTN_Click);
            // 
            // ExcludeBox1
            // 
            this.ExcludeBox1.Location = new System.Drawing.Point(169, 81);
            this.ExcludeBox1.Name = "ExcludeBox1";
            this.ExcludeBox1.Size = new System.Drawing.Size(100, 20);
            this.ExcludeBox1.TabIndex = 8;
            // 
            // ExcludeBox2
            // 
            this.ExcludeBox2.Location = new System.Drawing.Point(169, 134);
            this.ExcludeBox2.Name = "ExcludeBox2";
            this.ExcludeBox2.Size = new System.Drawing.Size(100, 20);
            this.ExcludeBox2.TabIndex = 9;
            // 
            // ExcludeBox3
            // 
            this.ExcludeBox3.Location = new System.Drawing.Point(169, 183);
            this.ExcludeBox3.Name = "ExcludeBox3";
            this.ExcludeBox3.Size = new System.Drawing.Size(100, 20);
            this.ExcludeBox3.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Exclude folder 3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Exclude folder 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(166, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Exclude folder 1";
            // 
            // FinishedSoundCheckBox
            // 
            this.FinishedSoundCheckBox.AutoSize = true;
            this.FinishedSoundCheckBox.Location = new System.Drawing.Point(87, 230);
            this.FinishedSoundCheckBox.Name = "FinishedSoundCheckBox";
            this.FinishedSoundCheckBox.Size = new System.Drawing.Size(123, 17);
            this.FinishedSoundCheckBox.TabIndex = 14;
            this.FinishedSoundCheckBox.Text = "Play finished sound?";
            this.FinishedSoundCheckBox.UseVisualStyleBackColor = true;
            // 
            // WorldOnlyCheckBox
            // 
            this.WorldOnlyCheckBox.AutoSize = true;
            this.WorldOnlyCheckBox.Location = new System.Drawing.Point(87, 209);
            this.WorldOnlyCheckBox.Name = "WorldOnlyCheckBox";
            this.WorldOnlyCheckBox.Size = new System.Drawing.Size(119, 17);
            this.WorldOnlyCheckBox.TabIndex = 15;
            this.WorldOnlyCheckBox.Text = "Backup world only?";
            this.WorldOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 247);
            this.Controls.Add(this.WorldOnlyCheckBox);
            this.Controls.Add(this.FinishedSoundCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ExcludeBox3);
            this.Controls.Add(this.ExcludeBox2);
            this.Controls.Add(this.ExcludeBox1);
            this.Controls.Add(this.SaveBTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TimeBetweenBox);
            this.Controls.Add(this.WorldFromLabel);
            this.Controls.Add(this.WorldFromBox);
            this.Controls.Add(this.WorldToLabel);
            this.Controls.Add(this.WorldToBox);
            this.Controls.Add(this.PluginsBTN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.TextBox ExcludeBox1;
        private System.Windows.Forms.TextBox ExcludeBox2;
        private System.Windows.Forms.TextBox ExcludeBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox FinishedSoundCheckBox;
        private System.Windows.Forms.CheckBox WorldOnlyCheckBox;
    }
}