using System;
using System.Configuration;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace SelfServiceBrench
{
    static class Program
    {

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var settings = new CefSettings();

            CefSharpSettings.LegacyJavascriptBindingEnabled = true;

            Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);

            //功能封装类
            ServiceManager service=ServiceManager.GetInstance();

            var broswer = new ChromiumWebBrowser();
            broswer.Dock = DockStyle.Fill;
            broswer.MenuHandler = new MenuHandler();
            //把功能封装类暴露给JS，
            broswer.RegisterJsObject("winForm", service, new CefSharp.BindingOptions() { CamelCaseJavascriptNames = false });

            string url = ConfigurationManager.AppSettings["weburl"];

            broswer.Load(url);

            var form = new MainForm();

            form.Service = service;

            form.Controls.Add(broswer);

            Application.Run(form);
        }
    }
}
