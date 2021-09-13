
namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    partial class AddHDForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxDate = new System.Windows.Forms.ComboBox();
            this.tbxHd = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxHdRatio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxZbh = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxZbhRatio = new System.Windows.Forms.TextBox();
            this.tbxBonus = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxSales = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxLXZBH = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxTotalJKFY = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "日期：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "研发投入：";
            // 
            // cbxDate
            // 
            this.cbxDate.FormattingEnabled = true;
            this.cbxDate.Location = new System.Drawing.Point(107, 23);
            this.cbxDate.Name = "cbxDate";
            this.cbxDate.Size = new System.Drawing.Size(121, 20);
            this.cbxDate.TabIndex = 3;
            this.cbxDate.SelectedIndexChanged += new System.EventHandler(this.cbxDate_SelectedIndexChanged);
            this.cbxDate.SelectedValueChanged += new System.EventHandler(this.cbxDate_SelectedValueChanged);
            // 
            // tbxHd
            // 
            this.tbxHd.Location = new System.Drawing.Point(107, 60);
            this.tbxHd.Name = "tbxHd";
            this.tbxHd.Size = new System.Drawing.Size(121, 21);
            this.tbxHd.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(525, 145);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存数据";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(28, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 39);
            this.label4.TabIndex = 6;
            this.label4.Text = "研发投入占营收比：";
            // 
            // tbxHdRatio
            // 
            this.tbxHdRatio.Location = new System.Drawing.Point(107, 103);
            this.tbxHdRatio.Name = "tbxHdRatio";
            this.tbxHdRatio.Size = new System.Drawing.Size(78, 21);
            this.tbxHdRatio.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "%";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(267, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 41);
            this.label6.TabIndex = 9;
            this.label6.Text = "研发投入资本化：";
            // 
            // tbxZbh
            // 
            this.tbxZbh.Location = new System.Drawing.Point(357, 59);
            this.tbxZbh.Name = "tbxZbh";
            this.tbxZbh.Size = new System.Drawing.Size(121, 21);
            this.tbxZbh.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(267, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 39);
            this.label7.TabIndex = 11;
            this.label7.Text = "研发投入资本化的比重：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(444, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "%";
            // 
            // tbxZbhRatio
            // 
            this.tbxZbhRatio.Location = new System.Drawing.Point(357, 103);
            this.tbxZbhRatio.Name = "tbxZbhRatio";
            this.tbxZbhRatio.Size = new System.Drawing.Size(78, 21);
            this.tbxZbhRatio.TabIndex = 12;
            // 
            // tbxBonus
            // 
            this.tbxBonus.Location = new System.Drawing.Point(107, 144);
            this.tbxBonus.Name = "tbxBonus";
            this.tbxBonus.Size = new System.Drawing.Size(121, 21);
            this.tbxBonus.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "分红：";
            // 
            // tbxSales
            // 
            this.tbxSales.Location = new System.Drawing.Point(357, 144);
            this.tbxSales.Name = "tbxSales";
            this.tbxSales.Size = new System.Drawing.Size(121, 21);
            this.tbxSales.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(270, 150);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "销售金额：";
            // 
            // tbxLXZBH
            // 
            this.tbxLXZBH.Location = new System.Drawing.Point(610, 57);
            this.tbxLXZBH.Name = "tbxLXZBH";
            this.tbxLXZBH.Size = new System.Drawing.Size(121, 21);
            this.tbxLXZBH.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(523, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 18;
            this.label11.Text = "利息资本化：";
            // 
            // tbxTotalJKFY
            // 
            this.tbxTotalJKFY.Location = new System.Drawing.Point(610, 100);
            this.tbxTotalJKFY.Name = "tbxTotalJKFY";
            this.tbxTotalJKFY.Size = new System.Drawing.Size(121, 21);
            this.tbxTotalJKFY.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(523, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 28);
            this.label12.TabIndex = 20;
            this.label12.Text = "借款费用资本化总额：";
            // 
            // AddHDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 189);
            this.Controls.Add(this.tbxTotalJKFY);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbxLXZBH);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbxSales);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbxBonus);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbxZbhRatio);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbxZbh);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxHdRatio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbxHd);
            this.Controls.Add(this.cbxDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddHDForm";
            this.Text = "AddHDForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDate;
        private System.Windows.Forms.TextBox tbxHd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxHdRatio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxZbh;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxZbhRatio;
        private System.Windows.Forms.TextBox tbxBonus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxSales;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxLXZBH;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxTotalJKFY;
        private System.Windows.Forms.Label label12;
    }
}