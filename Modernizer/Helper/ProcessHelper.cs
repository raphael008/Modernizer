using System;
using System.Diagnostics;
using System.IO;
using Modernizer.Model;

namespace Modernizer.Helper;

public static class ProcessHelper
{
    public static void Start(ConsoleApplication app)
    {
        var process = new Process();
        process.EnableRaisingEvents = true;
        process.StartInfo = new ProcessStartInfo
        {
            FileName = app.Path,
            Arguments = app.Args,
            CreateNoWindow = true,
            WorkingDirectory = Path.GetDirectoryName(app.Path),
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        process.OutputDataReceived += (sender, args) => { app.Output += $"{args.Data}{Environment.NewLine}"; };
        process.ErrorDataReceived += (sender, args) => { app.Output += $"{args.Data}{Environment.NewLine}"; };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        ChildProcessTracker.AddProcess(process);

        app.IsRunning = true;
        app.ChildProcess = process;
    }

    public static void Stop(ConsoleApplication app)
    {
        app.ChildProcess.Kill();

        app.IsRunning = false;
    }
}