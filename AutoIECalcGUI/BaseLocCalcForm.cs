using AutoIECalcPublic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoIECalcGUI
{
    public partial class BaseLocCalcForm : Form
    {
        public BaseLocCalcForm()
        {
            InitializeComponent();

            textAntenaProfile.Text = Program.Config.AntennaProfile;
            textSlantMeasurement.Text = Program.Config.SlantMeasure;
            textRadiusGround.Text = Program.Config.RadiusGround;
            textOffsetARP2Ground.Text = Program.Config.OffsetARP2Ground;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Calc();
        }

        private void AntennaCalcForm_Load(object sender, EventArgs e)
        {

        }

        private void btnForGround_Click(object sender, EventArgs e)
        {
            Calc("HIDE");
        }

        private void Calc(string mode="")
        {
            Program.Config.AntennaProfile = textAntenaProfile.Text;
            Program.Config.SlantMeasure = textSlantMeasurement.Text;
            Program.Config.RadiusGround = textRadiusGround.Text;
            Program.Config.OffsetARP2Ground = textOffsetARP2Ground.Text;

            Program.Config.Save();
            Program.Config.Save(ConfigSetting.BaseConfigPath);

            Program.Config.startTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            Process cmd = Process.Start("AutoIECalcCmd.exe", "BASE " + mode);


            btnConfirm.Enabled = false;
            btnForGround.Enabled = false;

            textRadiusGround.Enabled = false;
            textAntenaProfile.Enabled = false;
            textSlantMeasurement.Enabled = false;
            textOffsetARP2Ground.Enabled = false;

            cmd.WaitForExit();

            Program.Config.endTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            if (File.Exists(Program.Config.GetCalcBaseOutputPath()))
            {
                List<string> allLines = System.IO.File.ReadAllLines(Program.Config.GetCalcBaseOutputPath()).ToList();
                allLines.RemoveAll(x => x.Length == 0);

                string lastestLine = allLines.Last();
                List<string> elems = lastestLine.Split(" ".ToCharArray()).ToList();
                elems.RemoveAll(x => x.Length == 0);

                Program.Config.Lat[0] = elems[0];
                Program.Config.Lat[1] = elems[1];
                Program.Config.Lat[2] = elems[2];
                Program.Config.Lon[0] = elems[3];
                Program.Config.Lon[1] = elems[4];
                Program.Config.Lon[2] = elems[5];

                Program.Config.BasetStationHeight = elems[6];
            }

            Close();
        }
    }
}
