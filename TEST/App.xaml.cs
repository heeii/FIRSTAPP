namespace TEST
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Routing.RegisterRoute(nameof(registrationPage), typeof(registrationPage));
        }
    }
}
