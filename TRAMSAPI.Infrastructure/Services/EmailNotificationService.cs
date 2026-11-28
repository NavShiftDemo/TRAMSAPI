using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Infrastructure.Services;

public class EmailNotificationService : IOtpNotificationService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailNotificationService> _logger;

    public EmailNotificationService(
        IConfiguration configuration,
        ILogger<EmailNotificationService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<bool> SendOtpAsync(string destination, string otpCode, Purpose purpose)
    {
        var host = _configuration["SmtpSettings:Host"] ?? "smtp.gmail.com";
        var port = int.TryParse(_configuration["SmtpSettings:Port"], out var p) ? p : 587;
        var senderEmail = _configuration["SmtpSettings:SenderEmail"] ?? string.Empty;
        var senderPassword = _configuration["SmtpSettings:SenderPassword"] ?? string.Empty;
        var senderName = _configuration["SmtpSettings:SenderName"] ?? "TRAMS Support";

        // Remove any spaces and ensure lowercase for Google's app password
        senderPassword = senderPassword.Replace(" ", "").Trim().ToLowerInvariant();

        // Fallback: If credentials are not configured yet, log to console
        if (string.IsNullOrWhiteSpace(senderEmail) || string.IsNullOrWhiteSpace(senderPassword))
        {
            _logger.LogWarning(">>> [SMTP NOT CONFIGURED] Destination: {Destination} | OTP: {Code}. Please configure SmtpSettings in appsettings.json.",
                destination, otpCode);
            return true;
        }

        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(senderName, senderEmail));
            message.To.Add(new MailboxAddress(destination, destination));

            message.Subject = purpose switch
            {
                Purpose.RESET_PASSWORD => "TRAMS - Password Reset Verification Code",
                Purpose.VERIFY_EMAIL => "TRAMS - Email Verification Code",
                _ => "TRAMS - Verification Code"
            };

            var htmlBody = $@"
<!DOCTYPE html>
<html>
<body style='font-family: Arial, sans-serif; background-color: #f4f6f8; padding: 20px;'>
  <div style='max-width: 500px; margin: auto; background: #ffffff; padding: 30px; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);'>
    <h2 style='color: #2c3e50; text-align: center; margin-top: 0;'>TRAMS Security Verification</h2>
    <p style='color: #555; font-size: 15px;'>Hello,</p>
    <p style='color: #555; font-size: 15px;'>Your one-time verification code (OTP) for password reset is:</p>
    <div style='text-align: center; margin: 25px 0;'>
      <span style='font-size: 32px; font-weight: bold; letter-spacing: 6px; color: #1a73e8; background: #e8f0fe; padding: 12px 24px; border-radius: 6px; display: inline-block;'>{otpCode}</span>
    </div>
    <p style='color: #777; font-size: 13px;'>This code is valid for <strong>5 minutes</strong>. Do not share this code with anyone.</p>
    <hr style='border: none; border-top: 1px solid #eee; margin: 20px 0;' />
    <p style='color: #999; font-size: 12px; text-align: center;'>If you did not request this, please ignore this email or contact your system administrator.</p>
  </div>
</body>
</html>";

            message.Body = new TextPart(TextFormat.Html) { Text = htmlBody };

            using var client = new SmtpClient();

            // Gmail: STARTTLS on 587, or SSL on 465
            var socketOption = port == 465
                ? SecureSocketOptions.SslOnConnect
                : SecureSocketOptions.StartTls;

            await client.ConnectAsync(host, port, socketOption);
            await client.AuthenticateAsync(senderEmail, senderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation(">>> [EMAIL SENT] OTP successfully delivered to {Destination}", destination);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ">>> [EMAIL ERROR] Failed to send OTP email to {Destination}: {Message}", destination, ex.Message);
            return false;
        }
    }
}
