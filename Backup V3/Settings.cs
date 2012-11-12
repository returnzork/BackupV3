using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using returnzork.XmlReader;

namespace Backup_V3
{
    public partial class Settings : Form
    {
        XmlReader xml;
        public Settings()
        {
            InitializeComponent();
            xml = new XmlReader(Environment.GetEnvironmentVariable("APPDATA") + "\\returnzork\\BackupV3\\Settings.config");
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
        }

        private void SaveBTN_Click(object sender, EventArgs e)
        {
            xml.SaveKey("WorldTo", WorldToBox.Text);
            xml.SaveKey("WorldFrom", WorldFromBox.Text);
            xml.SaveKey("TimeBetween", TimeBetweenBox.Text);
        }
    }
}
