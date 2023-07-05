namespace client
{
    public partial class MainPage : ContentPage
    {
        RevitChat viewModel;
        public MainPage()
        {
            InitializeComponent();
            viewModel = new RevitChat();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.Connect();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await viewModel.Disconnect();
        }
    }
}