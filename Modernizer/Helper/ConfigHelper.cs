using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Modernizer.Model;
using Newtonsoft.Json;

namespace Modernizer.Helper;

public static class ConfigHelper
{
    private static readonly string configPath = "console-apps.json";

    public static List<ConsoleApplication> GetConsoleApplications()
    {
        if (!File.Exists(configPath)) throw new Exception($"config file {configPath} doesn't exist.");

        var apps = JsonConvert.DeserializeObject<List<ConsoleApplication>>(File.ReadAllText(configPath)) ??
                   new List<ConsoleApplication>();
        var idSet = apps
            .Select(application => application.Id)
            .ToHashSet();

        foreach (var app in apps)
            if (app.Id == Guid.Empty)
            {
                var newGuid = Guid.NewGuid();
                while (idSet.Contains(newGuid)) newGuid = Guid.NewGuid();

                idSet.Add(newGuid);
                app.Id = newGuid;
            }

        return apps;
    }
}