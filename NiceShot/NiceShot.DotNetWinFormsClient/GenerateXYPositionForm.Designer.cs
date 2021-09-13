
namespace NiceShot.DotNetWinFormsClient
{
    partial class GenerateXYPositionForm
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
            this.btnGenerateXY = new System.Windows.Forms.Button();
            this.btnOpenSourceDir = new System.Windows.Forms.Button();
            this.btnSavedFileDir = new System.Windows.Forms.Button();
            this.btnArcGisDataDir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenerateXY
            // 
            this.btnGenerateXY.Location = new System.Drawing.Point(168, 94);
            this.btnGenerateXY.Name = "btnGenerateXY";
            this.btnGenerateXY.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateXY.TabIndex = 0;
            this.btnGenerateXY.Text = "生成经纬度坐标";
            this.btnGenerateXY.UseVisualStyleBackColor = true;
            this.btnGenerateXY.Click += new System.EventHandler(this.btnGenerateXY_Click);
            // 
            // btnOpenSourceDir
            // 
            this.btnOpenSourceDir.Location = new System.Drawing.Point(132, 36);
            this.btnOpenSourceDir.Name = "btnOpenSourceDir";
            this.btnOpenSourceDir.Size = new System.Drawing.Size(151, 23);
            this.btnOpenSourceDir.TabIndex = 1;
            this.btnOpenSourceDir.Text = "打开源文件所在文件夹";
            this.btnOpenSourceDir.UseVisualStyleBackColor = true;
            this.btnOpenSourceDir.Click += new System.EventHandler(this.btnOpenSourceDir_Click);
            // 
            // btnSavedFileDir
            // 
            this.btnSavedFileDir.Location = new System.Drawing.Point(132, 157);
            this.btnSavedFileDir.Name = "btnSavedFileDir";
            this.btnSavedFileDir.Size = new System.Drawing.Size(151, 23);
            this.btnSavedFileDir.TabIndex = 2;
            this.btnSavedFileDir.Text = "打开保存文件所在文件夹";
            this.btnSavedFileDir.UseVisualStyleBackColor = true;
            this.btnSavedFileDir.Click += new System.EventHandler(this.btnSavedFileDir_Click);
            // 
            // btnArcGisDataDir
            // 
            this.btnArcGisDataDir.Location = new System.Drawing.Point(122, 213);
            this.btnArcGisDataDir.Name = "btnArcGisDataDir";
            this.btnArcGisDataDir.Size = new System.Drawing.Size(172, 23);
            this.btnArcGisDataDir.TabIndex = 3;
            this.btnArcGisDataDir.Text = "打开ArcGIS数据所在文件夹";
            this.btnArcGisDataDir.UseVisualStyleBackColor = true;
            this.btnArcGisDataDir.Click += new System.EventHandler(this.btnArcGisDataDir_Click);
            // 
            // GenerateXYPositionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 265);
            this.Controls.Add(this.btnArcGisDataDir);
            this.Controls.Add(this.btnSavedFileDir);
            this.Controls.Add(this.btnOpenSourceDir);
            this.Controls.Add(this.btnGenerateXY);
            this.Name = "GenerateXYPositionForm";
            this.Text = "GenerateXYPositionForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateXY;
        private System.Windows.Forms.Button btnOpenSourceDir;
        private System.Windows.Forms.Button btnSavedFileDir;
        private System.Windows.Forms.Button btnArcGisDataDir;
    }
}