using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

using returnzork.BackupV3_API;
using returnzork.BackupV3_API.XmlSettings;
using returnzork.ErrorLogging;

namespace returnzork.Backup_V3
{
    public partial class Form1 : Form
    {
        [ImportMany]
        public IEnumerable<BackupV3API> plugins { get; set; } 

        string SettingsFolder;
        string PluginsFolder;
        public string[] Imports = { "", "", "", "" };
        XmlSettings xml;

        ErrorLogger logger;

        bool ShouldIStop = false;


        string[] ExcludeFolders = { };


        string[] Keys = { "WorldFrom", "WorldTo", "TimeBetween", "ExcludeFolder1", "ExcludeFolder2", "ExcludeFolder3", "PlayFinishedSound", "WorldOnly" };



        public Form1()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            SettingsFolder = Environment.GetEnvironmentVariable("APPDATA") + "\\returnzork\\BackupV3\\";
            PluginsFolder = SettingsFolder + "Plugins\\";

            StopBtn.Enabled = false;
            StopBtn.Visible = false;


            xml = new XmlSettings(SettingsFolder + "Settings.config");


            logger = new ErrorLogger(SettingsFolder + "Error.log");

            Check();
            LoadPlugins();

            foreach (var plugin in plugins)
                plugin.Initialize();


            AddPluginInterface();
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
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("returnzork.Backup_V3.Settings.config");
                FileStream fs = new FileStream(SettingsFolder + "Settings.config", FileMode.CreateNew);
                for (int i = 0; i < stream.Length; i++)
                    fs.WriteByte((byte)stream.ReadByte());
                fs.Close();
            }
            else
                CheckForNewSettings();


