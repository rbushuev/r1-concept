using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace winworker.Controllers;

[ApiController]
[Route("[controller]")]
public class SystemController : ControllerBase
{

    private readonly ILogger<SystemController> _logger;

    public SystemController(ILogger<SystemController> logger)
    {
        _logger = logger;
    }

    [HttpGet()]
    public IActionResult Get()
    {
        if (OperatingSystem.IsWindows())
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.SetValue("r1.worker", "\"" + Assembly.GetExecutingAssembly().Location + "\"");
        }

        return Ok($"Включили автозапуск: {new Random().Next(1, 100)}\n");
    }


    // [HttpGet("/auto/disable")]
    // public IActionResult AutostartDisable()
    // {
    //     if (OperatingSystem.IsWindows())
    //     {
    //         RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
    //         rk.DeleteValue("r1.worker", false);
    //     }
    //     return Ok($"Выключили автозапуск: {new Random().Next(1, 100)}\n");
    // }
}
