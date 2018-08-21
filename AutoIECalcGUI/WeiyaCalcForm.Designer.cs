namespace AutoIECalcGUI
{
    partial class WeiyaCalcForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.IEPathEdit = new System.Windows.Forms.TextBox();
            this.IEPathBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BasePathBtn = new System.Windows.Forms.Button();
            this.BasePathEdit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BaseStationLat = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.BaseStationLon = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.BaseStationHeight = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.LeverArmOffsetX = new System.Windows.Forms.MaskedTextBox();
            this.LeverArmOffsetY = new System.Windows.Forms.MaskedTextBox();
            this.LeverArmOffsetZ = new System.Windows.Forms.MaskedTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.OutputPathBtn = new System.Windows.Forms.Button();
            this.OutputPathEdit = new System.Windows.Forms.TextBox();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.AntennaHeight = new System.Windows.Forms.MaskedTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RoverPathEdit = new System.Windows.Forms.TextBox();
            this.RoverPathBtn = new System.Windows.Forms.Button();
            this.CalcBaseLocBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.OutPutFileName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IEPathEdit
            // 
            this.IEPathEdit.Location = new System.Drawing.Point(218, 34);
            this.IEPathEdit.Name = "IEPathEdit";
            this.IEPathEdit.Size = new System.Drawing.Size(349, 21);
            this.IEPathEdit.TabIndex = 0;
            // 
            // IEPathBtn
            // 
            this.IEPathBtn.Location = new System.Drawing.Point(581, 33);
            this.IEPathBtn.Name = "IEPathBtn";
            this.IEPathBtn.Size = new System.Drawing.Size(75, 23);
            this.IEPathBtn.TabIndex = 1;
            this.IEPathBtn.Text = "选择";
            this.IEPathBtn.UseVisualStyleBackColor = true;
            this.IEPathBtn.Click += new System.EventHandler(this.IEPathBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Inertial Explorer V8.60 路径";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "基站数据路径";
            // 
            // BasePathBtn
            // 
            this.BasePathBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.BasePathBtn.Location = new System.Drawing.Point(581, 73);
            this.BasePathBtn.Name = "BasePathBtn";
            this.BasePathBtn.Size = new System.Drawing.Size(75, 23);
            this.BasePathBtn.TabIndex = 7;
            this.BasePathBtn.Text = "选择";
            this.BasePathBtn.UseVisualStyleBackColor = true;
            this.BasePathBtn.Click += new System.EventHandler(this.RawDataBtn_Click);
            // 
            // BasePathEdit
            // 
            this.BasePathEdit.Location = new System.Drawing.Point(218, 74);
            this.BasePathEdit.Name = "BasePathEdit";
            this.BasePathEdit.Size = new System.Drawing.Size(349, 21);
            this.BasePathEdit.TabIndex = 6;
            this.BasePathEdit.TextChanged += new System.EventHandler(this.BasePathEdit_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "基站坐标";
            // 
            // BaseStationLat
            // 
            this.BaseStationLat.Location = new System.Drawing.Point(218, 201);
            this.BaseStationLat.Name = "BaseStationLat";
            this.BaseStationLat.Size = new System.Drawing.Size(156, 21);
            this.BaseStationLat.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(201, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "N:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(394, 204);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 22;
            this.label9.Text = "E:";
            // 
            // BaseStationLon
            // 
            this.BaseStationLon.Location = new System.Drawing.Point(417, 201);
            this.BaseStationLon.Name = "BaseStationLon";
            this.BaseStationLon.Size = new System.Drawing.Size(150, 21);
            this.BaseStationLon.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(34, 237);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "基站高度";
            // 
            // BaseStationHeight
            // 
            this.BaseStationHeight.Location = new System.Drawing.Point(218, 228);
            this.BaseStationHeight.Name = "BaseStationHeight";
            this.BaseStationHeight.Size = new System.Drawing.Size(267, 21);
            this.BaseStationHeight.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(357, 303);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 29;
            this.label11.Text = "Y:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(198, 303);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 27;
            this.label12.Text = "X:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(34, 309);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 12);
            this.label13.TabIndex = 25;
            this.label13.Text = "立体系统天线偏移量";
            // 
            // LeverArmOffsetX
            // 
            this.LeverArmOffsetX.Location = new System.Drawing.Point(218, 300);
            this.LeverArmOffsetX.Name = "LeverArmOffsetX";
            this.LeverArmOffsetX.Size = new System.Drawing.Size(104, 21);
            this.LeverArmOffsetX.TabIndex = 30;
            // 
            // LeverArmOffsetY
            // 
            this.LeverArmOffsetY.Location = new System.Drawing.Point(380, 300);
            this.LeverArmOffsetY.Name = "LeverArmOffsetY";
            this.LeverArmOffsetY.Size = new System.Drawing.Size(104, 21);
            this.LeverArmOffsetY.TabIndex = 31;
            this.LeverArmOffsetY.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.LeverArmOffsetY_MaskInputRejected);
            // 
            // LeverArmOffsetZ
            // 
            this.LeverArmOffsetZ.Location = new System.Drawing.Point(524, 300);
            this.LeverArmOffsetZ.Name = "LeverArmOffsetZ";
            this.LeverArmOffsetZ.Size = new System.Drawing.Size(104, 21);
            this.LeverArmOffsetZ.TabIndex = 33;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(510, 303);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 12);
            this.label14.TabIndex = 32;
            this.label14.Text = "Z:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 36;
            this.label6.Text = "解算输出文件路径";
            // 
            // OutputPathBtn
            // 
            this.OutputPathBtn.Location = new System.Drawing.Point(581, 137);
            this.OutputPathBtn.Name = "OutputPathBtn";
            this.OutputPathBtn.Size = new System.Drawing.Size(75, 23);
            this.OutputPathBtn.TabIndex = 35;
            this.OutputPathBtn.Text = "选择";
            this.OutputPathBtn.UseVisualStyleBackColor = true;
            this.OutputPathBtn.Click += new System.EventHandler(this.OutputPathBtn_Click);
            // 
            // OutputPathEdit
            // 
            this.OutputPathEdit.Location = new System.Drawing.Point(218, 138);
            this.OutputPathEdit.Name = "OutputPathEdit";
            this.OutputPathEdit.Size = new System.Drawing.Size(349, 21);
            this.OutputPathEdit.TabIndex = 34;
            this.OutputPathEdit.TextChanged += new System.EventHandler(this.OutputPathEdit_TextChanged);
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Location = new System.Drawing.Point(581, 361);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(75, 23);
            this.ConfirmBtn.TabIndex = 37;
            this.ConfirmBtn.Text = "后台解算";
            this.ConfirmBtn.UseVisualStyleBackColor = true;
            this.ConfirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // AntennaHeight
            // 
            this.AntennaHeight.Location = new System.Drawing.Point(218, 255);
            this.AntennaHeight.Name = "AntennaHeight";
            this.AntennaHeight.Size = new System.Drawing.Size(267, 21);
            this.AntennaHeight.TabIndex = 39;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(34, 264);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 38;
            this.label15.Text = "基站天线高";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 40;
            this.label2.Text = "流动站数据路径";
            // 
            // RoverPathEdit
            // 
            this.RoverPathEdit.Location = new System.Drawing.Point(218, 103);
            this.RoverPathEdit.Name = "RoverPathEdit";
            this.RoverPathEdit.Size = new System.Drawing.Size(349, 21);
            this.RoverPathEdit.TabIndex = 41;
            this.RoverPathEdit.TextChanged += new System.EventHandler(this.RoverPathEdit_TextChanged);
            // 
            // RoverPathBtn
            // 
            this.RoverPathBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.RoverPathBtn.Location = new System.Drawing.Point(581, 102);
            this.RoverPathBtn.Name = "RoverPathBtn";
            this.RoverPathBtn.Size = new System.Drawing.Size(75, 23);
            this.RoverPathBtn.TabIndex = 42;
            this.RoverPathBtn.Text = "选择";
            this.RoverPathBtn.UseVisualStyleBackColor = true;
            this.RoverPathBtn.Click += new System.EventHandler(this.RoverPathBtn_Click);
            // 
            // CalcBaseLocBtn
            // 
            this.CalcBaseLocBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.CalcBaseLocBtn.Location = new System.Drawing.Point(581, 194);
            this.CalcBaseLocBtn.Name = "CalcBaseLocBtn";
            this.CalcBaseLocBtn.Size = new System.Drawing.Size(75, 23);
            this.CalcBaseLocBtn.TabIndex = 43;
            this.CalcBaseLocBtn.Text = "解基站坐标";
            this.CalcBaseLocBtn.UseVisualStyleBackColor = true;
            this.CalcBaseLocBtn.Click += new System.EventHandler(this.CalcBaseLocBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(662, 361);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 44;
            this.button1.Text = "界面解算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 45;
            this.label4.Text = "解算输出文件名";
            // 
            // OutPutFileName
            // 
            this.OutPutFileName.Location = new System.Drawing.Point(218, 165);
            this.OutPutFileName.Name = "OutPutFileName";
            this.OutPutFileName.Size = new System.Drawing.Size(175, 21);
            this.OutPutFileName.TabIndex = 46;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(394, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 47;
            this.label5.Text = ".PostT";
            // 
            // WeiyaCalcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 396);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.OutPutFileName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CalcBaseLocBtn);
            this.Controls.Add(this.RoverPathBtn);
            this.Controls.Add(this.RoverPathEdit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IEPathEdit);
            this.Controls.Add(this.ConfirmBtn);
            this.Controls.Add(this.AntennaHeight);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.IEPathBtn);
            this.Controls.Add(this.BaseStationHeight);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.BasePathEdit);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.BasePathBtn);
            this.Controls.Add(this.LeverArmOffsetX);
            this.Controls.Add(this.OutputPathBtn);
            this.Controls.Add(this.BaseStationLon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LeverArmOffsetY);
            this.Controls.Add(this.OutputPathEdit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.LeverArmOffsetZ);
            this.Controls.Add(this.BaseStationLat);
            this.Name = "WeiyaCalcForm";
            this.Text = "WeiyaCalcForm";
            this.Load += new System.EventHandler(this.AutoIECalcForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IEPathEdit;
        private System.Windows.Forms.Button IEPathBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BasePathBtn;
        private System.Windows.Forms.TextBox BasePathEdit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox BaseStationLat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox BaseStationLon;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.MaskedTextBox BaseStationHeight;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MaskedTextBox LeverArmOffsetX;
        private System.Windows.Forms.MaskedTextBox LeverArmOffsetY;
        private System.Windows.Forms.MaskedTextBox LeverArmOffsetZ;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button OutputPathBtn;
        private System.Windows.Forms.TextBox OutputPathEdit;
        private System.Windows.Forms.Button ConfirmBtn;
        private System.Windows.Forms.MaskedTextBox AntennaHeight;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RoverPathEdit;
        private System.Windows.Forms.Button RoverPathBtn;
        private System.Windows.Forms.Button CalcBaseLocBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox OutPutFileName;
        private System.Windows.Forms.Label label5;
    }
}

