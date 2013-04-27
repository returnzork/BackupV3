using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace returnzork.Backup_V3
{
    public partial class Settings : Form
    {

        Properties.MainSettings ms = new Properties.MainSettings();

        //XmlSettings xml;

        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Plugins pl = new Plugins();
            pl.Show();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            WorldToBox.Text = ms.WorldTo;

            WorldFromBox.Text = ms.WorldFrom;
            TimeBetweenBox.Text = Convert.ToString(ms.TimeBetween);

            ExcludeBox1.Text = ms.ExcludeFolder1;
            ExcludeBox2.Text = ms.ExcludeFolder2;
            ExcludeBox3.Text = ms.ExcludeFolder3;

            if(ms.PlayFinishedSound)
                FinishedSoundCheckBox.Checked = true;

            if (ms.WorldOnly)
                WorldOnlyCheckBox.Checked = true;
        }

        private void SaveBTN_Click(object sender, EventArgs e)
        {
            ms.WorldTo = WorldToBox.Text;
            ms.WorldFrom = WorldFromBox.Text;
            ms.TimeBetween = Convert.ToInt32(TimeBetweenBox.Text);

            ms.ExcludeFolder1 = ExcludeBox1.Text;
            ms.ExcludeFolder2 = ExcludeBox2.Text;
            ms.ExcludeFolder3 = ExcludeBox2.Text;


            if (FinishedSoundCheckBox.Checked)
                ms.PlayFinishedSound = true;
            else
                ms.PlayFinishedSound = false;

            if (WorldOnlyCheckBox.Checked)
                ms.WorldOnly = true;
            else
                ms.WorldOnly = false;
        }
    }
}