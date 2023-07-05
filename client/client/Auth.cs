using client.Models;
using Minio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class Auth : INotifyPropertyChanged
    {
        readonly HttpClient _httpClient = new();

        public string Email { get; set; }
        public string Password { get; set; }

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

        public Command SendAuthCommand { get; }

        public Auth()
        {
            SendAuthCommand = new Command(async () => {
                var a = await AuthF();
                Console.WriteLine(a);
            });
        }

        public async Task<string> AuthF()
        {
            //var putObjectArgs = new PutObjectArgs().WithBucket("r1pro").WithObject("каеф.jpg") .WithFileName(_filePath);
            //await _minio.PutObjectAsync(putObjectArgs).ConfigureAwait(false);

            //AuthRequest req = new(Email, Password);
            //using var res = await _httpClient.PostAsync($"{Constants.ServerUrl}/login", JsonContent.Create(req));
            //return await res.Content.ReadFromJsonAsync<AuthResponse>();
            return "asd";
        }

    public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
