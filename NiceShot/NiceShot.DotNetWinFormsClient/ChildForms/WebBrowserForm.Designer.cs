
namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    partial class WebBrowserForm
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
            this.pnlWebBrowserForm = new System.Windows.Forms.Panel();
            this.cwbBrowser = new CefSharp.WinForms.ChromiumWebBrowser();
            this.pnlWebBrowserForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlWebBrowserForm
            // 
            this.pnlWebBrowserForm.Controls.Add(this.cwbBrowser);
            this.pnlWebBrowserForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWebBrowserForm.Location = new System.Drawing.Point(0, 0);
            this.pnlWebBrowserForm.Name = "pnlWebBrowserForm";
            this.pnlWebBrowserForm.Size = new System.Drawing.Size(506, 450);
            this.pnlWebBrowserForm.TabIndex = 0;
            // 
            // cwbBrowser
            // 
            this.cwbBrowser.ActivateBrowserOnCreation = false;
            this.cwbBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cwbBrowser.Location = new System.Drawing.Point(0, 0);
            this.cwbBrowser.Name = "cwbBrowser";
            this.cwbBrowser.Size = new System.Drawing.Size(506, 450);
            this.cwbBrowser.TabIndex = 0;
            // 
            // WebBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 450);
            this.Controls.Add(this.pnlWebBrowserForm);
            this.Name = "WebBrowserForm";
            this.Text = "WebBrowserForm";
            this.pnlWebBrowserForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWebBrowserForm;
        private CefSharp.WinForms.ChromiumWebBrowser cwbBrowser;
    }
}