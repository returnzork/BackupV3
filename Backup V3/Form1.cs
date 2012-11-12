﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using BackupV3_API;
using System.IO;
using returnzork.XmlReader;
using System.Reflection;

namespace Backup_V3
{
    public partial class Form1 : Form
    {
        [ImportMany]
        public IEnumerable<BackupV3API> plugins { get; set; }


        string SettingsFolder;
        string PluginsFolder;
        string[] Imports = { "", "", "", "" };
        XmlReader xml;

        public Form1()
        {
            InitializeComponent();
            SettingsFolder = Environment.GetEnvironmentVariable("APPDATA") + "\\returnzork\\BackupV3\\";
            PluginsFolder = SettingsFolder + "Plugins\\";

            Check();
            LoadPlugins();
            AddPluginInterface();

            xml = new XmlReader(SettingsFolder + "Settings.config");
            
        }

        private void Check()
        {
            if (!Directory.Exists(SettingsFolder))
            {
                Directory.CreateDirectory(SettingsFolder);
                Directory.CreateDirectory(PluginsFolder);
            }
            if(!File.Exists(SettingsFolder + "Settings.config"))
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Backup_V3.Settings.config");
                FileStream fs = new FileStream(SettingsFolder + "Settings.config", FileMode.CreateNew);
                for (int i = 0; i < stream.Length; i++)
                    fs.WriteByte((byte)stream.ReadByte());
                fs.Close();
            }
        }
        
        public void LoadPlugins()
        {
            try
            {
                AggregateCatalog catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new DirectoryCatalog(PluginsFolder));
                CompositionContainer container = new CompositionContainer(catalog);
                container.ComposeParts(this);
            }
            catch (FileNotFoundException fnfex)
            {
                MessageBox.Show(fnfex.Message);
            }
            catch (CompositionException cex)
            {
                MessageBox.Show(cex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

#if DEBUG
            foreach (var plugin in plugins)
            {
                MessageBox.Show(plugin.Name() + " by " + plugin.Author() + " has loaded."); 
            }
#endif
        }

        private void SettingsBTN_click(object sender, EventArgs e)
        {
            Settings set = new Settings();
            set.Show();
        }

        bool isAdding;

        private void AddPluginInterface()
        {
            pluginsToolStripMenuItem.DropDown.Items.Clear();
            foreach (var plugin in plugins)
            {
                isAdding = true;
                pluginsToolStripMenuItem.DropDown.ItemClicked += new ToolStripItemClickedEventHandler(ToolStripClick);
                pluginsToolStripMenuItem.DropDown.Items.Add(plugin.Name());
            }
            isAdding = false;
        }

        private void ToolStripClick(object sender, ToolStripItemClickedEventArgs e)
        {
            if (!isAdding)
            {
                foreach (var pl in plugins)
                {
                    if (pl.Name() == e.ClickedItem.ToString())
                    {
                        pl.Interface();
                        break;
                    }
                }
            }
        }

        BackgroundWorker CopyWorker = new BackgroundWorker();


        private void StartBTN_Click(object sender, EventArgs e)
        {
            CopyWorker.DoWork += new  DoWorkEventHandler(CopyWorker_DoWork);
            CopyWorker.WorkerReportsProgress = true;
            CopyWorker.ProgressChanged += new ProgressChangedEventHandler(CopyWorker_ProgressChange);
            CopyWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CopyWorker_Completed);
            CopyWorker.RunWorkerAsync();
        }

        DateTime Started;// = DateTime.Now;
        DateTime End;// = DateTime.Now.AddMinutes(2);

        private void CopyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CopyWorker.ReportProgress(5);
            Started = DateTime.Now;     //set the start time to the current time
            End = DateTime.Now.AddMinutes(Convert.ToInt32(xml.GetKey("TimeBetween")));  //add the time between value to the current time
            while(Started < End)    //while when it started is less than the end time
            {
                System.Threading.Thread.Sleep(100);
                Started = DateTime.Now;
            }
            CopyWorker.ReportProgress(10);


            if (!Directory.Exists(xml.GetKey("WorldTo")))
            {
                Directory.CreateDirectory(xml.GetKey("WorldTo"));
            }

            string dt = DateTime.Now.ToString("MM.dd.yyyy  hh-mm-ss");

            if (!Directory.Exists(xml.GetKey("WorldTo") + dt))
            {
                Directory.CreateDirectory(xml.GetKey("WorldTo") + dt);
            }

            foreach (string DirCreate in Directory.GetDirectories(xml.GetKey("WorldFrom"), "*", SearchOption.AllDirectories))
            {
                string DIR = DirCreate.Replace(xml.GetKey("WorldFrom"), xml.GetKey("WorldTo") + dt + "\\");

                Directory.CreateDirectory(DIR);

                foreach (string file in Directory.GetFiles(DirCreate))
                {
                    string NewFile = file.Replace(xml.GetKey("WorldFrom"), xml.GetKey("WorldTo") + "\\" + dt + "\\");
                    File.Copy(file, NewFile);
                }
            }
            CopyWorker.ReportProgress(35);
            foreach (string file in Directory.GetFiles(xml.GetKey("WorldFrom")))
            {
                string NewFile = file.Replace(xml.GetKey("WorldFrom"), xml.GetKey("WorldTo") + "\\" + dt + "\\");
                File.Copy(file, NewFile);
            }
            CopyWorker.ReportProgress(75);
            


            UpdateImports();


            foreach (var plugin in plugins)
            {
                plugin.Work(Imports);   //do work for each plugin
            }
            CopyWorker.ReportProgress(100);
        }

        private void UpdateImports()
        {
            Imports[0] = xml.GetKey("WorldFrom");
            Imports[1] = xml.GetKey("WorldTo");
            Imports[2] = xml.GetKey("TimeBetween");
        }

        private void CopyWorker_ProgressChange(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void CopyWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}