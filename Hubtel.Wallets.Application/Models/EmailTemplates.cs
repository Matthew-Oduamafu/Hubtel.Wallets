namespace Hubtel.Wallets.Application.Models
{
    public static class EmailTemplates
    {
        public const string EmailSubjectForWallet = "Wallet Created Successfully";

        public const string HtmlEmailTemplate = @"<!DOCTYPE html>
<html lang=""en"">
<head>
  <meta charset=""UTF-8"">
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
  <title>Welcome to Hubtel Online Payment System</title>
  <style>
    /* Add your custom CSS styles here */
    body {
      font-family: Arial, sans-serif;
      background-color: #f5f5f5;
      padding: 20px;
    }

    .container {
      max-width: 600px;
      margin: 0 auto;
      background-color: #ffffff;
      padding: 20px;
      border-radius: 5px;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    h1 {
      color: #333333;
      font-size: 24px;
      margin-bottom: 20px;
    }

    p {
      color: #555555;
      font-size: 16px;
      line-height: 1.5;
      margin-bottom: 10px;
    }

    a {
      color: #007bff;
      text-decoration: none;
    }

    .btn {
      display: inline-block;
      background-color: #007bff;
      color: #ffffff;
      padding: 10px 20px;
      border-radius: 4px;
      text-decoration: none;
    }

    .logo {
      display: inline-block;
      height: 50px;
      width: auto;
      margin-bottom: 20px;
    }

    .banner {
      max-width: 100%;
      height: auto;
      margin-bottom: 20px;
    }
  </style>
</head>
<body>
  <div class=""container"">
    <img class=""logo"" src=""https://p.kindpng.com/picc/s/25-253110_tigo-logo-png-png-download-hubtel-ghana-logo.png"" alt=""Hubtel Logo"">
    <h1>Welcome to Hubtel Online Payment System</h1>
    <img class=""banner"" src=""https://educationghana.org/wp-content/uploads/2020/05/Screenshot_2020-05-12-Hubtel-Limited-Google-Search1.png"" alt=""Payment System Banner"">
    <p>
      Hello,
      <br>
      You're welcome to Hubtel's Online Payment System. Here, you'll find everything you need and can make payments with ease.
    </p>
    <p>
      We hope you'll find our services more satisfying and convenient for all your payment needs.
    </p>
    <p>
      If you have any questions or need assistance, please feel free to <a href=""mailto:support@hubtel.com"">contact us</a>.
    </p>
    <p>
      Thank you for choosing Hubtel!
    </p>
    <p>
      Best regards,
      <br>
      The Hubtel Team
    </p>
    <p>
      <a class=""btn"" href=""https://hubtel.com/get-the-app/"">Visit Our Website</a>
    </p>
  </div>
</body>
</html>
";
    }
}