            if (!Directory.Exists(SettingsFolder + "PluginConfig"))
                Directory.CreateDirectory(SettingsFolder + "PluginConfig");
            if (!Directory.Exists(SettingsFolder + "PluginLib"))
                Directory.CreateDirectory(SettingsFolder + "PluginLib");
        }

        private void CheckForNewSettings()
        {
            //TODO add keys if they do not exist

            string[] AllKeys = xml.GetAllKeys();

            foreach (string s in Keys)
            {
                if (AllKeys.Contains(s))
                    continue;
                else
                {
                    //Create Key
                    xml.CreateKey(s);
                }
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
                logger.MakeLog(fnfex);
            }
            catch (CompositionException cex)
            {
                logger.MakeLog(cex);
            }
            catch (Exception ex)
            {
                logger.MakeLog(ex);
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
                isAdding = true;    //used to make sure you are unable to click any toolstrip item until it is finished adding all the plugins to it
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
            CopyWorker.WorkerSupportsCancellation = true;
            CopyWorker.RunWorkerAsync();


            StartBTN.Visible = false;
            StartBTN.Enabled = false;

            StopBtn.Enabled = true;
            StopBtn.Visible = true;

            ShouldIStop = false;
        }


        private void StopBtn_Click(object sender, EventArgs e)
        {
            ShouldIStop = true;

            StartBTN.Enabled = true;
            StartBTN.Visible = true;

            StopBtn.Visible = false;
            StopBtn.Enabled = false;
        }

        private void UpdateExcludeFolders()
        {
            if (xml.GetKey("ExcludeFolder1") != "")
            {
                Array.Resize(ref ExcludeFolders, ExcludeFolders.Length + 1);
                ExcludeFolders[ExcludeFolders.Length - 1] = xml.GetKey("ExcludeFolder1");
            }
            if (xml.GetKey("ExcludeFolder2") != "")
            {
                Array.Resize(ref ExcludeFolders, ExcludeFolders.Length + 1);
                ExcludeFolders[ExcludeFolders.Length - 1] = xml.GetKey("ExcludeFolder2");
            }
            if(xml.GetKey("ExcludeFolder3") != "")
            {
                Array.Resize(ref ExcludeFolders, ExcludeFolders.Length + 1);
                ExcludeFolders[ExcludeFolders.Length - 1] = xml.GetKey("ExcludeFolder3");
            }
        }

        DateTime Started, End;

        private void CopyWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            if (!Directory.Exists(xml.GetKey("WorldFrom")))
            {
                MessageBox.Show("Directory \"" + xml.GetKey("WorldFrom") +"\" does not exist, cannot continue.");
                logger.MakeLog("Directory does not exist");
                ShouldIStop = true;
                CopyWorker.ReportProgress(0);
                return;
            }

            CopyWorker.ReportProgress(5);
            Started = DateTime.Now;     //set the start time to the current time
            End = DateTime.Now.AddMinutes(Convert.ToInt32(xml.GetKey("TimeBetween")));  //add the time between value to the current time
            while(Started < End)    //while when it started is less than the end time
            {
                System.Threading.Thread.Sleep(100);
                Started = DateTime.Now;

                if (CopyWorker.CancellationPending)
                    break;

                if (Convert.ToInt32(End.Minute) - Convert.ToInt32(Started.Minute) < 0)
                {
                    int ne = Math.Abs(Convert.ToInt32(End.Minute) - Convert.ToInt32(Started.Minute));                   

                    ne = Convert.ToInt32(xml.GetKey("TimeBetween")) - ne;

                    int newtime = Convert.ToInt32(xml.GetKey("TimeBetween")) + ne;


                    Invoke((MethodInvoker)delegate { TimeRemainingTextBox.Text = newtime + " minutes remaining"; });
                }
                else
                    Invoke((MethodInvoker)delegate { TimeRemainingTextBox.Text = Convert.ToInt32(End.Minute) - Convert.ToInt32(Started.Minute) + " minutes left"; });


                CopyWorker.ReportProgress(6);
            }


            if (CopyWorker.CancellationPending)
                return;

            UpdateExcludeFolders();
            CopyWorker.ReportProgress(10);



            if (!Directory.Exists(xml.GetKey("WorldTo")))
            {
                Directory.CreateDirectory(xml.GetKey("WorldTo"));
            }

            string dt = DateTime.Now.ToString("MM.dd.yyyy  hh-mm-ss tt");

            if (!Directory.Exists(xml.GetKey("WorldTo") + dt))
            {
                Directory.CreateDirectory(xml.GetKey("WorldTo") + dt);
            }


            string WorldOnly = xml.GetKey("WorldOnly");

            if (WorldOnly == "false")
            {
                foreach (string DirCreate in Directory.GetDirectories(xml.GetKey("WorldFrom"), "*", SearchOption.AllDirectories))
                {
                    string DIR = DirCreate.Replace(xml.GetKey("WorldFrom"), xml.GetKey("WorldTo") + dt + "\\");
                    bool ShouldIContinue = true;

                    foreach (string s in ExcludeFolders)
                    {
                        if (DirCreate.Contains(s))
                        {
                            ShouldIContinue = false;
                            continue;
                        }
                    }

                    if (!ShouldIContinue)
                        continue;

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
            }
            else
            {
                foreach (string dir in Directory.GetDirectories(xml.GetKey("WorldFrom") + "\\world\\", "*", SearchOption.AllDirectories))
                {
                    string DIR = dir.Replace(xml.GetKey("WorldFrom"), xml.GetKey("WorldTo") + dt + "\\");
                    Directory.CreateDirectory(DIR);

                    foreach (string file in Directory.GetFiles(dir))
                    {
                        string NewFile = file.Replace(xml.GetKey("WorldFrom") + "\\world\\", xml.GetKey("WorldTo") + "\\" + dt + "\\world\\");
                        File.Copy(file, NewFile);
                    }
                }

                foreach (string file in Directory.GetFiles(xml.GetKey("WorldFrom") + "\\world\\"))
                {
                    string NewFile = file.Replace(xml.GetKey("WorldFrom") + "\\world\\", xml.GetKey("WorldTo") + dt + "\\world\\");
                    File.Copy(file, NewFile);
                }
            }

            CopyWorker.ReportProgress(75);
            


            UpdateImports(dt);

            PluginWork();

            CopyWorker.ReportProgress(100);


            if(xml.GetKey("PlayFinishedSound") == "yes")
                System.Media.SystemSounds.Asterisk.Play();

        }

        private void PluginWork()
        {
            for (int i = 0; i < 10; i++)    //allows up to 10 priority numbers
                foreach (var plugin in plugins)
                    if (plugin.Priority() == i) //checks if the plugin has this priority number, and if it does run it.
                        plugin.Work(Imports);
        }

        private void UpdateImports(string time)
        {
            Imports[0] = xml.GetKey("WorldFrom");
            Imports[1] = xml.GetKey("WorldTo");
            Imports[2] = xml.GetKey("TimeBetween");
            Imports[3] = time;
        }

        private void CopyWorker_ProgressChange(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

            if (ShouldIStop)
            {
                StopBtn.Visible = false;
                StopBtn.Enabled = false;

                StartBTN.Visible = true;
                StartBTN.Enabled = true;
                CopyWorker.CancelAsync();
            }
        }

        private void CopyWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!CopyWorker.IsBusy && !ShouldIStop)
                CopyWorker.RunWorkerAsync();
        }
    }
}
