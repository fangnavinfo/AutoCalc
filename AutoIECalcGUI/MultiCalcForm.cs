using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;
using AutoIECalcPublic;
using System.Diagnostics;
using System.Threading;
using LitJson;

namespace AutoIECalcGUI
{
    public partial class MultiCalcForm : Form
    {
        public MultiCalcForm()
        {
            InitializeComponent();
            //MultiCalcElem.actUpdate = () => this.dataGridView1.Refresh();

            IEPathEdit.TextChanged += (sender, e) =>
            {
                if(IEPathEdit.TextLength != 0)
                {
                    btnSelectRootPath.Enabled = true;
                }
                else
                {
                    btnSelectRootPath.Enabled = false;
                }
            };

            this.dataGridView1.RowsAdded += (sender, e) =>
            {
                btnStartCalc.Enabled = true;
            };
        }

        private void btnStartCalc_Click(object sender, EventArgs e)
        {
            btnSelectRootPath.Enabled = false;
            IEPathBtn.Enabled = false;
            btnStartCalc.Enabled = false;

            startTime = DateTime.Now;

            var logPath = string.Format("{0}/{1}批量解算.log", textBoxPrjPath.Text, startTime.ToString("yyyyMMddHHmmss"));
            File.WriteAllText(logPath, "序号, 工程名, 基站名, 杆臂值, 基站解算坐标, 开始时间, 结束时间, 是否解算成功, 失败原因\n");

            Thread t = new Thread(new ThreadStart(() =>
           {
               Action act = () => this.dataGridView1.Refresh();

               foreach (var item in list)
               {
                   if(item.status == MultiCalcElem.Status.DataError)
                   {
                       item.startTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                       item.endTime = item.startTime;
                       exportLog(logPath, item);
                       continue;
                   }

                   item.Save(ConfigSetting.BaseConfigPath);
                   item.status = MultiCalcElem.Status.CalcBase;

                   item.startTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                   var processBase = Process.Start("AutoIECalcCmd.exe", "BASE ");
                   processBase.WaitForExit();

                   int value = processBase.ExitCode;
                   if (value != 2)
                   {
                       item.setRslt(value);
                       item.endTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                       exportLog(logPath, item);

                       act();

                       continue;
                   }

                   item.status = MultiCalcElem.Status.CalcPrj;

                   item.ReadBaseStationCoord();
                   item.Save(ConfigSetting.WeiyaConfigPath);

                   File.Delete(item.GetProjectCfgPath());
                   File.Delete(item.GetPostprocessPath());

                   var processPrj = Process.Start("AutoIECalcCmd.exe", "WEIYA ");
                   processPrj.WaitForExit();

                   item.endTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");

                   value = processPrj.ExitCode;
                   item.setRslt(value);
                   act();

                   exportLog(logPath, item);
               }

               btnSelectRootPath.Enabled = true;
               IEPathBtn.Enabled = true;
               btnStartCalc.Enabled = true;

               
           }));

            t.Start();

            //this.dataGridView1.Update();
        }

