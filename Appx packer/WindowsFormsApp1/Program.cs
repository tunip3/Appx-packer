using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	// Token: 0x02000005 RID: 5
	internal static class Program
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000032E2 File Offset: 0x000014E2
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
