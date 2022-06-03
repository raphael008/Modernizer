using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;

namespace Modernizer.Helper;

public class RegistryHelper
{
    public static void EnableLaunchAtStartUp()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        registryKey?.SetValue("Modernizer", Process.GetCurrentProcess().MainModule.FileName);
    }

    public static void DisableLaunchAtStartUp()
    {
        var registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        registryKey?.DeleteValue("Modernizer", false);
    }
}