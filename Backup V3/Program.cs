using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace returnzork.Backup_V3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length != 0)
            {
                bool error = false;
                foreach (string argument in args)
                {
                    Form1 form = new Form1();
                    switch (argument.ToLower())
                    {
                        case "backup":
                            form.Check();
                            form.ConsoleStartBackup();
                            break;
                        default:
                            error = true;
                            break;
                    }
                }
                if (error)
                    return 1;
                else
                    return 0;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            return 0;
        }
    }
}
