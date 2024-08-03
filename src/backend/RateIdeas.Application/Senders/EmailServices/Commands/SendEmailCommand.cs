namespace RateIdeas.Application.Senders.EmailServices.Commands;

public record SendEmailCommand : IRequest<bool>
{
    public SendEmailCommand(SendEmailCommand command)
    {
        To = command.To;
        Subject = command.Subject;
        Body = command.Body;
    }
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}

public class SendEmailCommandHandler(IOptions<EmailConfigurations> options) : IRequestHandler<SendEmailCommand, bool>
{
    private readonly EmailConfigurations configs = options.Value;

    public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.To) || !IsValidEmail(request.To))
            throw new NotFoundException($"Invalid email");

        SmtpClient smtpClient = new(configs.Host)
        {
            Port = configs.Port,
            Credentials = new NetworkCredential(configs.Email, configs.Password),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(options.Value.Email, options.Value.DisplayName),
            Subject = request.Subject,
            Body = request.Body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(request.To);
        await smtpClient.SendMailAsync(mailMessage, cancellationToken);

        return true;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return mailAddress.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
