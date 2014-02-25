using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace returnzork.ErrorLogging
{
    class ErrorLogger
    {
        string LogFile;

        public ErrorLogger(string LogFile)
        {
            this.LogFile = LogFile;
            if (!File.Exists(LogFile))
            {
                if(!Directory.Exists(LogFile))
                {
                    LogFile = LogFile.Replace("Error.log", "")
                    Directory.CreateDirectory(LogFile);
                    LogFile += "Error.log";
                }
                File.Create(LogFile);
            }
        }

        /// <summary>
        /// Add to the log file
        /// </summary>
        /// <param name="ex">Exception that occurred</param>
        public void MakeLog(Exception ex)
        {
            FileStream stream = new FileStream(LogFile, FileMode.Append);   //create a filestream to open the log
            StreamWriter writer = new StreamWriter(stream);     //create a streamwriter to write to the log file

            DateTime dt = DateTime.Now;     //set the time to log with

            writer.WriteLine(dt + ":   " + ex.Message + "\r\n");    //write to the log file
            writer.Close();     //close the writer so the file is not in use
            stream.Close();     //close the filestream so the file is not in use
        }

        /// <summary>
        /// Add to the log file
        /// </summary>
        /// <param name="error">String of error that occurred</param>
        public void MakeLog(string error)
        {
            FileStream stream = new FileStream(LogFile, FileMode.Append);
            StreamWriter writer = new StreamWriter(stream);

            DateTime dt = DateTime.Now;     //set the time to log with

            writer.WriteLine(dt + ":   " + error + "\r\n");
            writer.Close();
            stream.Close();
        }
    }
}
