using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1
{
	// Token: 0x02000004 RID: 4
	public partial class Form1 : Form
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020FA File Offset: 0x000002FA
		public Form1()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000212F File Offset: 0x0000032F
		public void Run()
		{
			this.ReadArg();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000213C File Offset: 0x0000033C
		private string RunProcess(string fileName, string args)
		{
			string result = "";
			Process process2 = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = fileName,
					Arguments = args,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};
			process2.Start();
			while (!process2.StandardOutput.EndOfStream)
			{
				string text = process2.StandardOutput.ReadLine();
				bool flag = text.Length > 0;
				if (flag)
				{
					result = text;
				}
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D8 File Offset: 0x000003D8
		private void ReadArg()
		{
			while (this.Checking)
			{
				this.WSAppPath = this.inputdir.Text;
				bool flag = this.WSAppPath.Contains("\"");
				if (flag)
				{
					this.WSAppPath = this.WSAppPath.Replace("\"", "");
					this.WSAppPath = "\"" + this.WSAppPath + "\"";
				}
				else
				{
					bool flag2 = File.Exists(this.WSAppPath + "\\" + this.WSAppXmlFile);
					if (!flag2)
					{
						this.Checking = true;
						throw new ArgumentException("Invailed App Path, '{0}' file not found!", this.WSAppXmlFile);
					}
					while (this.Checking)
					{
						this.WSAppOutputPath = this.outputdir.Text;
						bool flag3 = this.WSAppOutputPath.Contains("\"");
						if (flag3)
						{
							this.WSAppOutputPath = this.WSAppOutputPath.Replace("\"", "");
							this.WSAppOutputPath = "\"" + this.WSAppOutputPath + "\"";
						}
						else
						{
							bool flag4 = Directory.Exists(this.WSAppOutputPath);
							if (!flag4)
							{
								this.Checking = true;
								throw new ArgumentException("Invailed Output Path, '{0}' Directory not found!", this.WSAppOutputPath);
							}
							this.WSAppFileName = Path.GetFileName(this.WSAppPath);
							using (XmlReader xmlReader = XmlReader.Create(this.WSAppPath + "\\" + this.WSAppXmlFile))
							{
								while (xmlReader.Read())
								{
									bool flag5 = xmlReader.IsStartElement() && xmlReader.Name == "Identity";
									if (flag5)
									{
										string text = xmlReader["Name"];
										bool flag6 = text != null;
										if (flag6)
										{
											this.WSAppName = text;
										}
										text = xmlReader["Publisher"];
										bool flag7 = text != null;
										if (flag7)
										{
											this.WSAppPublisher = text;
										}
										text = xmlReader["Version"];
										bool flag8 = text != null;
										if (flag8)
										{
											this.WSAppVersion = text;
										}
										text = xmlReader["ProcessorArchitecture"];
										bool flag9 = text != null;
										if (flag9)
										{
											this.WSAppProcessorArchitecture = text;
										}
									}
								}
							}
							while (this.Checking)
							{
								this.MakeAppx();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002478 File Offset: 0x00000678
		private void MakeAppx()
		{
			string text = this.AppCurrentDirctory + "\\Appxpacker\\MakeAppx.exe";
			string args = string.Concat(new string[]
			{
				"pack - d \"",
				this.WSAppPath,
				"\" -p \"",
				this.WSAppOutputPath,
				"\\",
				this.WSAppFileName,
				".appx\" -l"
			});
			bool @checked = this.semantic.Checked;
			if (@checked)
			{
				args = string.Concat(new string[]
				{
					"pack -d \"",
					this.WSAppPath,
					"\" -p \"",
					this.WSAppOutputPath,
					"\\",
					this.WSAppFileName,
					".appx\" -l -nv"
				});
			}
			bool checked2 = this.patchtdf.Checked;
			if (checked2)
			{
				List<string> devices = new List<string>();
				List<string> adevices = new List<string>();
				devices.Add("Windows.Mobile");
				devices.Add("Windows.Xbox");
				devices.Add("Windows.Holographic");
				devices.Add("Windows.Team");
				devices.Add("Windows.IoT");
				devices.Add("Windows.Desktop");
				string manifest = File.ReadAllText(this.WSAppPath + "\\AppxManifest.xml");
				foreach (string value in devices)
				{
					bool flag = manifest.Contains(value);
					if (flag)
					{
						adevices.Add(value);
					}
				}
				string replace = adevices.First<string>();
				MessageBox.Show(replace);
				manifest = manifest.Replace(replace, "Windows.Universal");
				File.WriteAllText(this.WSAppPath + "\\AppxManifest.xml", manifest);
			}
			bool flag2 = File.Exists(text);
			if (!flag2)
			{
				this.Checking = false;
				throw new ArgumentException("Can't create '.appx' file, 'MakeAppx.exe' file not found!");
			}
			bool flag3 = File.Exists(this.WSAppOutputPath + "\\" + this.WSAppFileName + ".appx");
			if (flag3)
			{
				File.Delete(this.WSAppOutputPath + "\\" + this.WSAppFileName + ".appx");
			}
			Label label = this.status;
			label.Text += "Please wait.. Creating '.appx' package file.";
			string tempvar = this.WSAppFileName + ".appx";
			bool flag4 = this.RunProcess(text, args).ToLower().Contains("succeeded");
			if (flag4)
			{
				Label label2 = this.status;
				label2.Text = label2.Text + "\nPackage " + tempvar + " creation succeeded.";
				while (this.Checking)
				{
					this.MakeCert();
				}
				return;
			}
			this.Checking = false;
			throw new ArgumentException("Package '{0}' creation failed. ", this.WSAppFileName + ".appx");
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002770 File Offset: 0x00000970
		private void MakeCert()
		{
			string text = this.AppCurrentDirctory + "\\Appxpacker\\MakeCert.exe";
			string args = string.Concat(new string[]
			{
				"-n \"",
				this.WSAppPublisher,
				"\" -r -a sha256 -len 2048 -cy end -h 0 -eku 1.3.6.1.5.5.7.3.3 -b 01/01/2000 -sv \"",
				this.WSAppOutputPath,
				"\\",
				this.WSAppFileName,
				".pvk\" \"",
				this.WSAppOutputPath,
				"\\",
				this.WSAppFileName,
				".cer\""
			});
			bool flag = File.Exists(text);
			if (!flag)
			{
				this.Checking = false;
				throw new ArgumentException("Can't create Certificate for the package, 'MakeCert.exe' file not found!");
			}
			bool flag2 = File.Exists(this.WSAppOutputPath + "\\" + this.WSAppFileName + ".pvk");
			if (flag2)
			{
				File.Delete(this.WSAppOutputPath + "\\" + this.WSAppFileName + ".pvk");
			}
			bool flag3 = File.Exists(this.WSAppOutputPath + "\\" + this.WSAppFileName + ".cer");
			if (flag3)
			{
				File.Delete(this.WSAppOutputPath + "\\" + this.WSAppFileName + ".cer");
			}
			Label label = this.status;
			label.Text += "\nPlease wait.. Creating certificate for the package.";
			bool flag4 = this.RunProcess(text, args).ToLower().Contains("succeeded");
			if (flag4)
			{
				while (this.Checking)
				{
					this.Pvk2Pfx();
				}
				return;
			}
			this.Checking = false;
			throw new ArgumentException("Failed to create Certificate for the package... Prees any Key exit.");
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002918 File Offset: 0x00000B18
		private void Pvk2Pfx()
		{
			string text = this.AppCurrentDirctory + "\\Appxpacker\\Pvk2Pfx.exe";
			string args = string.Concat(new string[]
			{
				"-pvk \"",
				this.WSAppOutputPath,
				"\\",
				this.WSAppFileName,
				".pvk\" -spc \"",
				this.WSAppOutputPath,
				"\\",
				this.WSAppFileName,
				".cer\" -pfx \"",
				this.WSAppOutputPath,
				"\\",
				this.WSAppFileName,
				".pfx\""
			});
			bool flag = File.Exists(text);
			if (!flag)
			{
				this.Checking = false;
				throw new ArgumentException("Can't convert Certificate to sign the package, 'Pvk2Pfx.exe' file not found!");
			}
			bool flag2 = File.Exists(this.WSAppOutputPath + "\\" + this.WSAppFileName + ".pfx");
			if (flag2)
			{
				File.Delete(this.WSAppOutputPath + "\\" + this.WSAppFileName + ".pfx");
			}
			bool flag3 = this.RunProcess(text, args).Length == 0;
			if (flag3)
			{
				while (this.Checking)
				{
					this.SignApp();
				}
				return;
			}
			this.Checking = false;
			throw new ArgumentException("Can't convert certificate to sign the package... Prees any Key exit...");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002A68 File Offset: 0x00000C68
		private void SignApp()
		{
			string text = this.AppCurrentDirctory + "\\Appxpacker\\SignTool.exe";
			string args = string.Concat(new string[]
			{
				"sign -fd SHA256 -a -f \"",
				this.WSAppOutputPath,
				"\\",
				this.WSAppFileName,
				".pfx\" \"",
				this.WSAppOutputPath,
				"\\",
				this.WSAppFileName,
				".appx\""
			});
			bool flag = File.Exists(text);
			if (!flag)
			{
				this.Checking = false;
				throw new ArgumentException("Can't Sign the package, 'SignTool.exe' file not found!");
			}
			Label label = this.status;
			label.Text += "Please wait.. Signing the package, this may take some minutes.";
			bool flag2 = this.RunProcess(text, args).ToLower().Contains("successfully signed");
			if (flag2)
			{
				this.Checking = false;
				MessageBox.Show("Package signed and created. Please install the '.cer' file to [Local Computer\\Trusted Root Certification Authorities] before installing the Appx Package (if installing on pc)");
				return;
			}
			this.Checking = false;
			throw new ArgumentException("Can't Sign the package, Press any Key to exit...");
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002B61 File Offset: 0x00000D61
		private void start_Click(object sender, EventArgs e)
		{
			this.Run();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002B6B File Offset: 0x00000D6B
		private void inputdir_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002B6E File Offset: 0x00000D6E
		private void label1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002B71 File Offset: 0x00000D71
		private void outputdir_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002B74 File Offset: 0x00000D74
		private void label2_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002B77 File Offset: 0x00000D77
		private void label3_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002B7A File Offset: 0x00000D7A
		private void Form1_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002B7D File Offset: 0x00000D7D
		private void button1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002B80 File Offset: 0x00000D80
		private void Form1_Load_1(object sender, EventArgs e)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002B84 File Offset: 0x00000D84
		private void id_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog di = new FolderBrowserDialog();
			di.Description = " select app folder";
			bool flag = di.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				this.inputdir.Text = di.SelectedPath;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002BC4 File Offset: 0x00000DC4
		private void od_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog duo = new FolderBrowserDialog();
			duo.Description = " select output folder";
			bool flag = duo.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				this.outputdir.Text = duo.SelectedPath;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002C03 File Offset: 0x00000E03
		private void patchtdf_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x04000004 RID: 4
		private string AppCurrentDirctory = Directory.GetCurrentDirectory();

		// Token: 0x04000005 RID: 5
		private string WSAppXmlFile = "AppxManifest.xml";

		// Token: 0x04000006 RID: 6
		private string WSAppName;

		// Token: 0x04000007 RID: 7
		private string WSAppPath;

		// Token: 0x04000008 RID: 8
		private string WSAppVersion;

		// Token: 0x04000009 RID: 9
		private string WSAppFileName;

		// Token: 0x0400000A RID: 10
		private string WSAppOutputPath;

		// Token: 0x0400000B RID: 11
		private string WSAppProcessorArchitecture;

		// Token: 0x0400000C RID: 12
		private string WSAppPublisher;

		// Token: 0x0400000D RID: 13
		private bool Checking = true;
	}
}
