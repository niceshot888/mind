
namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    partial class SinaBondForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbxKeywords = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_bond = new System.Windows.Forms.DataGridView();
            this.btnViewFinReport = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_bond)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv_bond, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(363, 366);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnViewFinReport);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.tbxKeywords);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(357, 42);
            this.panel1.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(194, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbxKeywords
            // 
            this.tbxKeywords.Location = new System.Drawing.Point(67, 9);
            this.tbxKeywords.Name = "tbxKeywords";
            this.tbxKeywords.Size = new System.Drawing.Size(121, 21);
            this.tbxKeywords.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "关键词：";
            // 
            // dgv_bond
            // 
            this.dgv_bond.AllowUserToAddRows = false;
            this.dgv_bond.AllowUserToDeleteRows = false;
            this.dgv_bond.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv_bond.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_bond.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_bond.Location = new System.Drawing.Point(3, 51);
            this.dgv_bond.Name = "dgv_bond";
            this.dgv_bond.ReadOnly = true;
            this.dgv_bond.RowTemplate.Height = 23;
            this.dgv_bond.Size = new System.Drawing.Size(357, 312);
            this.dgv_bond.TabIndex = 1;
            this.dgv_bond.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_bond_RowPostPaint);
            // 
            // btnViewFinReport
            // 
            this.btnViewFinReport.Location = new System.Drawing.Point(273, 9);
            this.btnViewFinReport.Name = "btnViewFinReport";
            this.btnViewFinReport.Size = new System.Drawing.Size(75, 23);
            this.btnViewFinReport.TabIndex = 3;
            this.btnViewFinReport.Text = "年报";
            this.btnViewFinReport.UseVisualStyleBackColor = true;
            this.btnViewFinReport.Click += new System.EventHandler(this.btnViewFinReport_Click);
            // 
            // SinaBondForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 366);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SinaBondForm";
            this.Text = "SinaBondForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_bond)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxKeywords;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgv_bond;
        private System.Windows.Forms.Button btnViewFinReport;
    }
}