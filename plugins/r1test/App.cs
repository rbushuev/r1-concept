using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace r1test
{
    public class App : IExternalApplication
    {
        public static RevitConnector.Connector connector = new RevitConnector.Connector();

        public Result OnStartup(UIControlledApplication application)
        {
            Debug.Write($"Старт");

            Task.Run(() =>
            {
                Debug.Write("Запускаем Коннектор");
                connector.Run();
            });

            //application.ControlledApplication.ApplicationInitialized += AIP;


            TaskDialog.Show("R1 Module", $"R1Test Plugin v1.01") ;

            return Result.Succeeded;
        }

        void AIP(object sender, ApplicationInitializedEventArgs e)
        {

            Debug.Write($"Event Started...");

            var app = sender as Application;

            var uiapp = new UIApplication(app);

            var openOptions = new OpenOptions();
            openOptions.SetOpenWorksetsConfiguration(new WorksetConfiguration(WorksetConfigurationOption.CloseAllWorksets));

            ModelPath modelPathout = ModelPathUtils.ConvertUserVisiblePathToModelPath(@"D:\revit2\var 1\0000 Практическое задание.rvt");

            uiapp.OpenAndActivateDocument(modelPathout, openOptions, false);
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }
}
