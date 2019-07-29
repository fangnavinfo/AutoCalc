using System;

namespace AutoIECalcGUI
{
    partial class MultiCalcForm
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
            this.IEPathEdit = new System.Windows.Forms.TextBox();
            this.IEPathBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartCalc = new System.Windows.Forms.Button();
            this.textBoxPrjPath = new System.Windows.Forms.TextBox();
            this.btnSelectRootPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // IEPathEdit
            // 
            this.IEPathEdit.Location = new System.Drawing.Point(244, 22);
            this.IEPathEdit.Name = "IEPathEdit";
            this.IEPathEdit.ReadOnly = true;
            this.IEPathEdit.Size = new System.Drawing.Size(278, 21);
            this.IEPathEdit.TabIndex = 3;
            // 
            // IEPathBtn
            // 
            this.IEPathBtn.Location = new System.Drawing.Point(537, 21);
            this.IEPathBtn.Name = "IEPathBtn";
            this.IEPathBtn.Size = new System.Drawing.Size(75, 23);
            this.IEPathBtn.TabIndex = 4;
            this.IEPathBtn.Text = "选择";
            this.IEPathBtn.UseVisualStyleBackColor = true;
            this.IEPathBtn.Click += new System.EventHandler(this.IEPathBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Inertial Explorer V8.80 路径";
            // 
            // btnStartCalc
            // 
            this.btnStartCalc.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.btnStartCalc.Enabled = false;
            this.btnStartCalc.Location = new System.Drawing.Point(537, 270);
            this.btnStartCalc.Name = "btnStartCalc";
            this.btnStartCalc.Size = new System.Drawing.Size(75, 23);
            this.btnStartCalc.TabIndex = 21;
            this.btnStartCalc.Text = "启动解算";
            this.btnStartCalc.UseVisualStyleBackColor = true;
            this.btnStartCalc.Click += new System.EventHandler(this.btnStartCalc_Click);
            // 
            // textBoxPrjPath
            // 
            this.textBoxPrjPath.Location = new System.Drawing.Point(244, 47);
            this.textBoxPrjPath.Name = "textBoxPrjPath";
            this.textBoxPrjPath.ReadOnly = true;
            this.textBoxPrjPath.Size = new System.Drawing.Size(278, 21);
            this.textBoxPrjPath.TabIndex = 25;
            // 
            // btnSelectRootPath
            // 
            this.btnSelectRootPath.Enabled = false;
            this.btnSelectRootPath.Location = new System.Drawing.Point(537, 46);
            this.btnSelectRootPath.Name = "btnSelectRootPath";
            this.btnSelectRootPath.Size = new System.Drawing.Size(75, 23);
            this.btnSelectRootPath.TabIndex = 26;
            this.btnSelectRootPath.Text = "选择";
            this.btnSelectRootPath.UseVisualStyleBackColor = true;
            this.btnSelectRootPath.Click += new System.EventHandler(this.btnSelectRootPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "批量解算工程的根路径";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(39, 94);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(623, 150);
            this.dataGridView1.TabIndex = 0;
            // 
            // MultiCalcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 305);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBoxPrjPath);
            this.Controls.Add(this.btnSelectRootPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnStartCalc);
            this.Controls.Add(this.IEPathEdit);
            this.Controls.Add(this.IEPathBtn);
            this.Controls.Add(this.label1);
            this.Name = "MultiCalcForm";
            this.Text = "MultiCalcForm";
            this.Load += new System.EventHandler(this.MultiCalcForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IEPathEdit;
        private System.Windows.Forms.Button IEPathBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartCalc;
        private System.Windows.Forms.TextBox textBoxPrjPath;
        private System.Windows.Forms.Button btnSelectRootPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;

        private DateTime startTime;
    }
}