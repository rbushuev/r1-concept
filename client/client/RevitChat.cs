using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class RevitChat : INotifyPropertyChanged
    {
        readonly HubConnection hubConnection;

        public string Command { get; set; }

        public ObservableCollection<MessageData> Messages { get; } = new();

        bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }
        
        bool isConnected;
        public bool IsConnected
        {
            get => isConnected;
            set
            {
                if (isConnected != value)
                {
                    isConnected = value;
                    OnPropertyChanged("IsConnected");
                }
            }
        }

        public Command SendMessageCommand { get; }

        public RevitChat()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl($"{Constants.ServerUrl}/revit/chat")
                .Build();

            IsConnected = false;
            IsBusy = false;

            SendMessageCommand = new Command(async () => await SendMessage(), () => IsConnected);

            hubConnection.Closed += async (error) =>
            {
                SendLocalMessage("connect closed...");
                IsConnected = false;
                await Task.Delay(5000);
                await Connect();
            };

            hubConnection.On<string>("Received", async (command) =>
            {
                if (command == "revit")
                {
                    try
                    {
                        string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TestProject.rvt");

                        SendLocalMessage($"скачиваем файл проекта...");
                        await HttpHelper.DownloadFileAsync($"{Constants.ServerUrl}/revit/files/TestProject.rvt", filepath);
                        SendLocalMessage($"сохранили на рабочем столе...");
                        SendLocalMessage($"открываем Revit...");
                        var process = Process.Start(new ProcessStartInfo{FileName = filepath, UseShellExecute = true});
                    }
                    catch (System.Exception ex)
                    {
                        SendLocalMessage($"нужен файлик на рабочем столе... {ex.Message}");
                        return;
                    }
                }
                SendLocalMessage(command);
            });
        }

        // подключение к чату
        public async Task Connect()
        {
            if (IsConnected)
                return;
            try
            {
                await hubConnection.StartAsync();
                SendLocalMessage("welcome...");
                IsConnected = true;
            }
            catch (Exception ex)
            {
                SendLocalMessage($"connected error: {ex.Message}");
            }
        }

        // Отключение от чата
        public async Task Disconnect()
        {
            if (!IsConnected) return;

            await hubConnection.StopAsync();
            IsConnected = false;
            SendLocalMessage("leave...");
        }

        // Отправка сообщения
        async Task SendMessage()
        {
            try
            {
                IsBusy = true;
                await hubConnection.InvokeAsync("Send", Command);
            }
            catch (Exception ex)
            {
                SendLocalMessage($"send error: {ex.Message}");
            }

            IsBusy = false;
        }

        // Добавление сообщения
        private void SendLocalMessage(string command)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Messages.Insert(0, new MessageData(command)); 
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }

    public record MessageData(string Command);
}