        private void exportLog(string logPath, MultiCalcElem elem)
        {

            var data = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8} \n",
                                                                       elem.index,
                                                                       elem.name,
                                                                       elem.GetRawBaseStationFileName(), 
                                                                       string.Format("({0} {1} {2})", elem.LeverArmOffsetX, elem.LeverArmOffsetY, elem.LeverArmOffsetZ),
                                                                       string.Format("({0}:{1}:{2} {3}:{4}:{5} {6})", elem.Lon[0], elem.Lon[1], elem.Lon[2], elem.Lat[0], elem.Lat[1], elem.Lat[2], elem.BasetStationHeight),
                                                                       elem.startTime,
                                                                       elem.endTime,
                                                                       elem.status == MultiCalcElem.Status.CalcSuccess? "是":"否",
                                                                       elem.errorInfo).Replace(" (  )", "").Replace(" (:: :: )", "");
            File.AppendAllText(logPath, data);
        }

        private void btnSelectRootPath_Click(object sender, EventArgs e)
        {
            this.dataGridView1.ReadOnly = true;
        
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxPrjPath.Text = "";
                list.Clear();

                if (!Directory.Exists(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不存在", "提示");
                    return;
                }

                var prjPaths = Directory.EnumerateDirectories(dialog.SelectedPath, "@@*");
                if(!prjPaths.Any())
                {
                    MessageBox.Show(this, "文件夹路径下不存在工程目录", "提示");
                    return;
                }

                var time = DateTime.Now.ToString();

                textBoxPrjPath.Text = dialog.SelectedPath;
                foreach (var elem in prjPaths)
                {
                    AddProject(elem, IEPathEdit.Text, time);    
                }
            }

            //this.dataGridView1.DataSource = dt;
        }

        private void AddProject(string prjname, string iePath, string date)
        {
            var rawRootPath = prjname + @"\";

            MultiCalcElem elem = new MultiCalcElem();
            elem.startTime = date;
            elem.prjname = prjname.Substring(prjname.IndexOf("@@"));
            elem.IEPath = iePath;
            
            elem.outputName = elem.prjname.Replace("@@", "");

            try
            {
                elem.Init(rawRootPath);
                elem.status = MultiCalcElem.Status.beforeCalc;
            }
            catch(Exception e)
            {
                elem.status = MultiCalcElem.Status.DataError;
                elem.error = e.Message;
                MessageBox.Show(this, string.Format("{0}, {1}", elem.name, e.Message));
            }

            elem.index = list.Count();
            list.Add(elem);
            
        }

        private void IEPathBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!Directory.Exists(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不存在，" + dialog.SelectedPath, "提示");
                    return;
                }

                var filePath = dialog.SelectedPath + @"\wGpsIns.exe";
                if(!File.Exists(filePath))
                {
                    MessageBox.Show(this, "IE程序不存在，" + filePath, "提示");
                    return;
                }

                IEPathEdit.Text = dialog.SelectedPath + @"\";

                Program.Config.IEPath = IEPathEdit.Text;
                Program.Config.Save();
            }
        }

        private void MultiCalcForm_Load(object sender, EventArgs e)
        {
            Program.Config = ConfigSetting.Load(ConfigSetting.WeiyaConfigPath);
            this.IEPathEdit.Text = Program.Config.IEPath;
            
            //dt = new DataTable();
            //dt.Columns.Add(new DataColumn("工程名称", typeof(string)));
            //dt.Columns.Add(new DataColumn("当前状态", typeof(string)));

            this.dataGridView1.DataSource = list;
            this.dataGridView1.Columns[0].HeaderText = "序号";
            this.dataGridView1.Columns[1].HeaderText = "工程名称";
            this.dataGridView1.Columns[2].HeaderText = "当前状态";
            this.dataGridView1.Columns[3].HeaderText = "错误信息";
        }

        BindingList<MultiCalcElem> list = new BindingList<MultiCalcElem>();
        //DataTable dt;
    }

    class MultiCalcElem : ConfigSetting
    {
        //public static Action actUpdate;

        public int index { get; set; }

        public string name
        {
            get
            {
                return prjname;
            }
        }

        public string status_show
        {
            get
            {
                switch(status)
                {
                    case Status.beforeCalc:
                        return "等待解算";
                    case Status.DataError:
                        return "数据错误";
                    case Status.CalcBase:
                        return "解算基站坐标";
                    case Status.CalcPrj:
                        return "解算工程文件";
                    case Status.CalcBaseFail:
                        return "解算基站坐标失败";
                    case Status.CalcPrjFail:
                        return "解算工程文件失败";
                    case Status.CalcSuccess:
                        return "解算成功";
                    default:
                        throw new Exception("not support status");
                }
            }
        }

        public string errorInfo
        {
            get
            {
                return error.Split('\n')[0];
            }
        }

        public enum Status
        {
            beforeCalc,
            DataError,
            
            CalcBase,
            CalcPrj,

            CalcBaseFail,
            CalcPrjFail,

            CalcSuccess,
        }

        public Status status;
        public string prjname;
        public string error = "";
        
        internal void ReadBaseStationCoord()
        {
            if (File.Exists(GetCalcBaseOutputPath()))
            {
                List<string> allLines = System.IO.File.ReadAllLines(GetCalcBaseOutputPath()).ToList();
                allLines.RemoveAll(x => x.Length == 0);

                string lastestLine = allLines.Last();
                List<string> elems = lastestLine.Split(" ".ToCharArray()).ToList();
                elems.RemoveAll(x => x.Length == 0);

                Lat[0] = elems[0];
                Lat[1] = elems[1];
                Lat[2] = elems[2];
                Lon[0] = elems[3];
                Lon[1] = elems[4];
                Lon[2] = elems[5];

                BasetStationHeight = elems[6];
                AntennaMeasureHeight = "0.0";
            }
        }

        internal void setRslt(int value)
        {
            if (value == 2)
            {
                status = MultiCalcElem.Status.CalcSuccess;
            }
            else if (value == 1)
            {
                status = MultiCalcElem.Status.CalcPrjFail;
                error = File.ReadAllText(ConfigSetting.errfile);
            }
            else
            {
                status = MultiCalcElem.Status.CalcPrjFail;
                error = "AutoIECalcCmd not Finish!";
            }

            //actUpdate();
        }

        //public string basePath;
        //public string roverPath;
        //public string outputPath;
        //public string error;

        //public (double x, double y, double z) LeverArmOffset;
    }
}
