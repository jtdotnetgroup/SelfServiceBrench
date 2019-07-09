using System;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrintLib;
using ServicesLib;

namespace SelfServiceBrench
{
    /// <summary>
    /// 功能封装类，整个程序所有功能都封装在此类
    /// 此类设计为单例模式
    /// </summary>
    public class ServiceManager:IServiceManager
    {

        public string User { get; set; }

        /// <summary>
        /// 私有构造方法，类外不允许实例化，通过GetInstance方法获取实例
        /// </summary>
        private ServiceManager()
        {

        }

        static ServiceManager service=new ServiceManager();

        public static ServiceManager GetInstance()
        {
            return service;
        }

        //退出程序
        public void ExitApp()
        {
            Application.Exit();
        }

        
        public void  ShowMessage(string msg)
        {
             MessageBox.Show(msg);
        }

        //打印预览
        public void Preview(string printData)
        {
            //多线程调用显示预览窗口
            Thread initPreviewFormThread = new Thread(new ParameterizedThreadStart(InitPreviewForm));
            initPreviewFormThread.SetApartmentState(ApartmentState.STA);
            initPreviewFormThread.Start(printData);

        }

        //初始化打印预览窗，
        private void InitPreviewForm(object json)
        {

            var jsonOjb= JSONHelper.JsonToObject<PrintLableEntity>(json.ToString());

            jsonOjb.Qty = 100;

            var table = jsonOjb.ToDataTable();

            DataSet dataSet=new DataSet();
            dataSet.Tables.Add(table);

            var reportFileName = ConfigurationManager.AppSettings["reportFileName"];
            PreviewForm form = new PreviewForm(dataSet,reportFileName,table.TableName );
            //调用ShowDialog方法显示打印预览窗口，如果用Show会一闪而过
            form.ShowDialog();
        }


    }

    public class Tester
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}