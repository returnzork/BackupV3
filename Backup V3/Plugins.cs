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

using returnzork.BackupV3_API;

namespace Backup_V3
{
    public partial class Plugins : Form
    {
        [ImportMany]
        public IEnumerable<BackupV3API> plugins { get; set; }

       

        public Plugins()
        {
            InitializeComponent();
            PopulatePluginsBox();
        }

        void PopulatePluginsBox()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog(Environment.GetEnvironmentVariable("APPDATA") + "\\returnzork\\BackupV3\\plugins\\"));
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);



            foreach (var pl in plugins)
            {
                richTextBox1.Text = richTextBox1.Text + "\"" + pl.Name() + "\"   by  \"" + pl.Author() + "\"\r\n";
            }
        }
    }
}
