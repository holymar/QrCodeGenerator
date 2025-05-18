using IbanNet.Builders;
using IbanNet.Registry;
using System.Text;

namespace QrCodeGenerator.Core.Generators;

public class CzechPaymentGenerator
{
    private readonly PureGenerator _pureGenerator;

    public CzechPaymentGenerator(PureGenerator pureGenerator)
    {
        _pureGenerator = pureGenerator;
    }

    public byte[] Generate(string? iban, string? accountNumber, string? bankCode, decimal amount, string currency, int? variableSymbol, string? message)
    {
        var text = new StringBuilder();
        text.Append("SPD*1.0*ACC:");
        if (string.IsNullOrWhiteSpace(iban))
        {
            if (string.IsNullOrWhiteSpace(accountNumber) || string.IsNullOrWhiteSpace(bankCode))
                throw new ArgumentException("There must be at least IBAN or AccountNumber + BankCode provided!");

            IIbanRegistry registry = IbanRegistry.Default;

            iban = new IbanBuilder()
                .WithCountry("CZ", registry)
                .WithBankIdentifier(bankCode)
                .WithBankAccountNumber(accountNumber)
                .Build();
        }
        text.Append($"{iban}*");
        text.Append($"AM:{amount}*");
        text.Append("PT:IP*");
        if (variableSymbol.HasValue)
            text.Append($"X-VS:{variableSymbol.Value}*");
        if (!string.IsNullOrWhiteSpace(message))
            text.Append($"MSG:{message}*");
        text.Append($"CC:{currency}*");

        return _pureGenerator.Generate(text.ToString());
    }
}