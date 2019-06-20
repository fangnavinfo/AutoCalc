using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoIECalcPublic;

namespace AutoIECalcGUI
{
    public partial class SelectForm : Form
    {
        public SelectForm()
        {
            InitializeComponent();
        }

        private void btnWeiya_Click(object sender, EventArgs e)
        {
            this.Hide();

            Program.Config = ConfigSetting.Load(ConfigSetting.WeiyaConfigPath);
            WeiyaCalcForm dialog = new WeiyaCalcForm();
            dialog.ShowDialog();
            this.Close();
        }

        private void btnHad_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            StereoMatchLaunchForm dialog = new StereoMatchLaunchForm();
            dialog.ShowDialog();

            this.Close();
        }
    }
}
