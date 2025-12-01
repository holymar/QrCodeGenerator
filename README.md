# QrCodeGenerator API

REST API for generating QR codes with support for Czech payment QR codes (SPAYD format).

## Features

- Generate general QR codes from any text
- Generate Czech payment QR codes (SPAYD format)
- Support for both IBAN and Czech account number/bank code format
- Cross-platform thanks to ImageSharp (no System.Drawing dependency)

## Technologies

- .NET 10.0
- ASP.NET Core Web API
- [QRCoder-ImageSharp](https://www.nuget.org/packages/QRCoder-ImageSharp/)
- [SixLabors.ImageSharp](https://www.nuget.org/packages/SixLabors.ImageSharp/)
- [IbanNet](https://www.nuget.org/packages/IbanNet/)

## Getting Started

### Run Locally

```bash
dotnet run --project src/QrCodeGenerator.Api
```

API will be available at `https://localhost:7226`

### Docker

```bash
# Build container
dotnet publish src/QrCodeGenerator.Api/QrCodeGenerator.Api.csproj \
    --os linux --arch x64 -c Release -p:PublishProfile=DefaultContainer

# Run container
docker run -p 7226:7226 qrcodegenerator-api:latest
```

API will be available at `http://localhost:7226`

## API Endpoints

### General QR Code

```
GET /QrCode?text={text}
```

**Parameters:**

| Parameter | Type   | Required | Description              |
|-----------|--------|----------|--------------------------|
| text      | string | Yes      | Text to encode into QR   |

**Example:**

```bash
curl "http://localhost:7226/QrCode?text=Hello%20World" --output qr.png
```

### Czech Payment QR Code

```
GET /CzechPayment?amount={amount}&...
```

**Parameters:**

| Parameter      | Type    | Required | Description                    |
|----------------|---------|----------|--------------------------------|
| iban           | string  | No*      | Recipient's IBAN               |
| accountNumber  | string  | No*      | Account number (Czech format)  |
| bankCode       | string  | No*      | Bank code                      |
| amount         | decimal | Yes      | Payment amount                 |
| currency       | string  | No       | Currency (default: CZK)        |
| variableSymbol | int     | No       | Variable symbol                |
| message        | string  | No       | Message for recipient          |

*Either `iban` or combination of `accountNumber` + `bankCode` is required

**Examples:**

With IBAN:
```bash
curl "http://localhost:7226/CzechPayment?iban=CZ6508000000192000145399&amount=1500&variableSymbol=123456" --output payment.png
```

With account number:
```bash
curl "http://localhost:7226/CzechPayment?accountNumber=192000145399&bankCode=0800&amount=1500&message=Invoice%20123" --output payment.png
```

## Swagger UI

Interactive API documentation is available at the root URL after starting the application:

- **Local:** https://localhost:7226
- **Docker:** http://localhost:7226

## Project Structure

```
QrCodeGenerator/
├── src/
│   ├── QrCodeGenerator.Api/          # Web API project
│   │   ├── Controllers/
│   │   │   ├── QrCodeController.cs
│   │   │   └── CzechPaymentController.cs
│   │   └── Program.cs
│   └── QrCodeGenerator.Core/         # Core library
│       └── Generators/
│           ├── PureGenerator.cs
│           └── CzechPaymentGenerator.cs
└── QrCodeGenerator.sln
```

## License

MIT
