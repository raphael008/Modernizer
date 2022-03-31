using System.Windows;

namespace Modernizer;

public partial class ApplicationWindow : Window
{
    public ConsoleApplication ConsoleApplication { get; set; }
    
    public ApplicationWindow()
    {
        InitializeComponent();
    }
}