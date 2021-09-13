namespace NiceShot.DotNetWinFormsClient
{
    partial class EmFinReportForm
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
            this.components = new System.ComponentModel.Container();
            this.lsbFinReports = new System.Windows.Forms.ListBox();
            this.cmsEmReport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_viewpdf = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEmReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsbFinReports
            // 
            this.lsbFinReports.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsbFinReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbFinReports.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lsbFinReports.FormattingEnabled = true;
            this.lsbFinReports.ItemHeight = 20;
            this.lsbFinReports.Location = new System.Drawing.Point(0, 0);
            this.lsbFinReports.Name = "lsbFinReports";
            this.lsbFinReports.Size = new System.Drawing.Size(333, 416);
            this.lsbFinReports.TabIndex = 0;
            this.lsbFinReports.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lsbFinReports_DrawItem);
            this.lsbFinReports.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsbFinReports_MouseDoubleClick);
            this.lsbFinReports.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lsbFinReports_MouseDown);
            // 
            // cmsEmReport
            // 
            this.cmsEmReport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_viewpdf});
            this.cmsEmReport.Name = "cmsEmReport";
            this.cmsEmReport.Size = new System.Drawing.Size(147, 26);
            // 
            // tsmi_viewpdf
            // 
            this.tsmi_viewpdf.Name = "tsmi_viewpdf";
            this.tsmi_viewpdf.Size = new System.Drawing.Size(146, 22);
            this.tsmi_viewpdf.Text = "查看PDF文件";
            this.tsmi_viewpdf.Click += new System.EventHandler(this.tsmi_viewpdf_Click);
            // 
            // EmFinReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 416);
            this.Controls.Add(this.lsbFinReports);
            this.Name = "EmFinReportForm";
            this.Text = "查看财务报表";
            this.cmsEmReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lsbFinReports;
        private System.Windows.Forms.ContextMenuStrip cmsEmReport;
        private System.Windows.Forms.ToolStripMenuItem tsmi_viewpdf;
    }
}