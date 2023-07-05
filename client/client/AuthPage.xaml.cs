namespace client;

public partial class AuthPage : ContentPage
{
    Auth viewModel;
    public AuthPage()
    {
        InitializeComponent();
        viewModel = new Auth();
        BindingContext = viewModel;
    }
}