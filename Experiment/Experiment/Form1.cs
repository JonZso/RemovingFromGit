
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Xml;


namespace Experiment
{
    public partial class Form1 : Form
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        private List<String> files = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sysText.Text = "Coming soon in a new feature..";
            pictureBox1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string son = Environment.CurrentDirectory.ToString();
            string dad = Directory.GetParent(son).FullName;
            var ver = assembly.GetName().Version.ToString(3);

            try
            {
                bool exists = System.IO.Directory.Exists("../tools");
                if (!exists)
                {
                    DirectoryInfo subfolder = new DirectoryInfo($@"{son}\app-{ver}\source");
                    subfolder.MoveTo(@"tools");
                    sysText.Text = ("Folder Was Created..\nEveryhing has been moved over now");
                }
                else
                {
                    sysText.Text = ("Already created.");
                }
            }
            catch
            {
                sysText.Text = "Already moved over";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sysText.Text = "";
            string son = Environment.CurrentDirectory.ToString();
            string dad = Directory.GetParent(son).FullName;

            string curFile = @"..\tools\Controller\App.exe";
            sysText.AppendText(File.Exists(curFile)
                ? $"File Is in the incorrect Location When back at menu press 3.\n"
                : "File has been moved (hopefully to the right place)\n\n");
            sysText.AppendText(Environment.NewLine);
            string askDad = $@"tools\Controller\App.exe";
            sysText.AppendText(File.Exists(askDad)
                ? $"Yeah it's in the right place! (phew)\n"
                : "Uh Oh.. It's not in the correct place (not in here either!)\n\n");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string son = Environment.CurrentDirectory.ToString();
            var ver = assembly.GetName().Version.ToString(3);
            DirectoryInfo subfolder = new DirectoryInfo($@"{son}\app-{ver}\immersive");
            string subPath = subfolder.ToString();
            bool exists = System.IO.Directory.Exists(subPath);
            if (!exists)
                sysText.Text = ("Already Moved the files over...");
            else
            {
                sysText.Text = ("Moving Files over now...");
                subfolder.MoveTo(@"../tools");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sysText.Text = "";
            string subPath = "../random"; // your code goes here
            bool exists = System.IO.Directory.Exists(subPath);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(subPath);
                sysText.Text = "Folder was created";
            }
            else
            {
                sysText.AppendText("Should be inside directory...");
            }

            string subPath1 = "random"; // your code goes here
            bool exists1 = System.IO.Directory.Exists(subPath1);
            if (!exists1)
            {
                System.IO.Directory.CreateDirectory(subPath1);
                sysText.Text = ("Folder was created");
            }
            else
            {
                sysText.AppendText(Environment.NewLine);
                sysText.AppendText("Should be inside directory...");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var ver = assembly.GetName().Version.ToString(3);

            string str_directory = Environment.CurrentDirectory.ToString();
            string parent = System.IO.Directory.GetParent(str_directory).FullName;
            sysText.Text =
                ($@"The child location : {str_directory}
The parent location is : {parent}

You're currently using version : {ver}
the location of this excutable is : 
        {assembly
                    .Location}");


        }

        private void button7_Click(object sender, EventArgs e)
        {
        
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string x = openFileDialog1.FileName;

                textBox2.Text = x;
                sysText.Text = "";
                sysText.Text = $"Folder has been selected... waiting.{Environment.NewLine}";

            }

        }



        private void button8_Click(object sender, EventArgs e)
        {           
                string str_directory = Environment.CurrentDirectory.ToString();
                string parent = System.IO.Directory.GetParent(str_directory).FullName;
             

                string zipPath = textBox2.Text;
                string extractPath = $"{textBox5.Text}";
            try
            {

                ZipFile.ExtractToDirectory(zipPath, extractPath);
                sysText.AppendText($"Files extracted to: {Environment.NewLine}{extractPath} {Environment.NewLine}Click [Grab Files] which will find all of (files) files. You can either select a new folder or press [Cancel] to keep default.");
            }
            catch (ArgumentException)
            {
                    sysText.Text = "The file already exists.. Press Clean to clean up previous temp files.. "  ;
            }

          GrabJsonFiles();
          MoveJsonFiles();
          EditConfigJsonFile();

            label1.Text = $"You're destination is: {extractPath}";

        }

        private void EditConfigJsonFile()
        {
            string str_directory = Environment.CurrentDirectory.ToString();
            string json = File.ReadAllText($@"{str_directory}\immersive\WebApp\App\Config\Config.json");
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonObj["AppRoot"] = $@"{textBox5.Text.Replace(@"\", "\\")}\App\Apps";
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText($@"{str_directory}\immersive\WebApp\App\Config\Config.json", output);
        }

        private void MoveJsonFiles()
        {

            try
            {

                foreach (var xd in files)
                {
                    sysText.AppendText(($"{xd}{Environment.NewLine}"));
                    var xx = Path.GetFileName(xd);
                    System.IO.File.Copy(xd,
                     //   $@"immersive\WebApp\App\Scenelists\{xx}",
                     $@"C: \Users\ImmersiveJon\Desktop\PICK ME\StoreHere\{xx}",
                        true);
                }
            }
            catch {
            }

            sysText.AppendText($"{Environment.NewLine}{files.Count} Files Moved.");
        }

        private void GrabJsonFiles()
        {
            string x;
        
      
            x = textBox5.Text;

            files.AddRange(System.IO.Directory.GetFiles(x, $"*.json", SearchOption.TopDirectoryOnly));

            foreach (string d in Directory.GetDirectories(x))
            {

                files.AddRange(System.IO.Directory.GetFiles(d, $"*.json"));
            }

            sysText.AppendText($"{Environment.NewLine}{files.Count} -  Files Found. {Environment.NewLine}");
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            var ver = assembly.GetName().Version.ToString(3);
            this.Text = $"Extractor (v{ver})"; 
        }


        private void button11_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog3.ShowDialog() == DialogResult.OK)
            {
                var partFilePath = folderBrowserDialog3.SelectedPath;
                textBox5.Text = partFilePath;
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            string json = File.ReadAllText(@"immersive\WebApp\App\Config\Config.json");
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonObj["AppRoot"] = $@"{textBox5.Text.Replace(@"\", "\\" )}\App\Apps";
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(@"immersive\WebApp\App\Config\Config.json", output);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            GrabJsonFiles();
            MoveJsonFiles();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string str_directory = Environment.CurrentDirectory.ToString();
            CreateConFig(str_directory);
        }


    private void CreateConFig(string x)
        {
            string output = $@"<?xml version=""1.0""?>
<configuration>
  <appSettings>
    <add key=""port"" value=""3333""/>
    <add key=""LauncherPath2"" value=""..\App\App\Player\Immersive Player.exe""/>
    <add key=""LauncherPath"" value=""{x}\immersive\immersive.exe""/>    
  </appSettings>
 <startup><supportedRuntime version=""v4.0"" sku ="".NETFramework,Version=v4.0""/></startup></configuration>
              ";
            File.WriteAllText($@"{x}\immersive\Controller\Lol.text", output);
        }
    }
}


   // <add key=""LauncherPath"" value=""%PROGRAMDATA%\App\App\Player\Immersive Player.exe""/>    
