using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RevitConnector
{
    public class Connector
    {
        public static HubConnection _connection;

        public Connector() 
        {
           
        }

        private void Connection_Received(string obj)
        {
            Debug.Write(obj);
        }

        public async void Run()
        {

            for (int i = 5; i > 0; i--)
            {
                Debug.Write(i);
                Thread.Sleep(5000);
            }
            Debug.Write("Запускаем сервер");

            _connection = new HubConnection("https://rbushuev.azurewebsites.net/revit/chat");
            _connection.Received += Connection_Received;
           
            _connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Write($"Не смогли подключиться:{task.Exception.GetBaseException()}");
                }
                else
                {
                    Debug.Write("Connected");

                    var hub = _connection.CreateHubProxy("revit");
                    hub.On<string>("Received", (x) =>
                    {
                        Debug.Write(x);
                    });

                }
            }).Wait();

        }

        public async void Activate()
        {
            Debug.Write("Уже работает");
        }
    }
}
