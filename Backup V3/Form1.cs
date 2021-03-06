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

        ErrorLogger logger;

        bool ShouldIStop = false;


        string[] ExcludeFolders = { };


        Properties.MainSettings ms = new Properties.MainSettings();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {

            setVariables();
            StopBtn.Enabled = false;
            StopBtn.Visible = false;

            

            Check();
            LoadPlugins();


            foreach (var plugin in plugins)
                plugin.Initialize();


            AddPluginInterface();
        }

        private void setVariables()
        {
            SettingsFolder = Environment.GetEnvironmentVariable("APPDATA") + "\\returnzork\\BackupV3\\";
            PluginsFolder = SettingsFolder + "Plugins\\";
            logger = new ErrorLogger(SettingsFolder + "Error.log");
        }


        /// <summary>
        /// Checks if the folders that need to exist actually exist
        /// </summary>
        public void Check()
        {
            if (SettingsFolder == null)
                setVariables();
            if (!Directory.Exists(SettingsFolder))
            {
                Directory.CreateDirectory(SettingsFolder);
                Directory.CreateDirectory(PluginsFolder);
            }

            if (!Directory.Exists(SettingsFolder + "PluginConfig"))
                Directory.CreateDirectory(SettingsFolder + "PluginConfig");
            if (!Directory.Exists(SettingsFolder + "PluginLib"))
                Directory.CreateDirectory(SettingsFolder + "PluginLib");
            if (!Directory.Exists(SettingsFolder + "Plugins"))
                Directory.CreateDirectory(SettingsFolder + "Plugins");
        }

        /// <summary>
        /// Load all the plugins
        /// </summary>
        private void LoadPlugins()
        {
            try
            {
                AggregateCatalog catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new DirectoryCatalog(PluginsFolder));
                CompositionContainer container = new CompositionContainer(catalog);
                container.ComposeParts(this);
            }
            catch (ReflectionTypeLoadException rtle)
            {
                logger.MakeLog(rtle);
                Exception[] exceptions = rtle.LoaderExceptions;
                foreach (var EX in exceptions)
                {
                    logger.MakeLog(EX);
                }
                MessageBox.Show("There was an error with loading one or more plugins.\nThe program will now exit.");
                Environment.Exit(2);
            }
            catch (Exception ex)
            {
                logger.MakeLog(ex);
            }

#if DEBUG
            if (plugins != null)
            {
                foreach (var plugin in plugins)
                {
                    MessageBox.Show(plugin.Name() + " by " + plugin.Author() + " has loaded.");
                }
            }
