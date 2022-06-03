using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Modernizer.Model;

public class ConsoleApplication : INotifyPropertyChanged
{
    [JsonIgnore] private bool isRunning;
    [JsonIgnore] private string output;

    [JsonIgnore] public Guid Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("path")] public string Path { get; set; }

    [JsonProperty("args")] public string Args { get; set; }

    [JsonProperty("autoStart")] public bool AutoStart { get; set; }

    [JsonIgnore] public Process ChildProcess { get; set; }

    public bool IsRunning
    {
        get => isRunning;

        set
        {
            if (value != isRunning)
            {
                isRunning = value;
                NotifyPropertyChanged();
            }
        }
    }

    public string Output
    {
        get => output;

        set
        {
            if (value != output)
            {
                output = value;
                NotifyPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}