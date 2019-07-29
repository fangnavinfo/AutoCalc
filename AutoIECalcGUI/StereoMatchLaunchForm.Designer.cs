namespace AutoIECalcGUI
{
    partial class StereoMatchLaunchForm
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
            this.exePathEdit = new System.Windows.Forms.TextBox();
            this.exePathBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rootPathEdit = new System.Windows.Forms.TextBox();
            this.rootPathBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ccdCSVPathEdit = new System.Windows.Forms.TextBox();
            this.ccdCSVBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.inPathEdit = new System.Windows.Forms.TextBox();
            this.inPathBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.exPathEdit = new System.Windows.Forms.TextBox();
            this.exPathBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.outputPathEdit = new System.Windows.Forms.TextBox();
            this.outputPathBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exePathEdit
            // 
            this.exePathEdit.Location = new System.Drawing.Point(223, 44);
            this.exePathEdit.Name = "exePathEdit";
            this.exePathEdit.ReadOnly = true;
            this.exePathEdit.Size = new System.Drawing.Size(349, 21);
            this.exePathEdit.TabIndex = 3;
            this.exePathEdit.TextChanged += new System.EventHandler(this.IEPathEdit_TextChanged);
            // 
            // exePathBtn
            // 
            this.exePathBtn.Location = new System.Drawing.Point(606, 42);
            this.exePathBtn.Name = "exePathBtn";
            this.exePathBtn.Size = new System.Drawing.Size(75, 23);
            this.exePathBtn.TabIndex = 4;
            this.exePathBtn.Text = "选择";
            this.exePathBtn.UseVisualStyleBackColor = true;
            this.exePathBtn.Click += new System.EventHandler(this.exePathBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "StereoMatch.exe 路径";
            // 
            // rootPathEdit
            // 
            this.rootPathEdit.Location = new System.Drawing.Point(223, 85);
            this.rootPathEdit.Name = "rootPathEdit";
            this.rootPathEdit.ReadOnly = true;
            this.rootPathEdit.Size = new System.Drawing.Size(349, 21);
            this.rootPathEdit.TabIndex = 6;
            this.rootPathEdit.TextChanged += new System.EventHandler(this.rootPathEdit_TextChanged);
            // 
            // rootPathBtn
            // 
            this.rootPathBtn.Location = new System.Drawing.Point(606, 83);
            this.rootPathBtn.Name = "rootPathBtn";
            this.rootPathBtn.Size = new System.Drawing.Size(75, 23);
            this.rootPathBtn.TabIndex = 7;
            this.rootPathBtn.Text = "选择";
            this.rootPathBtn.UseVisualStyleBackColor = true;
            this.rootPathBtn.Click += new System.EventHandler(this.rootPathBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "采集数据根路径";
            // 
            // ccdCSVPathEdit
            // 
            this.ccdCSVPathEdit.Location = new System.Drawing.Point(223, 112);
            this.ccdCSVPathEdit.Name = "ccdCSVPathEdit";
            this.ccdCSVPathEdit.ReadOnly = true;
            this.ccdCSVPathEdit.Size = new System.Drawing.Size(349, 21);
            this.ccdCSVPathEdit.TabIndex = 9;
            // 
            // ccdCSVBtn
            // 
            this.ccdCSVBtn.Location = new System.Drawing.Point(606, 110);
            this.ccdCSVBtn.Name = "ccdCSVBtn";
            this.ccdCSVBtn.Size = new System.Drawing.Size(75, 23);
            this.ccdCSVBtn.TabIndex = 10;
            this.ccdCSVBtn.Text = "选择";
            this.ccdCSVBtn.UseVisualStyleBackColor = true;
            this.ccdCSVBtn.Click += new System.EventHandler(this.ccdCSVBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "立体像对文件";
            // 
            // inPathEdit
            // 
            this.inPathEdit.Location = new System.Drawing.Point(223, 139);
            this.inPathEdit.Name = "inPathEdit";
            this.inPathEdit.ReadOnly = true;
            this.inPathEdit.Size = new System.Drawing.Size(349, 21);
            this.inPathEdit.TabIndex = 12;
            // 
            // inPathBtn
            // 
            this.inPathBtn.Location = new System.Drawing.Point(606, 137);
            this.inPathBtn.Name = "inPathBtn";
            this.inPathBtn.Size = new System.Drawing.Size(75, 23);
            this.inPathBtn.TabIndex = 13;
            this.inPathBtn.Text = "选择";
            this.inPathBtn.UseVisualStyleBackColor = true;
            this.inPathBtn.Click += new System.EventHandler(this.inPathBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "相机畸变参数";
            // 
            // exPathEdit
            // 
            this.exPathEdit.Location = new System.Drawing.Point(223, 166);
            this.exPathEdit.Name = "exPathEdit";
            this.exPathEdit.ReadOnly = true;
            this.exPathEdit.Size = new System.Drawing.Size(349, 21);
            this.exPathEdit.TabIndex = 15;
            // 
            // exPathBtn
            // 
            this.exPathBtn.Location = new System.Drawing.Point(606, 164);
            this.exPathBtn.Name = "exPathBtn";
            this.exPathBtn.Size = new System.Drawing.Size(75, 23);
            this.exPathBtn.TabIndex = 16;
            this.exPathBtn.Text = "选择";
            this.exPathBtn.UseVisualStyleBackColor = true;
            this.exPathBtn.Click += new System.EventHandler(this.exPathBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "立体标定参数";
            // 
            // outputPathEdit
            // 
            this.outputPathEdit.Location = new System.Drawing.Point(223, 193);
            this.outputPathEdit.Name = "outputPathEdit";
            this.outputPathEdit.ReadOnly = true;
            this.outputPathEdit.Size = new System.Drawing.Size(349, 21);
            this.outputPathEdit.TabIndex = 18;
            // 
            // outputPathBtn
            // 
            this.outputPathBtn.Location = new System.Drawing.Point(606, 191);
            this.outputPathBtn.Name = "outputPathBtn";
            this.outputPathBtn.Size = new System.Drawing.Size(75, 23);
            this.outputPathBtn.TabIndex = 19;
            this.outputPathBtn.Text = "选择";
            this.outputPathBtn.UseVisualStyleBackColor = true;
            this.outputPathBtn.Click += new System.EventHandler(this.outputPathBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 20;
            this.label6.Text = "立体校正输出";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(606, 230);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 21;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // StereoMatchLaunchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 281);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.outputPathEdit);
            this.Controls.Add(this.outputPathBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.exPathEdit);
            this.Controls.Add(this.exPathBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.inPathEdit);
            this.Controls.Add(this.inPathBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ccdCSVPathEdit);
            this.Controls.Add(this.ccdCSVBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rootPathEdit);
            this.Controls.Add(this.rootPathBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.exePathEdit);
            this.Controls.Add(this.exePathBtn);
            this.Controls.Add(this.label1);
            this.Name = "StereoMatchLaunchForm";
            this.Text = "StereoMatchLaunchForm";
            this.Load += new System.EventHandler(this.StereoMatchLaunchForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox exePathEdit;
        private System.Windows.Forms.Button exePathBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rootPathEdit;
        private System.Windows.Forms.Button rootPathBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ccdCSVPathEdit;
        private System.Windows.Forms.Button ccdCSVBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox inPathEdit;
        private System.Windows.Forms.Button inPathBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox exPathEdit;
        private System.Windows.Forms.Button exPathBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox outputPathEdit;
        private System.Windows.Forms.Button outputPathBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnStart;
    }
}