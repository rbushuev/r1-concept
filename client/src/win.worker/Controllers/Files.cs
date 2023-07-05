using System.Diagnostics;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;

namespace winworker.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController : ControllerBase
{

    private readonly IConfiguration _cfg;
    private readonly ILogger<FilesController> _logger;

    public FilesController(IConfiguration cfg, ILogger<FilesController> logger)
    {
        _cfg = cfg;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        // _logger.LogInformation($"Открываем: {_cfg.GetValue<string>("file")}");
        // var path = $"{_cfg.GetValue<string>("file")}";
        // var process = Process.Start( new ProcessStartInfo {  FileName = path, UseShellExecute = true } );

        _logger.LogInformation($"Запускаем: Revit");

        try
        {
            // new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TestProject.rvt"));
            var process = Process.Start(new ProcessStartInfo { FileName = @"C:\Users\...\Desktop\TestProject.rvt", UseShellExecute = true });
        }
        catch (System.Exception err)
        {
            _logger.LogInformation($"Ошибка запуска: Revit: {err}");
            throw;
        }

        return Ok($"Открываем Revit...");
    }

    // [HttpGet]
    // public IActionResult Get()
    // {
    //     Stopwatch stopWatch = new Stopwatch();
    //     stopWatch.Start();

    //     var ms = new MemoryStream();

    //     using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, true))
    //     {
    //         archive.CreateEntryFromFile(@"C:\Users\...\Desktop\tests\w\wserver\static\data.json", "result.json", CompressionLevel.SmallestSize);
    //     }

    //     stopWatch.Stop();
    //     _logger.LogInformation($"Архивация: {ms.Length / 1024 / 1024}Mb за: {stopWatch.Elapsed.Seconds}sec");

    //     return File(ms.ToArray(), "application/zip", $"test.zip");

    // }
}
