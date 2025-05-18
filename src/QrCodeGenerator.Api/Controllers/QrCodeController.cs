using Microsoft.AspNetCore.Mvc;
using QrCodeGenerator.Core.Generators;

namespace QrCodeGenerator.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class QrCodeController : Controller
{
    private readonly PureGenerator _pureGenerator;

    public QrCodeController(PureGenerator pureGenerator)
    {
        _pureGenerator = pureGenerator;
    }

    [HttpGet]
    public IActionResult Generate(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return BadRequest("Text cannot be null or empty.");
        }
        var qrCodeImage = _pureGenerator.Generate(text);
        return File(qrCodeImage, "image/png");
    }
}
