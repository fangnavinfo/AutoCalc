namespace AutoIECalcGUI
{
    partial class SelectForm
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
            this.btnWeiya = new System.Windows.Forms.Button();
            this.btnHad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWeiya
            // 
            this.btnWeiya.Location = new System.Drawing.Point(27, 45);
            this.btnWeiya.Name = "btnWeiya";
            this.btnWeiya.Size = new System.Drawing.Size(135, 23);
            this.btnWeiya.TabIndex = 0;
            this.btnWeiya.Text = "解算PostT";
            this.btnWeiya.UseVisualStyleBackColor = true;
            this.btnWeiya.Click += new System.EventHandler(this.btnWeiya_Click);
            // 
            // btnHad
            // 
            this.btnHad.Location = new System.Drawing.Point(27, 85);
            this.btnHad.Name = "btnHad";
            this.btnHad.Size = new System.Drawing.Size(135, 23);
            this.btnHad.TabIndex = 1;
            this.btnHad.Text = "畸变纠正";
            this.btnHad.UseVisualStyleBackColor = true;
            this.btnHad.Click += new System.EventHandler(this.btnHad_Click);
            // 
            // SelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 179);
            this.Controls.Add(this.btnHad);
            this.Controls.Add(this.btnWeiya);
            this.Name = "SelectForm";
            this.Text = "SelectCalc";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWeiya;
        private System.Windows.Forms.Button btnHad;
    }
}