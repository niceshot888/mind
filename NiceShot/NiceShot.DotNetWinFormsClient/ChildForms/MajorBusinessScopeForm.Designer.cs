
namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    partial class MajorBusinessScopeForm
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
            this.dgv_m_biz_scope = new System.Windows.Forms.DataGridView();
            this.cms_details = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_view_report = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_setbgcolor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_setpink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_setblue = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_setgreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_setpurple = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_settransparent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_hideotherdata = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_showalldata = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_update_data = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_clearall = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_wdm = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m_biz_scope)).BeginInit();
            this.cms_details.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_m_biz_scope
            // 
            this.dgv_m_biz_scope.AllowUserToAddRows = false;
            this.dgv_m_biz_scope.AllowUserToDeleteRows = false;
            this.dgv_m_biz_scope.AllowUserToResizeColumns = false;
            this.dgv_m_biz_scope.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_m_biz_scope.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_m_biz_scope.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_m_biz_scope.Location = new System.Drawing.Point(0, 0);
            this.dgv_m_biz_scope.Name = "dgv_m_biz_scope";
            this.dgv_m_biz_scope.ReadOnly = true;
            this.dgv_m_biz_scope.RowTemplate.Height = 23;
            this.dgv_m_biz_scope.Size = new System.Drawing.Size(753, 549);
            this.dgv_m_biz_scope.TabIndex = 0;
            this.dgv_m_biz_scope.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_m_biz_scope_CellMouseDown);
            this.dgv_m_biz_scope.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_m_biz_scope_DataBindingComplete);
            this.dgv_m_biz_scope.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_m_biz_scope_RowPostPaint);
            // 
            // cms_details
            // 
            this.cms_details.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_wdm,
            this.tsmi_view_report,
            this.tsmi_setbgcolor,
            this.tsmi_hideotherdata,
            this.tsmi_showalldata,
            this.tsmi_update_data,
            this.tsmi_clearall});
            this.cms_details.Name = "cms_details";
            this.cms_details.Size = new System.Drawing.Size(181, 180);
            // 
            // tsmi_view_report
            // 
            this.tsmi_view_report.Name = "tsmi_view_report";
            this.tsmi_view_report.Size = new System.Drawing.Size(180, 22);
            this.tsmi_view_report.Text = "查看财务报表";
            this.tsmi_view_report.Click += new System.EventHandler(this.tsmi_view_report_Click);
            // 
            // tsmi_setbgcolor
            // 
            this.tsmi_setbgcolor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_setpink,
            this.tsmi_setblue,
            this.tsmi_setgreen,
            this.tsmi_setpurple,
            this.tsmi_settransparent});
            this.tsmi_setbgcolor.Name = "tsmi_setbgcolor";
            this.tsmi_setbgcolor.Size = new System.Drawing.Size(180, 22);
            this.tsmi_setbgcolor.Text = "设置背景色";
            // 
            // tsmi_setpink
            // 
            this.tsmi_setpink.Name = "tsmi_setpink";
            this.tsmi_setpink.Size = new System.Drawing.Size(136, 22);
            this.tsmi_setpink.Text = "设置为粉色";
            this.tsmi_setpink.Click += new System.EventHandler(this.tsmi_setpink_Click);
            // 
            // tsmi_setblue
            // 
            this.tsmi_setblue.Name = "tsmi_setblue";
            this.tsmi_setblue.Size = new System.Drawing.Size(136, 22);
            this.tsmi_setblue.Text = "设置为蓝色";
            this.tsmi_setblue.Click += new System.EventHandler(this.tsmi_setblue_Click);
            // 
            // tsmi_setgreen
            // 
            this.tsmi_setgreen.Name = "tsmi_setgreen";
            this.tsmi_setgreen.Size = new System.Drawing.Size(136, 22);
            this.tsmi_setgreen.Text = "设置为绿色";
            this.tsmi_setgreen.Click += new System.EventHandler(this.tsmi_setgreen_Click);
            // 
            // tsmi_setpurple
            // 
            this.tsmi_setpurple.Name = "tsmi_setpurple";
            this.tsmi_setpurple.Size = new System.Drawing.Size(136, 22);
            this.tsmi_setpurple.Text = "设置为紫色";
            this.tsmi_setpurple.Click += new System.EventHandler(this.tsmi_setpurple_Click);
            // 
            // tsmi_settransparent
            // 
            this.tsmi_settransparent.Name = "tsmi_settransparent";
            this.tsmi_settransparent.Size = new System.Drawing.Size(136, 22);
            this.tsmi_settransparent.Text = "设置为透明";
            this.tsmi_settransparent.Click += new System.EventHandler(this.tsmi_settransparent_Click);
            // 
            // tsmi_hideotherdata
            // 
            this.tsmi_hideotherdata.Name = "tsmi_hideotherdata";
            this.tsmi_hideotherdata.Size = new System.Drawing.Size(180, 22);
            this.tsmi_hideotherdata.Text = "隐藏其他行数据";
            this.tsmi_hideotherdata.Click += new System.EventHandler(this.tsmi_hideotherdata_Click);
            // 
            // tsmi_showalldata
            // 
            this.tsmi_showalldata.Name = "tsmi_showalldata";
            this.tsmi_showalldata.Size = new System.Drawing.Size(180, 22);
            this.tsmi_showalldata.Text = "显示全部行数据";
            this.tsmi_showalldata.Click += new System.EventHandler(this.tsmi_showalldata_Click);
            // 
            // tsmi_update_data
            // 
            this.tsmi_update_data.Name = "tsmi_update_data";
            this.tsmi_update_data.Size = new System.Drawing.Size(180, 22);
            this.tsmi_update_data.Text = "更新主营构成数据";
            this.tsmi_update_data.Click += new System.EventHandler(this.tsmi_update_data_Click);
            // 
            // tsmi_clearall
            // 
            this.tsmi_clearall.Name = "tsmi_clearall";
            this.tsmi_clearall.Size = new System.Drawing.Size(180, 22);
            this.tsmi_clearall.Text = "清除所有背景色";
            this.tsmi_clearall.Click += new System.EventHandler(this.tsmi_clearall_Click);
            // 
            // tsmi_wdm
            // 
            this.tsmi_wdm.Name = "tsmi_wdm";
            this.tsmi_wdm.Size = new System.Drawing.Size(180, 22);
            this.tsmi_wdm.Text = "问董秘";
            this.tsmi_wdm.Click += new System.EventHandler(this.tsmi_wdm_Click);
            // 
            // MajorBusinessScopeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 549);
            this.Controls.Add(this.dgv_m_biz_scope);
            this.Name = "MajorBusinessScopeForm";
            this.Text = "主营构成";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_m_biz_scope)).EndInit();
            this.cms_details.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_m_biz_scope;
        private System.Windows.Forms.ContextMenuStrip cms_details;
        private System.Windows.Forms.ToolStripMenuItem tsmi_setpink;
        private System.Windows.Forms.ToolStripMenuItem tsmi_setblue;
        private System.Windows.Forms.ToolStripMenuItem tsmi_settransparent;
        private System.Windows.Forms.ToolStripMenuItem tsmi_clearall;
        private System.Windows.Forms.ToolStripMenuItem tsmi_setgreen;
        private System.Windows.Forms.ToolStripMenuItem tsmi_hideotherdata;
        private System.Windows.Forms.ToolStripMenuItem tsmi_showalldata;
        private System.Windows.Forms.ToolStripMenuItem tsmi_setbgcolor;
        private System.Windows.Forms.ToolStripMenuItem tsmi_update_data;
        private System.Windows.Forms.ToolStripMenuItem tsmi_setpurple;
        private System.Windows.Forms.ToolStripMenuItem tsmi_view_report;
        private System.Windows.Forms.ToolStripMenuItem tsmi_wdm;
    }
}