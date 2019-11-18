using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string AppCurrentDirctory = Directory.GetCurrentDirectory();

        private string WSAppXmlFile = "AppxManifest.xml";

        private string WSAppName;

        private string WSAppPath;

        private string WSAppVersion;

        private string WSAppFileName;

        private string WSAppOutputPath;

        private string WSAppProcessorArchitecture;

        private string WSAppPublisher;

        private bool Checking = true;

        public void Run()
        {
            ReadArg();
        }

        private string RunProcess(string fileName, string args)
        {
            string text = "";
            string result = "";
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            Process process2 = process;
            process2.Start();
            while (!process2.StandardOutput.EndOfStream)
            {
                text = process2.StandardOutput.ReadLine();
                if (text.Length > 0)
                {
                    result = text;
                }
            }
            return result;
        }

        private void ReadArg()
        {
            while (Checking)
            {
                WSAppPath = inputdir.Text;
                if (WSAppPath.Contains("\""))
                {
                    WSAppPath = WSAppPath.Replace("\"", "");
                    WSAppPath = "\"" + WSAppPath + "\"";
                }
                else if (File.Exists(WSAppPath + "\\" + WSAppXmlFile))
                {
                    while (Checking)
                    {
                        WSAppOutputPath = outputdir.Text;
                        if (WSAppOutputPath.Contains("\""))
                        {
                            WSAppOutputPath = WSAppOutputPath.Replace("\"", "");
                            WSAppOutputPath = "\"" + WSAppOutputPath + "\"";
                        }
                        else if (Directory.Exists(WSAppOutputPath))
                        {
                            WSAppFileName = Path.GetFileName(WSAppPath);
                            using (XmlReader xmlReader = XmlReader.Create(WSAppPath + "\\" + WSAppXmlFile))
                            {
                                while (xmlReader.Read())
                                {
                                    if (xmlReader.IsStartElement() && xmlReader.Name == "Identity")
                                    {
                                        string text = xmlReader["Name"];
                                        if (text != null)
                                        {
                                            WSAppName = text;
                                        }
                                        text = xmlReader["Publisher"];
                                        if (text != null)
                                        {
                                            WSAppPublisher = text;
                                        }
                                        text = xmlReader["Version"];
                                        if (text != null)
                                        {
                                            WSAppVersion = text;
                                        }
                                        text = xmlReader["ProcessorArchitecture"];
                                        if (text != null)
                                        {
                                            WSAppProcessorArchitecture = text;
                                        }
                                    }
                                }
                            }
                            while (Checking)
                            {
                                MakeAppx();
                            }
                        }
                        else
                        {
                            Checking = true;
                            throw new ArgumentException("Invailed Output Path, '{0}' Directory not found!", WSAppOutputPath);
                        }
                    }
                }
                else
                {
                    Checking = true;
                    throw new ArgumentException("Invailed App Path, '{0}' file not found!", WSAppXmlFile);
                }
            }
        }

        private void MakeAppx()
        {
            string text
             = AppCurrentDirctory + "\\Appxpacker\\MakeAppx.exe";
            string args = "pack - d \"" + WSAppPath + "\" -p \"" + WSAppOutputPath + "\\" + WSAppFileName + ".appx\" -l";
            if (semantic.Checked) { args += " -nv";}
            if (patchtdf.Checked) {
                // a majority of this is just shoddy detection code to avoidproducing any unexpected errors later
                // create devices list
                List<string> devices = new List<string>();
                List<string> adevices = new List<string>();
                // add devices
                devices.Add("Windows.Mobile");
                devices.Add("Windows.Xbox");
                devices.Add("Windows.Holographic");
                devices.Add("Windows.Team");
                devices.Add("Windows.IoT");
                devices.Add("Windows.Desktop");
                // open manifest
                string manifest = File.ReadAllText(WSAppPath + "\\AppxManifest.xml");
                // loop through devices and search for them
                foreach (string value in devices) {
                    if ((manifest.Contains(value))) {
                        adevices.Add(value);
                    }
                }
                string replace = adevices.First();
                MessageBox.Show(replace);
                manifest = manifest.Replace(replace, "Windows.Universal");
                File.WriteAllText((WSAppPath + "\\AppxManifest.xml"), manifest);
            }
            if (File.Exists(text))
            {
                if (File.Exists(WSAppOutputPath + "\\" + WSAppFileName + ".appx"))
                {
                    File.Delete(WSAppOutputPath + "\\" + WSAppFileName + ".appx");
                }
                status.Text += ("Please wait.. Creating '.appx' package file.");

                string tempvar = WSAppFileName + ".appx";
                if (RunProcess(text, args).ToLower().Contains("succeeded"))
                {
                    status.Text += ("\nPackage "+ tempvar + " creation succeeded.");
                    while (Checking)
                    {
                        MakeCert();
                    }
                }
                else
                {
                    Checking = false;
                    throw new ArgumentException("Package '{0}' creation failed. ", WSAppFileName + ".appx");
                }
            }
            else
            {
                Checking = false;
                throw new ArgumentException("Can't create '.appx' file, 'MakeAppx.exe' file not found!");
            }
        }

        private void MakeCert()
        {
            string text = AppCurrentDirctory + "\\Appxpacker\\MakeCert.exe";
            string args = "-n \"" + WSAppPublisher + "\" -r -a sha256 -len 2048 -cy end -h 0 -eku 1.3.6.1.5.5.7.3.3 -b 01/01/2000 -sv \"" + WSAppOutputPath + "\\" + WSAppFileName + ".pvk\" \"" + WSAppOutputPath + "\\" + WSAppFileName + ".cer\"";
            if (File.Exists(text))
            {
                if (File.Exists(WSAppOutputPath + "\\" + WSAppFileName + ".pvk"))
                {
                    File.Delete(WSAppOutputPath + "\\" + WSAppFileName + ".pvk");
                }
                if (File.Exists(WSAppOutputPath + "\\" + WSAppFileName + ".cer"))
                {
                    File.Delete(WSAppOutputPath + "\\" + WSAppFileName + ".cer");
                }
                status.Text += ("\nPlease wait.. Creating certificate for the package.");
                if (RunProcess(text, args).ToLower().Contains("succeeded"))
                {
                    while (Checking)
                    {
                        Pvk2Pfx();
                    }
                }
                else
                {
                    Checking = false;
                    throw new ArgumentException("Failed to create Certificate for the package... Prees any Key exit.");
                }
            }
            else
            {
                Checking = false;
                throw new ArgumentException("Can't create Certificate for the package, 'MakeCert.exe' file not found!");
            }
        }

        private void Pvk2Pfx()
        {
            string text = AppCurrentDirctory + "\\Appxpacker\\Pvk2Pfx.exe";
            string args = "-pvk \"" + WSAppOutputPath + "\\" + WSAppFileName + ".pvk\" -spc \"" + WSAppOutputPath + "\\" + WSAppFileName + ".cer\" -pfx \"" + WSAppOutputPath + "\\" + WSAppFileName + ".pfx\"";
            if (File.Exists(text))
            {
                if (File.Exists(WSAppOutputPath + "\\" + WSAppFileName + ".pfx"))
                {
                    File.Delete(WSAppOutputPath + "\\" + WSAppFileName + ".pfx");
                }
                //Converting certificate to sign the package.
                if (RunProcess(text, args).Length == 0)
                {
                    while (Checking)
                    {
                        SignApp();
                    }
                }
                else
                {
                    Checking = false;
                    throw new ArgumentException("Can't convert certificate to sign the package... Prees any Key exit...");
                }
            }
            else
            {
                Checking = false;
                throw new ArgumentException("Can't convert Certificate to sign the package, 'Pvk2Pfx.exe' file not found!");
            }
        }

        private void SignApp()
        {
            string text = AppCurrentDirctory + "\\Appxpacker\\SignTool.exe";
            string args = "sign -fd SHA256 -a -f \"" + WSAppOutputPath + "\\" + WSAppFileName + ".pfx\" \"" + WSAppOutputPath + "\\" + WSAppFileName + ".appx\"";
            if (File.Exists(text))
            {
                status.Text += ("Please wait.. Signing the package, this may take some minutes.");
                if (RunProcess(text, args).ToLower().Contains("successfully signed"))
                {
                    Checking = false;
                    MessageBox.Show("Package signed and created. Please install the '.cer' file to [Local Computer\\Trusted Root Certification Authorities] before installing the Appx Package (if installing on pc)");
                }
                else
                {
                    Checking = false;
                    throw new ArgumentException("Can't Sign the package, Press any Key to exit...");
                }
            }
            else
            {
                Checking = false;
                throw new ArgumentException("Can't Sign the package, 'SignTool.exe' file not found!");
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void inputdir_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void outputdir_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void id_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog di = new FolderBrowserDialog();
            di.Description = " select app folder";
            if (di.ShowDialog() == DialogResult.OK)
                inputdir.Text = di.SelectedPath;
        }

        private void od_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog duo = new FolderBrowserDialog();
            duo.Description = " select output folder";
            if (duo.ShowDialog() == DialogResult.OK)
                outputdir.Text = duo.SelectedPath;
        }

        private void patchtdf_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
