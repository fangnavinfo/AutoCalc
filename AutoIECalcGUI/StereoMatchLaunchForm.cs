using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Xml.Linq;
using LitJson;

namespace AutoIECalcGUI
{
    public partial class StereoMatchLaunchForm : Form
    {
        private string StereoMatchPath;

        public StereoMatchLaunchForm()
        {
            InitializeComponent();
        }

        private void IEPathEdit_TextChanged(object sender, EventArgs e)
        {

        }

        private void exePathBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                if(!File.Exists(dialog.SelectedPath + @"\StereoMatch.exe"))
                {
                    MessageBox.Show(this, "对应文件夹下不存在StereoMatch.exe", "提示");
                    return;
                }

                exePathEdit.Text = dialog.SelectedPath + @"\";
            }
        }

        private void rootPathBtn_Click(object sender, EventArgs e)
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

                if (rootPathEdit.Text == dialog.SelectedPath + @"\")
                {
                    return;
                }

                rootPathEdit.Text = dialog.SelectedPath + @"\";

                
            }
        }

        private void ccdCSVBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "*.csv";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ccdCSVPathEdit.Text = dialog.FileName;
            }
        }

        private void inPathBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "*.xml";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                inPathEdit.Text = dialog.FileName;
            }
        }

        private void exPathBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "*.xml";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                exPathEdit.Text = dialog.FileName;
            }
        }

        private void outputPathBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                outputPathEdit.Text = dialog.SelectedPath;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GreateXmlFile();
            GreateJsonFile();

            Process p = new Process();// Process.Start(appPath);
            p.StartInfo = new ProcessStartInfo();
            p.StartInfo.WorkingDirectory = exePathEdit.Text;
            p.StartInfo.FileName = exePathEdit.Text + "/StereoMatch.exe";
            p.StartInfo.Arguments = "config.xml";
            p.Start();
        }

        private void GreateXmlFile()
        {
            XElement xmlTree1 = new XElement("opencv_storage",
                                                new XElement("StereoRectifyCSV", ccdCSVPathEdit.Text),
                                                new XElement("IntrinsicXml",     inPathEdit.Text),
                                                new XElement("ExtrinsicsXML",    exPathEdit.Text),
                                                new XElement("OutputsStereoFolder", outputPathEdit.Text),
                                                new XElement("isOptimizeStereoParam", 1),
                                                new XElement("numofMatchingPoint", 20000),
                                                new XElement("isAutoStartProcess", 0),
                                                new XElement("compression_value", 80)
                                            );

            XmlTextWriter writer = new XmlTextWriter(exePathEdit.Text + @"/config.xml", Encoding.ASCII);
            XDocument xdoc = new XDocument(xmlTree1);
            xdoc.Save(writer);
            writer.Close();
        }

        private void GreateJsonFile()
        {
            JsonData jsonData = new JsonData();
            jsonData["exepath"] = exePathEdit.Text;
            jsonData["prjpath"] = rootPathEdit.Text;

            StreamWriter sw = new StreamWriter(StereoMatchPath);   //利用写入流创建文件
            sw.Write(jsonData.ToJson());
            sw.Close();
        }

        private void rootPathEdit_TextChanged(object sender, EventArgs e)
        {
            string ccdCSVPath = rootPathEdit.Text + @"RawData\CCD\";
            if (!Directory.Exists(ccdCSVPath))
            {
                MessageBox.Show(this, string.Format("默认立体像对文件所在目录{0}不存在，请手工输入路径！", ccdCSVPath));
                return;
            }
            else
            {
                ccdCSVPath = Directory.EnumerateFiles(ccdCSVPath, "*.csv").FirstOrDefault();
                if (ccdCSVPath == null)
                {
                    MessageBox.Show(this, string.Format("默认立体像对文件{0}不存在，请手工输入路径！", ccdCSVPath));
                    return;
                }
            }

            ccdCSVPathEdit.Text = ccdCSVPath;

            string outputPath = rootPathEdit.Text + @"Output";
            if (!Directory.Exists(outputPath))
            {
                MessageBox.Show(this, string.Format("默认数输出路径{0}不存在，请手工输入路径！", outputPath));
                return;
            }
            outputPathEdit.Text = outputPath;

            string inPath = rootPathEdit.Text + @"Output\intrinsics.xml";
            if (!File.Exists(inPath))
            {
                MessageBox.Show(this, string.Format("默认相机畸变参数文件{0}不存在，请手工输入路径！", inPath));
                return;
            }
            inPathEdit.Text = inPath;

            string exPath = rootPathEdit.Text + @"Output\extrinsics.xml";
            if (!File.Exists(inPath))
            {
                MessageBox.Show(this, string.Format("默认立体标定参数文件{0}不存在，请手工输入路径！", exPath));
                return;
            }
            exPathEdit.Text = exPath;
        }

        private void StereoMatchLaunchForm_Load(object sender, EventArgs e)
        {
            StereoMatchPath = System.AppDomain.CurrentDomain.BaseDirectory + @"\StereoMatchPath.txt";
            if (!File.Exists(StereoMatchPath))
            {
                return;
            }

            var jsonData = JsonMapper.ToObject(File.ReadAllText(StereoMatchPath));
            if (File.Exists(jsonData["exepath"].ToString() + "StereoMatch.exe"))
            {
                exePathEdit.Text = jsonData["exepath"].ToString();
            }

            if (Directory.Exists(jsonData["prjpath"].ToString()))
            {
                rootPathEdit.Text = jsonData["prjpath"].ToString();
            }
        }
    }
}
