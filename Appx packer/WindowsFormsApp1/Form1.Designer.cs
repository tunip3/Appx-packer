namespace WindowsFormsApp1
{
	// Token: 0x02000004 RID: 4
	public partial class Form1 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002C08 File Offset: 0x00000E08
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002C40 File Offset: 0x00000E40
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::WindowsFormsApp1.Form1));
			this.label1 = new global::System.Windows.Forms.Label();
			this.inputdir = new global::System.Windows.Forms.TextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.outputdir = new global::System.Windows.Forms.TextBox();
			this.start = new global::System.Windows.Forms.Button();
			this.status = new global::System.Windows.Forms.Label();
			this.id = new global::System.Windows.Forms.Button();
			this.od = new global::System.Windows.Forms.Button();
			this.semantic = new global::System.Windows.Forms.CheckBox();
			this.patchtdf = new global::System.Windows.Forms.CheckBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(50, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "App path";
			this.label1.Click += new global::System.EventHandler(this.label1_Click);
			this.inputdir.Location = new global::System.Drawing.Point(12, 25);
			this.inputdir.Name = "inputdir";
			this.inputdir.Size = new global::System.Drawing.Size(356, 20);
			this.inputdir.TabIndex = 1;
			this.inputdir.TextChanged += new global::System.EventHandler(this.inputdir_TextChanged);
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(9, 48);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(68, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Output folder";
			this.label2.Click += new global::System.EventHandler(this.label2_Click);
			this.outputdir.Location = new global::System.Drawing.Point(12, 64);
			this.outputdir.Name = "outputdir";
			this.outputdir.Size = new global::System.Drawing.Size(356, 20);
			this.outputdir.TabIndex = 3;
			this.outputdir.TextChanged += new global::System.EventHandler(this.outputdir_TextChanged);
			this.start.Location = new global::System.Drawing.Point(12, 110);
			this.start.Name = "start";
			this.start.Size = new global::System.Drawing.Size(356, 26);
			this.start.TabIndex = 4;
			this.start.Text = "Create appx";
			this.start.UseVisualStyleBackColor = true;
			this.start.Click += new global::System.EventHandler(this.start_Click);
			this.status.AutoSize = true;
			this.status.Location = new global::System.Drawing.Point(12, 139);
			this.status.Name = "status";
			this.status.Size = new global::System.Drawing.Size(0, 13);
			this.status.TabIndex = 5;
			this.status.Click += new global::System.EventHandler(this.label3_Click);
			this.id.Location = new global::System.Drawing.Point(374, 24);
			this.id.Name = "id";
			this.id.Size = new global::System.Drawing.Size(26, 20);
			this.id.TabIndex = 6;
			this.id.Text = "...";
			this.id.UseVisualStyleBackColor = true;
			this.id.Click += new global::System.EventHandler(this.id_Click);
			this.od.Location = new global::System.Drawing.Point(374, 64);
			this.od.Name = "od";
			this.od.Size = new global::System.Drawing.Size(26, 19);
			this.od.TabIndex = 7;
			this.od.Text = "...";
			this.od.UseVisualStyleBackColor = true;
			this.od.Click += new global::System.EventHandler(this.od_Click);
			this.semantic.AutoSize = true;
			this.semantic.Location = new global::System.Drawing.Point(214, 90);
			this.semantic.Name = "semantic";
			this.semantic.Size = new global::System.Drawing.Size(154, 17);
			this.semantic.TabIndex = 8;
			this.semantic.Text = "Disable semantic validation";
			this.semantic.UseVisualStyleBackColor = true;
			this.patchtdf.AutoSize = true;
			this.patchtdf.Checked = true;
			this.patchtdf.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.patchtdf.Location = new global::System.Drawing.Point(12, 90);
			this.patchtdf.Name = "patchtdf";
			this.patchtdf.Size = new global::System.Drawing.Size(157, 17);
			this.patchtdf.TabIndex = 9;
			this.patchtdf.Text = "Patch Target Device Family";
			this.patchtdf.UseVisualStyleBackColor = true;
			this.patchtdf.CheckedChanged += new global::System.EventHandler(this.patchtdf_CheckedChanged);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(412, 194);
			base.Controls.Add(this.patchtdf);
			base.Controls.Add(this.semantic);
			base.Controls.Add(this.od);
			base.Controls.Add(this.id);
			base.Controls.Add(this.status);
			base.Controls.Add(this.start);
			base.Controls.Add(this.outputdir);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.inputdir);
			base.Controls.Add(this.label1);
			base.Icon = (global::System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "Form1";
			this.Text = "Appx packer";
			base.Load += new global::System.EventHandler(this.Form1_Load_1);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400000E RID: 14
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x0400000F RID: 15
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000010 RID: 16
		private global::System.Windows.Forms.TextBox inputdir;

		// Token: 0x04000011 RID: 17
		private global::System.Windows.Forms.Label label2;

		// Token: 0x04000012 RID: 18
		private global::System.Windows.Forms.TextBox outputdir;

		// Token: 0x04000013 RID: 19
		private global::System.Windows.Forms.Button start;

		// Token: 0x04000014 RID: 20
		private global::System.Windows.Forms.Label status;

		// Token: 0x04000015 RID: 21
		private global::System.Windows.Forms.Button id;

		// Token: 0x04000016 RID: 22
		private global::System.Windows.Forms.Button od;

		// Token: 0x04000017 RID: 23
		private global::System.Windows.Forms.CheckBox semantic;

		// Token: 0x04000018 RID: 24
		private global::System.Windows.Forms.CheckBox patchtdf;
	}
}
