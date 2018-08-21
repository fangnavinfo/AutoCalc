namespace AutoIECalcGUI
{
    partial class BaseLocCalcForm
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
            this.textAntenaProfile = new System.Windows.Forms.TextBox();
            this.textSlantMeasurement = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textRadiusGround = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textOffsetARP2Ground = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnForGround = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "天线类型（Antenna Profile）";
            // 
            // textAntenaProfile
            // 
            this.textAntenaProfile.Location = new System.Drawing.Point(15, 29);
            this.textAntenaProfile.Name = "textAntenaProfile";
            this.textAntenaProfile.Size = new System.Drawing.Size(299, 21);
            this.textAntenaProfile.TabIndex = 1;
            // 
            // textSlantMeasurement
            // 
            this.textSlantMeasurement.Location = new System.Drawing.Point(15, 83);
            this.textSlantMeasurement.Name = "textSlantMeasurement";
            this.textSlantMeasurement.Size = new System.Drawing.Size(299, 21);
            this.textSlantMeasurement.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "天线斜高（Slant Measurement）";
            // 
            // textRadiusGround
            // 
            this.textRadiusGround.Location = new System.Drawing.Point(15, 140);
            this.textRadiusGround.Name = "textRadiusGround";
            this.textRadiusGround.Size = new System.Drawing.Size(299, 21);
            this.textRadiusGround.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "设备半径（Radius of ground plane）";
            // 
            // textOffsetARP2Ground
            // 
            this.textOffsetARP2Ground.Location = new System.Drawing.Point(15, 194);
            this.textOffsetARP2Ground.Name = "textOffsetARP2Ground";
            this.textOffsetARP2Ground.Size = new System.Drawing.Size(299, 21);
            this.textOffsetARP2Ground.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(323, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "设备高度（Offset from ARP to center of ground plane）";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(239, 239);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "界面解算";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnForGround
            // 
            this.btnForGround.Location = new System.Drawing.Point(158, 239);
            this.btnForGround.Name = "btnForGround";
            this.btnForGround.Size = new System.Drawing.Size(75, 23);
            this.btnForGround.TabIndex = 9;
            this.btnForGround.Text = "后台解算";
            this.btnForGround.UseVisualStyleBackColor = true;
            this.btnForGround.Click += new System.EventHandler(this.btnForGround_Click);
            // 
            // BaseLocCalcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 283);
            this.Controls.Add(this.btnForGround);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.textOffsetARP2Ground);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textRadiusGround);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textSlantMeasurement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textAntenaProfile);
            this.Controls.Add(this.label1);
            this.Name = "BaseLocCalcForm";
            this.Text = "BaseLocCalc";
            this.Load += new System.EventHandler(this.AntennaCalcForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textAntenaProfile;
        private System.Windows.Forms.TextBox textSlantMeasurement;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textRadiusGround;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textOffsetARP2Ground;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnForGround;
    }
}