namespace NiceShot.DotNetWinFormsClient
{
    partial class StockDetailsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbl_stock_details = new System.Windows.Forms.DataGridView();
            this.cms_stockdetails = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_addhd = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_stock_details)).BeginInit();
            this.cms_stockdetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbl_stock_details
            // 
            this.tbl_stock_details.AllowUserToAddRows = false;
            this.tbl_stock_details.AllowUserToDeleteRows = false;
            this.tbl_stock_details.AllowUserToResizeColumns = false;
            this.tbl_stock_details.AllowUserToResizeRows = false;
            this.tbl_stock_details.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)), true);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbl_stock_details.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tbl_stock_details.ColumnHeadersHeight = 40;
            this.tbl_stock_details.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(1)), true);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tbl_stock_details.DefaultCellStyle = dataGridViewCellStyle2;
            this.tbl_stock_details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_stock_details.GridColor = System.Drawing.Color.LightSteelBlue;
            this.tbl_stock_details.Location = new System.Drawing.Point(0, 0);
            this.tbl_stock_details.Name = "tbl_stock_details";
            this.tbl_stock_details.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tbl_stock_details.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.tbl_stock_details.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tbl_stock_details.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.tbl_stock_details.RowTemplate.Height = 23;
            this.tbl_stock_details.Size = new System.Drawing.Size(930, 238);
            this.tbl_stock_details.TabIndex = 0;
            this.tbl_stock_details.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tbl_stock_details_CellMouseDown);
            this.tbl_stock_details.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.tbl_stock_details_RowPostPaint);
            // 
            // cms_stockdetails
            // 
            this.cms_stockdetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_addhd});
            this.cms_stockdetails.Name = "cms_stockdetails";
            this.cms_stockdetails.Size = new System.Drawing.Size(173, 26);
            // 
            // tsmi_addhd
            // 
            this.tsmi_addhd.Name = "tsmi_addhd";
            this.tsmi_addhd.Size = new System.Drawing.Size(172, 22);
            this.tsmi_addhd.Text = "补充研发投入数据";
            this.tsmi_addhd.Click += new System.EventHandler(this.tsmi_addhd_Click);
            // 
            // StockDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 238);
            this.Controls.Add(this.tbl_stock_details);
            this.Name = "StockDetailsForm";
            this.Text = "StockDetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.tbl_stock_details)).EndInit();
            this.cms_stockdetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView tbl_stock_details;
        private System.Windows.Forms.ContextMenuStrip cms_stockdetails;
        private System.Windows.Forms.ToolStripMenuItem tsmi_addhd;
    }
}