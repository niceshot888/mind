namespace NiceShot.DotNetWinFormsClient
{
    partial class ViewPdfForm
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
            this.wbPdfViewer = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbPdfViewer
            // 
            this.wbPdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbPdfViewer.Location = new System.Drawing.Point(0, 0);
            this.wbPdfViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbPdfViewer.Name = "wbPdfViewer";
            this.wbPdfViewer.ScriptErrorsSuppressed = true;
            this.wbPdfViewer.Size = new System.Drawing.Size(1148, 589);
            this.wbPdfViewer.TabIndex = 0;
            // 
            // ViewPdfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 589);
            this.Controls.Add(this.wbPdfViewer);
            this.Name = "ViewPdfForm";
            this.Text = "ViewPdfForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbPdfViewer;
    }
}