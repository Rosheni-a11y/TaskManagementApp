namespace PeopleTaskMobile
{
    public partial class InfoPage : ContentPage
    {
        public InfoPage()
        {
            InitializeComponent();
        }

        private async void OnBackToTasksClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}