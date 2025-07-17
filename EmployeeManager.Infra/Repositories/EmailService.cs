using System.Net.Mail;
using EmployeeManager.Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace EmployeeManager.Infra.Repositories;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var emailMessage = new MimeMessage();
        
        emailMessage.From.Add(MailboxAddress.Parse(_configuration["EMAIL_ADDRESS"]));
        
        emailMessage.To.Add(MailboxAddress.Parse(email));
        
        emailMessage.Subject = subject;
        
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

    //     using var smtp = new SmtpClient();
    //
    //     await smtp.ConnectAsync(
    //         _configuration["EmailSettings:smtpServer"],
    //         int.Parse(_configuration["EmailSettings:smtpPort"]),
    //         useSsl: true
    //     );
    //     
    //     await smtp.SendAsync(emailMessage);
    //
    //     await smtp.DisconnectAsync(true);
     }
}