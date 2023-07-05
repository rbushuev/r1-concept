using Microsoft.AspNetCore.Mvc;
using winworker.Models;

namespace winworker.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{

    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation($"Получили команду");

        return Ok($"Получили команду: {new Random().Next(1, 100)}\n");
    }

    [HttpPost]
    public IActionResult Post(TestVM vm)
    {
        _logger.LogInformation($"Получили команду: {vm.Name} ");

        return Ok($"Получили команду: {new Random().Next(1, 100)}\n");
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
