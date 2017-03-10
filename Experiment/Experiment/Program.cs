using Squirrel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Experiment
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!Directory.Exists("Test"))
            {
                Directory.CreateDirectory("Test");
            }
            Task.Run(async () =>
            {
                using (var mgr = new UpdateManager("https://s3-us-west-2.amazonaws.com/jonvideotesting"))
                {
                    await mgr.UpdateApp();
                }
            });
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
