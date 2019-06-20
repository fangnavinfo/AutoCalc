﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AutoIECalcPublic;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoIECalcGUI
{
    public partial class WeiyaCalcForm : Form
    {
        public WeiyaCalcForm()
        {
            InitializeComponent();

            rawRootPath.Text = Program.Config.RawPath;
            IEPathEdit.Text = Program.Config.IEPath;

            BasePathEdit.Text = Program.Config.BaseDataPath;
            RoverPathEdit.Text = Program.Config.RoverDataPath;
            OutputPathEdit.Text = Program.Config.OutputPath;
            OutPutFileName.Text = Program.Config.outputName;

            BaseStationHeight.Text = Program.Config.BasetStationHeight;
            AntennaHeight.Text = Program.Config.AntennaMeasureHeight;

            BaseStationLat.Text = string.Format("{0}:{1}:{2}", Program.Config.Lat[0], Program.Config.Lat[1], Program.Config.Lat[2]);
            BaseStationLon.Text = string.Format("{0}:{1}:{2}", Program.Config.Lon[0], Program.Config.Lon[1], Program.Config.Lon[2]);
            if (BaseStationLat.Text == "::")
            {
                BaseStationLat.Text = "";
            }
            if (BaseStationLon.Text == "::")
            {
                BaseStationLon.Text = "";
            }

            LeverArmOffsetX.Text = Program.Config.LeverArmOffsetX;
            LeverArmOffsetY.Text = Program.Config.LeverArmOffsetY;
            LeverArmOffsetZ.Text = Program.Config.LeverArmOffsetZ;
        }

        private void AutoIECalcForm_Load(object sender, EventArgs e)
        {

        }
        

        private void IEPathBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                IEPathEdit.Text = dialog.SelectedPath + @"\";
            }
        }

        private void RawDataBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                BasePathEdit.Text = dialog.SelectedPath + @"\";
            }
        }

        private void RoverPathBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                RoverPathEdit.Text = dialog.SelectedPath + @"\";
            }
        }

        private void CalcBaseLocBtn_Click(object sender, EventArgs e)
        {
            Program.Config.RawPath = rawRootPath.Text;
            Program.Config.IEPath = IEPathEdit.Text;

            Program.Config.BaseDataPath = BasePathEdit.Text;
            Program.Config.RoverDataPath = RoverPathEdit.Text;
            Program.Config.OutputPath = OutputPathEdit.Text;
            Program.Config.outputName = OutPutFileName.Text;

            Program.Config.BasetStationHeight = BaseStationHeight.Text;
            Program.Config.AntennaMeasureHeight = AntennaHeight.Text;

            //Program.Config.Save();
            //var dialog = new BaseLocCalcForm();
            //dialog.ShowDialog(this);
            CalcBase();

            BaseStationLat.Text = string.Format("{0}:{1}:{2}", Program.Config.Lat[0], Program.Config.Lat[1], Program.Config.Lat[2]);
            BaseStationLon.Text = string.Format("{0}:{1}:{2}", Program.Config.Lon[0], Program.Config.Lon[1], Program.Config.Lon[2]);
            BaseStationHeight.Text = Program.Config.BasetStationHeight;

            Program.Config.Save();
        }

        private void OutputPathBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                OutputPathEdit.Text = dialog.SelectedPath + @"\";
            }
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            Calc("HIDE");
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calc();
        }

        private void Calc(string mode ="")
        {
            try
            {
                Program.Config.RawPath = rawRootPath.Text;
                Program.Config.IEPath = IEPathEdit.Text;

                Program.Config.BaseDataPath = BasePathEdit.Text;
                Program.Config.RoverDataPath = RoverPathEdit.Text;
                Program.Config.OutputPath = OutputPathEdit.Text;
                Program.Config.outputName = OutPutFileName.Text;

                Program.Config.BasetStationHeight = BaseStationHeight.Text;
                Program.Config.AntennaMeasureHeight = AntennaHeight.Text;

                string[] Lat = BaseStationLat.Text.Split(":".ToCharArray());
                Program.Config.Lat[0] = Lat[0];
                Program.Config.Lat[1] = Lat[1];
                Program.Config.Lat[2] = Lat[2];

                string[] Lon = BaseStationLon.Text.Split(":".ToCharArray());
                Program.Config.Lon[0] = Lon[0];
                Program.Config.Lon[1] = Lon[1];
                Program.Config.Lon[2] = Lon[2];

                Program.Config.LeverArmOffsetX = LeverArmOffsetX.Text;
                Program.Config.LeverArmOffsetY = LeverArmOffsetY.Text;
                Program.Config.LeverArmOffsetZ = LeverArmOffsetZ.Text;

                CheckArgumentVaild();

                Program.Config.Save();

                Process.Start("AutoIECalcCmd.exe", "WEIYA " + mode);

                Application.Exit();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void CheckArgumentVaild()
        {
            if (!Directory.EnumerateFiles(BasePathEdit.Text, "*.1?o").Any() && !Directory.EnumerateFiles(BasePathEdit.Text, "*.LOG").Any()
                && !Directory.EnumerateFiles(BasePathEdit.Text, "*.DAT").Any())
            {
                //throw new ArgumentException("基站目录无法找到 *.1?o/*.LOG/*.DAT 文件 " + BasePathEdit.Text);
            }

            if (!Directory.EnumerateFiles(RoverPathEdit.Text, "*.TXT").Any()
                && !Directory.EnumerateFiles(RoverPathEdit.Text, "*.gps").Any())
            {
                throw new ArgumentException("流动站目录无法找到 *.TXT/*.gps 文件 " + RoverPathEdit.Text);
            }

            if(File.Exists(Program.Config.GetProjectCfgPath()))
            {
                throw new ArgumentException("解算工程文件已经存在，请删除后再进行解算 " + Program.Config.GetProjectCfgPath());
            }

            if (File.Exists(Program.Config.GetPostprocessPath()))
            {
                throw new ArgumentException("解算输出文件已经存在，请删除后再进行解算 " + Program.Config.GetPostprocessPath());
            }

            //if (!Directory.EnumerateFiles(RoverPathEdit.Text, "*.imr").Any())
            //{
            //    throw new ArgumentException("流动站目录无法找到 *.imr 文件 " + RoverPathEdit.Text);
            //}

            if (BaseStationLat.Text.Count(x => x == ':') != 2 || BaseStationLon.Text.Count(x => x == ':') != 2)
            {
                throw new ArgumentException("基站坐标格式必须为“XX:XX:XX”!");
            }
        }

        private void LeverArmOffsetY_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void OutputPathEdit_TextChanged(object sender, EventArgs e)
        {

        }

        private void BasePathEdit_TextChanged(object sender, EventArgs e)
        {

        }

        private void RoverPathEdit_TextChanged(object sender, EventArgs e)
        {
            string pattern = "ROVER";

            bool isReplace = false;
            string OutputPath = Regex.Replace(RoverPathEdit.Text, pattern, new MatchEvaluator((Match mc) =>
                {
                    isReplace = true;
                    return "Preprocess";
                }), 
                RegexOptions.RightToLeft | RegexOptions.IgnoreCase);

            if (isReplace)
            {
                OutputPathEdit.Text = OutputPath.Replace(@"RawData\", "");
            }

            pattern = @"@@.*?\\";
            var match = Regex.Match(RoverPathEdit.Text, pattern);
            if (match.Success)
            {
                OutPutFileName.Text = match.Value.Replace("@@", "").Replace(@"\", "");
            }
        }

        private void CalcBase(string mode = "")
        {
            //Program.Config.AntennaProfile = textAntenaProfile.Text;
            //Program.Config.SlantMeasure = textSlantMeasurement.Text;
            //Program.Config.RadiusGround = textRadiusGround.Text;
            //Program.Config.OffsetARP2Ground = textOffsetARP2Ground.Text;

            Program.Config.Save();
            Program.Config.Save(ConfigSetting.BaseConfigPath);

            Process cmd = Process.Start("AutoIECalcCmd.exe", "BASE " + mode);

            //btnConfirm.Enabled = false;
            //btnForGround.Enabled = false;

            //textRadiusGround.Enabled = false;
            //textAntenaProfile.Enabled = false;
            //textSlantMeasurement.Enabled = false;
            //textOffsetARP2Ground.Enabled = false;

            cmd.WaitForExit();

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

        private void RawRootPathBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                if (!dialog.SelectedPath.Split('\\').Last().StartsWith("@@"))
                {
                    MessageBox.Show(this, "采集数据根目录必须以\"@@\"开头");
                    return;
                }

                rawRootPath.Text = dialog.SelectedPath + @"\";

                if(BasePathEdit.Text == "" && RoverPathEdit.Text == "" && OutputPathEdit.Text == "")
                {
                    string basePath = rawRootPath.Text + @"RawData\BASE\";
                    if (!Directory.Exists(basePath))
                    {
                        MessageBox.Show(this, string.Format("默认基站路径{0} 不存在，请手工输入！", basePath));
                    }
                    BasePathEdit.Text = basePath;

                    string roverPath = rawRootPath.Text + @"RawData\ROVER\";
                    if (!Directory.Exists(basePath))
                    {
                        MessageBox.Show(this, string.Format("默认流动站路径{0} 不存在，请手工输入！", basePath));
                    }
                    RoverPathEdit.Text = roverPath;

                    string outputPath = rawRootPath.Text + @"Output\";
                    OutputPathEdit.Text = outputPath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process cmd = Process.Start("aimapCheck.exe", rawRootPath.Text + " -check_basetime");
            cmd.WaitForExit();

            MessageBox.Show("检查完毕！");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process cmd = Process.Start("aimapCheck.exe", rawRootPath.Text + " -check_track2photo");
            cmd.WaitForExit();

            MessageBox.Show("检查完毕！");
        }
    }
}
