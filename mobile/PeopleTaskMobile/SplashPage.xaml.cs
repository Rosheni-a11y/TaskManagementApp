namespace PeopleTaskMobile
{
    public partial class SplashPage : ContentPage
    {
        public SplashPage()
        {
            InitializeComponent();
            StartTimer();
        }

        private async void StartTimer()
        {
            await Task.Delay(2500); // shows for 2.5 seconds
            Application.Current!.Windows[0].Page = new AppShell();
        }
    }
}