using alkan_task.ViewModels;

namespace alkan_task
{
    public partial class App : Application
    {
        public static MainViewModel viewModel = new MainViewModel();
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}