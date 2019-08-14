using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutoIECalcGUI
{
    public partial class CheckForm : Form
    {
        public BindingList<CheckElem> list = new BindingList<CheckElem>();
        
        public CheckForm()
        {
            InitializeComponent();

            this.dataGridView1.AutoGenerateColumns = false;

            var columncb = new DataGridViewCheckBoxColumn() { DataPropertyName = "selected", Name = "选择 ", AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader};
            this.dataGridView1.Columns.Add(columncb);

            var columnname = new DataGridViewTextBoxColumn() { DataPropertyName = "name", Name = "检查项名称", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, ReadOnly = true };
            this.dataGridView1.Columns.Add(columnname);

            var columnproc = new DataGridViewTextBoxColumn() { DataPropertyName = "proc", Name = "检查进度", ReadOnly = true };
            this.dataGridView1.Columns.Add(columnproc);

            var columrslt = new DataGridViewLinkColumn() { DataPropertyName = "rslt", Name = "检查结果", ReadOnly = true };
            this.dataGridView1.Columns.Add(columrslt);

            this.dataGridView1.DataSource = list;

            
            //list.Add(new CheckElem("检查数据是否完整", "-check_exposure"));
            list.Add(new CheckElem("检查照片曝光度", "-check_exposure--step:10", "Exposure"));
            list.Add(new CheckElem("检查基站与流动站时间覆盖以及收星", "-check_basetime", "CheckBaseRoverTime"));
            list.Add(new CheckElem("检查轨迹和照片对应关系", "-check_track2photo", "Track2Photo"));

        }

        private void CheckForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;

            CheckElem.actUpdate = ()=> this.dataGridView1.Refresh();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var cb = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                cb.ReadOnly = true;
            }

            btnConfirm.Enabled = false;
            btnCancel.Enabled = false;

            foreach(var elem in list)
            {
                elem.StatusReset();
            }

            var dir = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            Thread t = new Thread(new ThreadStart(() =>
            {
                foreach (var elem in list.Where(x => x.selected))
                {
                    elem.Run(dir);
                }

                Action act = () =>
                {
                    MessageBox.Show(this, "检查完毕！");

                    btnConfirm.Enabled = true;
                    btnCancel.Enabled = true;

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        var cb = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                        cb.ReadOnly = false;
                    }
                };

                Invoke(act);
            }));

            t.Start();
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if(!(cell is DataGridViewLinkCell))
            {
                return;
            }

            var query = list.Single(x => x.name == dataGridView1.Rows[e.RowIndex].Cells[1].Value as string);
            if(query.proc != "检查结束")
            {
                return;
            }
            if(query.rslt == "运行异常")
            {
                if(!File.Exists(query.logpath))
                {
                    MessageBox.Show("日志文件不存在:" + query.logpath);
                    return;
                }

                Process.Start("explorer.exe", query.logpath);
                return;
            }

            if(!Directory.Exists(query.outputDir))
            {
                MessageBox.Show("结果目录不存在:" + query.outputDir);
                return;
            }

            Process.Start("explorer.exe", query.outputDir);
        }

        public class CheckElem
        {
            public bool selected { get; set; }
            public string name { get; set; }
            public static Action actUpdate;

            public string proc
            {
                get
                {
                    switch(status)
                    {
                        case CheckStatus.before:
                            return "等待检查";
                        case CheckStatus.checking:
                            return "正在检查";
                        default:
                            return "检查结束";
                    }
                }
            }

            public string rslt
            {
                get
                {
                    switch(status)
                    {
                        case CheckStatus.success:
                            return "合格";
                        case CheckStatus.failed:
                            return "不合格";
                        case CheckStatus.maual:
                            return "请人工确认";
                        case CheckStatus.exception:
                            return "运行异常";
                        default:
                            return "";
                    }
                }
            }

            public string outputDir
            {
                get
                {
                    return string.Format("{0}check\\{1}\\{2}", Program.Config.RawPath, itemDir, timeDir);
                }
            }

            public string logpath
            {
                get
                {
                    return "log.txt";
                }
            }

            private readonly string cmdParam;

            private readonly string itemDir;
            private string timeDir;
            private CheckStatus status;

            private void UpdateStatus(CheckStatus status)
            {
                this.status = status;
                actUpdate?.Invoke();
            }

            private enum CheckStatus
            {
                before,
                checking,
                success,
                failed,
                maual,
                exception,
            }

            public CheckElem(string name, string cmdParam, string outputDirectory)
            {
                this.selected = true;
                this.name = name;
                this.cmdParam = cmdParam;
                this.status = CheckStatus.before;
                this.itemDir = outputDirectory;
            }

            public void Run(string dir)
            {
                timeDir = dir;

                Process cmd = Process.Start("aimapCheck.exe", Program.Config.RawPath + " " + cmdParam + " -dir--" + dir);
                UpdateStatus(CheckStatus.checking);

                cmd.WaitForExit();

                int value = cmd.ExitCode;
                switch (value)
                {
                    case 1:
                        UpdateStatus(CheckStatus.success);
                        break;
                    case 0:
                        UpdateStatus(CheckStatus.failed);
                        break;
                    case 2:
                        UpdateStatus(CheckStatus.maual);
                        break;
                    default:
                        UpdateStatus(CheckStatus.exception);
                        break;
                }
            }

            public void StatusReset()
            {
                UpdateStatus(CheckStatus.before);
            }
        }
    }
}
