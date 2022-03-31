using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Modernizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ConsoleApplication> Applications { get; set; }
        
        public MainWindow()
        {
            Applications = new ObservableCollection<ConsoleApplication>();
            InitializeComponent();
            InitializeApplications();
        }
        
        private void InitializeApplications()
        {
            // Applications = new ObservableCollection<ConsoleApplication>();
            for (int i = 0; i < 3; i++)
            {
                var application = new ConsoleApplication();
                application.Name = $"App - {i}";
                application.Path = $"Path - {i}";
                Applications.Add(application);
            }

            // MyListView.ItemsSource = Applications;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var application = new ConsoleApplication();
            var random = new Random();
            application.Name = $"App - {random.Next()}";
            application.Path = $"Path - {random.Next()}";
            Applications.Add(application);
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = MyListView.SelectedItem as ConsoleApplication;
            
            var applicationWindow = new ApplicationWindow();
            applicationWindow.ConsoleApplication = selectedItem;
            applicationWindow.Show();
        }
    }
}