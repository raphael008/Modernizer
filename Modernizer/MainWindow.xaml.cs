using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Modernizer.Helper;
using Modernizer.Model;

namespace Modernizer;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        InitializeBindings();

        var apps = ConfigHelper.GetConsoleApplications();
        apps.ForEach(Apps.Add);

        InitializeAutoStartApps();

        Console.WriteLine();
    }

    public ObservableCollection<ConsoleApplication> Apps { get; set; }

    private void InitializeBindings()
    {
        Apps = new ObservableCollection<ConsoleApplication>();

        MyListView.ItemsSource = Apps;
    }

    private void InitializeAutoStartApps()
    {
        var worker = new BackgroundWorker();
        worker.DoWork += (sender, args) =>
        {
            Apps
                .Where(application => application.AutoStart)
                .Where(application => !application.IsRunning)
                .ToList()
                .ForEach(ProcessHelper.Start);
        };
        worker.RunWorkerAsync();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var application = new ConsoleApplication();
        var random = new Random();
        application.Name = $"App - {random.Next()}";
        application.Path = $"Path - {random.Next()}";
        Apps.Add(application);
    }

    private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
    {
        foreach (var app in Apps) app.ChildProcess.Kill();
    }

    private void StartButton_OnClick(object sender, RoutedEventArgs e)
    {
        var app = ((Button) sender).DataContext as ConsoleApplication;

        if (app == null) return;

        ProcessHelper.Start(app);
    }

    private void StopButton_OnClick(object sender, RoutedEventArgs e)
    {
        var app = ((Button) sender).DataContext as ConsoleApplication;

        if (app == null) return;

        ProcessHelper.Stop(app);
    }

    private void RestartButton_OnClick(object sender, RoutedEventArgs e)
    {
        var app = ((Button) sender).DataContext as ConsoleApplication;

        if (app == null) return;

        ProcessHelper.Stop(app);
        ProcessHelper.Start(app);
    }

    private void FolderButton_OnClick(object sender, RoutedEventArgs e)
    {
        var app = ((Button) sender).DataContext as ConsoleApplication;

        if (app == null) return;

        Process.Start("explorer.exe", Path.GetDirectoryName(app.Path) ?? string.Empty);
    }

    private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
    {
        RegistryHelper.EnableLaunchAtStartUp();
    }

    private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
    {
        RegistryHelper.DisableLaunchAtStartUp();
    }
}