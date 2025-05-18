using Microsoft.AspNetCore.Mvc;
using QrCodeGenerator.Core.Generators;

namespace QrCodeGenerator.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class CzechPaymentController : ControllerBase
{
    private readonly CzechPaymentGenerator _czechPaymentGenerator;
    public CzechPaymentController(CzechPaymentGenerator czechPaymentGenerator)
    {
        _czechPaymentGenerator = czechPaymentGenerator;
    }

    [HttpGet]
    public IActionResult GenerateCzechPayment(string? iban, string? accountNumber, string? bankCode, decimal amount, int? variableSymbol, string? message, string currency = "CZK")
    {
        try
        {
            var qrCodeImage = _czechPaymentGenerator.Generate(iban, accountNumber, bankCode, amount, currency, variableSymbol, message);
            return File(qrCodeImage, "image/png");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
