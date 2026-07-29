namespace PeopleTaskMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAboutClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoPage());
        }

            protected override bool OnBackButtonPressed()
        {
            if (webView.CanGoBack)
            {
                webView.GoBack();
                return true; // we handled it, don't close the app
            }

            return base.OnBackButtonPressed(); // let default behavior happen (e.g. exit app)
        }
    }

}
