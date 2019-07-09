using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastReport;

namespace SelfServiceBrench
{
    public partial class PreviewForm : Form
    {
        private Report FReport;
        /// <summary>
        /// 打印数据集
        /// </summary>
        public DataSet FDataSet { get; set; }
        /// <summary>
        /// 打印模板文件路径
        /// </summary>
        public string ReportFileName { get; set; }
        /// <summary>
        ///  数据表名称
        /// </summary>
        public string DataTableName { get; set; }

        private bool AutoPrint { get; set; }

        public PreviewForm(DataSet dataSet,string reportFileName,string dataTableName)
        {
            InitializeComponent();
            this.FDataSet = dataSet;
            this.ReportFileName = reportFileName;
            this.DataTableName = dataTableName;

            var autoPrint =Convert.ToBoolean( ConfigurationManager.AppSettings["autoprint"]);
            this.AutoPrint = autoPrint;
        }

        private void PreviewForm_Load(object sender, EventArgs e)
        {
            InitReportPreview();
        }

        private void InitReportPreview()
        {
            FReport = new Report();
            FReport.Preview = this.previewControl1;
            if (LoadReportFile())
            {
                FReport.RegisterData(FDataSet);
                FReport.Prepare();

                if (AutoPrint)
                {
                    FReport.Print();
                }
                else
                {
                    FReport.ShowPrepared();
                }

                
            }
        }

        //加载打印模板文件
        private bool LoadReportFile()
        {
            //模板文件名称
            string reportFileName = ReportFileName;

            StringBuilder builder = new StringBuilder();
            builder.Append(AppDomain.CurrentDomain.BaseDirectory);
            builder.Append(@"Report\");
            builder.Append(reportFileName);

            //报表文件路径
            string reportFilePath = builder.ToString();

            if(!File.Exists(reportFilePath))
            {
                MessageBox.Show(reportFilePath+"打印模板不存在，请检查！");
                return false;
            }

            FReport.Load(reportFilePath);

            return true;
        }
    }
}
