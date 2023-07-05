using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace winworker.Workers;


public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly string registry_ket_unistall = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Stopwatch stopWatch = new();
        stopWatch.Start();

        while (!stoppingToken.IsCancellationRequested)
        {

            // var apps = "";

            // if (OperatingSystem.IsWindows())
            //     using (var key = Registry.LocalMachine.OpenSubKey(registry_ket_unistall))
            //         if (key != null)
            //             foreach (string subkey_name in key.GetSubKeyNames())
            //                 using (var subkey = key.OpenSubKey(subkey_name))
            //                     if (subkey != null)
            //                         if (subkey.GetValue("DisplayName") != null)
            //                             if (subkey.GetValue("DisplayName").ToString().Contains("Revit"))
            //                                 apps += $"{subkey.GetValue("DisplayName")}\n";

            // Process.Start("revit.exe", ".");

            // using (var writer = File.AppendText(@"C:\Users\...\AppData\Roaming\test.txt"))
            //     await writer.WriteLineAsync($"{apps}");

            using (var writer = File.AppendText(@"C:\Users\...\AppData\Roaming\test.txt"))
                await writer.WriteLineAsync($"Сейчас: {DateTimeOffset.Now}");

            _logger.LogInformation($"Дебаг служба работает: {stopWatch.Elapsed.ToString()}");

            await Task.Delay(5000, stoppingToken);
        }
    }
}