#endif
        }

       
        private void SettingsBTN_click(object sender, EventArgs e)
        {
            Settings set = new Settings();
            set.Show();
        }

        bool isAdding;

        /// <summary>
        /// Add all plugin interfaces to the toolstrip
        /// </summary>
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

        /// <summary>
        /// Toolstrip item clicked
        /// </summary>
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


        /// <summary>
        /// Start the backup worker
        /// </summary>
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
        public void ConsoleStartBackup()
        {
            Backup(true);
        }

        /// <summary>
        /// Stop the backup worker
        /// </summary>
        private void StopBtn_Click(object sender, EventArgs e)
        {
            ShouldIStop = true;

            StartBTN.Enabled = true;
            StartBTN.Visible = true;

            StopBtn.Visible = false;
            StopBtn.Enabled = false;
        }

        /// <summary>
        /// Checks what folders are to be skipped in the backup
        /// </summary>
        private void UpdateExcludeFolders()
        {
            if (ms.ExcludeFolder1 != "")
            {
                Array.Resize(ref ExcludeFolders, ExcludeFolders.Length + 1);
                ExcludeFolders[ExcludeFolders.Length - 1] = ms.ExcludeFolder1;
            }
            if (ms.ExcludeFolder2 != "")
            {
                Array.Resize(ref ExcludeFolders, ExcludeFolders.Length + 1);
                ExcludeFolders[ExcludeFolders.Length - 1] = ms.ExcludeFolder2;
            }
            if(ms.ExcludeFolder3 != "")
            {
                Array.Resize(ref ExcludeFolders, ExcludeFolders.Length + 1);
                ExcludeFolders[ExcludeFolders.Length - 1] = ms.ExcludeFolder3;
            }
        }

        DateTime Started, End;

        /// <summary>
        /// The actual working part of the backup
        /// </summary>
        private void CopyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!Directory.Exists(ms.WorldFrom))
            {
                MessageBox.Show("Directory \"" + ms.WorldFrom + "\" does not exist, cannot continue.");
                logger.MakeLog("Directory does not exist");
                ShouldIStop = true;
                CopyWorker.ReportProgress(0);
                return;
            }

            CopyWorker.ReportProgress(5);
            Started = DateTime.Now;     //set the start time to the current time
            End = DateTime.Now.AddMinutes(ms.TimeBetween);  //add the time between value to the current time
            while(Started < End)    //while when it started is less than the end time
            {
                System.Threading.Thread.Sleep(100);
                Started = DateTime.Now;

                if (CopyWorker.CancellationPending)
                    break;

                if (Convert.ToInt32(End.Minute) - Convert.ToInt32(Started.Minute) < 0)
                {
                    int ne = Math.Abs(Convert.ToInt32(End.Minute) - Convert.ToInt32(Started.Minute));                   

                    ne = ms.TimeBetween - ne;

                    int newtime = ms.TimeBetween + ne;


                    Invoke((MethodInvoker)delegate { TimeRemainingTextBox.Text = newtime + " minutes remaining"; });
                }
                else
                    Invoke((MethodInvoker)delegate { TimeRemainingTextBox.Text = Convert.ToInt32(End.Minute) - Convert.ToInt32(Started.Minute) + " minutes left"; });


                CopyWorker.ReportProgress(6);
            }


            if (CopyWorker.CancellationPending)
                return;



            PluginWork(true);

            Backup();
        }

        /// <summary>
        /// Perform the backup
        /// </summary>
        /// <param name="console">true if ran from console</param>
        private void Backup(bool console = false)
        {
            if (!Directory.Exists(ms.WorldFrom))
            {
                if (!Directory.Exists(ms.WorldFrom))
                {
                    MessageBox.Show("Directory \"" + ms.WorldFrom + "\" does not exist, cannot continue.");
                    logger.MakeLog("Directory does not exist");
                    ShouldIStop = true;
                    if(!console)
                        CopyWorker.ReportProgress(0);
                    return;
                }
            }



            UpdateExcludeFolders();
            if(!console)
                CopyWorker.ReportProgress(10);



            if (!Directory.Exists(ms.WorldTo))
            {
                Directory.CreateDirectory(ms.WorldTo);
            }

            string dt = DateTime.Now.ToString("MM.dd.yyyy  hh-mm-ss tt");

            if (!Directory.Exists(ms.WorldTo + dt))
            {
                Directory.CreateDirectory(ms.WorldTo + "\\" + dt);
            }


            bool WorldOnly = ms.WorldOnly;

            if (!WorldOnly)
            {
                foreach (string DirCreate in Directory.GetDirectories(ms.WorldFrom, "*", SearchOption.AllDirectories))
                {
                    if(!ms.WorldTo.EndsWith("\\"))
                        ms.WorldTo += "\\";
                    string DIR = DirCreate.Replace(ms.WorldFrom, ms.WorldTo + dt + "\\");
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
                        string NewFile = file.Replace(ms.WorldFrom, ms.WorldTo + dt + "\\");
                        File.Copy(file, NewFile);
                    }
                }
                if (!console)
                    CopyWorker.ReportProgress(35);
                foreach (string file in Directory.GetFiles(ms.WorldFrom))
                {
                    string NewFile = file.Replace(ms.WorldFrom, ms.WorldTo + "\\" + dt + "\\");
                    File.Copy(file, NewFile);
                }
            }
            else
            {
                foreach (string dir in Directory.GetDirectories(ms.WorldFrom + "\\world\\", "*", SearchOption.AllDirectories))
                {
                    string DIR = dir.Replace(ms.WorldFrom, ms.WorldTo + dt + "\\");
                    Directory.CreateDirectory(DIR);

                    foreach (string file in Directory.GetFiles(dir))
                    {
                        string NewFile = file.Replace(ms.WorldFrom + "\\world\\", ms.WorldTo + "\\" + dt + "\\world\\");
                        File.Copy(file, NewFile);
                    }
                }

                foreach (string file in Directory.GetFiles(ms.WorldFrom + "\\world\\"))
                {
                    string NewFile = file.Replace(ms.WorldFrom + "\\world\\", ms.WorldTo + dt + "\\world\\");
                    File.Copy(file, NewFile);
                }
            }

            if(!console)
                CopyWorker.ReportProgress(75);



            UpdateImports(dt);

            PluginWork();

            if(!console)
                CopyWorker.ReportProgress(100);


            if (ms.PlayFinishedSound)
                System.Media.SystemSounds.Asterisk.Play();


        }

        /// <summary>
        /// Executes the plugins
        /// </summary>
        /// <param name="before">set to true when running before the backup</param>
        private void PluginWork(bool before = false)
        {
            if (plugins == null)
                return;
            if (before)
            {
                for (int i = 0; i < 10; i++)
                {
                    foreach (var plugin in plugins)
                    {
                        if (plugin.RunTime != TimeToRun.After)
                        {
                            if (plugin.Priority() == i)
                            {
                                plugin.Work(Imports);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)    //allows up to 10 priority numbers
                {
                    foreach (var plugin in plugins)
                    {
                        if (plugin.RunTime != TimeToRun.Before)
                        {
                            if (plugin.Priority() == i) //checks if the plugin has this priority number, and if it does run it.
                            {
                                plugin.Work(Imports);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update what is imported to the plugins
        /// </summary>
        /// <param name="time">Current date and time</param>
        private void UpdateImports(string time)
        {
            Imports[0] = ms.WorldFrom;
            Imports[1] = ms.WorldTo;
            Imports[2] = Convert.ToString(ms.TimeBetween);
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
