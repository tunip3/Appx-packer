namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.inputdir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.outputdir = new System.Windows.Forms.TextBox();
            this.start = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.Button();
            this.od = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "App path";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // inputdir
            // 
            this.inputdir.Location = new System.Drawing.Point(12, 25);
            this.inputdir.Name = "inputdir";
            this.inputdir.Size = new System.Drawing.Size(356, 20);
            this.inputdir.TabIndex = 1;
            this.inputdir.TextChanged += new System.EventHandler(this.inputdir_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Output folder";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // outputdir
            // 
            this.outputdir.Location = new System.Drawing.Point(12, 64);
            this.outputdir.Name = "outputdir";
            this.outputdir.Size = new System.Drawing.Size(356, 20);
            this.outputdir.TabIndex = 3;
            this.outputdir.TextChanged += new System.EventHandler(this.outputdir_TextChanged);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(12, 90);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(356, 26);
            this.start.TabIndex = 4;
            this.start.Text = "Create appx";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(12, 132);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 13);
            this.status.TabIndex = 5;
            this.status.Click += new System.EventHandler(this.label3_Click);
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(374, 24);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(26, 20);
            this.id.TabIndex = 6;
            this.id.Text = "...";
            this.id.UseVisualStyleBackColor = true;
            this.id.Click += new System.EventHandler(this.id_Click);
            // 
            // od
            // 
            this.od.Location = new System.Drawing.Point(374, 64);
            this.od.Name = "od";
            this.od.Size = new System.Drawing.Size(26, 19);
            this.od.TabIndex = 7;
            this.od.Text = "...";
            this.od.UseVisualStyleBackColor = true;
            this.od.Click += new System.EventHandler(this.od_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 203);
            this.Controls.Add(this.od);
            this.Controls.Add(this.id);
            this.Controls.Add(this.status);
            this.Controls.Add(this.start);
            this.Controls.Add(this.outputdir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inputdir);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Appx packer";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputdir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox outputdir;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button id;
        private System.Windows.Forms.Button od;
    }
}

