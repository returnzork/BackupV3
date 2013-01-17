using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using returnzork.BackupV3_API.XmlSettings;

namespace returnzork.Backup_V3
{
    public partial class Settings : Form
    {
        XmlSettings xml;

        public Settings()
        {
            InitializeComponent();
            xml = new XmlSettings(Environment.GetEnvironmentVariable("APPDATA") + "\\returnzork\\BackupV3\\Settings.config");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Plugins pl = new Plugins();
            pl.Show();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            WorldToBox.Text = xml.GetKey("WorldTo");
            WorldFromBox.Text = xml.GetKey("WorldFrom");
            TimeBetweenBox.Text = xml.GetKey("TimeBetween");

            ExcludeBox1.Text = xml.GetKey("ExcludeFolder1");
            ExcludeBox2.Text = xml.GetKey("ExcludeFolder2");
            ExcludeBox3.Text = xml.GetKey("ExcludeFolder3");

            if (xml.GetKey("PlayFinishedSound") == "true")
                FinishedSoundCheckBox.Checked = true;

            if (xml.GetKey("WorldOnly") == "true")
                WorldOnlyCheckBox.Checked = true;
        }

        private void SaveBTN_Click(object sender, EventArgs e)
        {
            xml.SaveKey("WorldTo", WorldToBox.Text);
            xml.SaveKey("WorldFrom", WorldFromBox.Text);
            xml.SaveKey("TimeBetween", TimeBetweenBox.Text);

            xml.SaveKey("ExcludeFolder1", ExcludeBox1.Text);
            xml.SaveKey("ExcludeFolder2", ExcludeBox2.Text);
            xml.SaveKey("ExcludeFolder3", ExcludeBox3.Text);

            if (FinishedSoundCheckBox.Checked)
                xml.SaveKey("PlayFinishedSound", "true");
            else
                xml.SaveKey("PlayFinishedSound", "false");

            if (WorldOnlyCheckBox.Checked)
                xml.SaveKey("WorldOnly", "true");
            else
                xml.SaveKey("WorldOnly", "false");
        }
    }
}
