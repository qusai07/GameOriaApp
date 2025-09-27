//IEmailService: لإرسال رسائل البريد الإلكتروني

namespace GameOria.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
        Task SendTemplatedEmailAsync(string templateName, object templateData, EmailMessage emailMessage, CancellationToken cancellationToken = default);
    }

    public class EmailMessage
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public bool IsHtml { get; set; } = true;
        public List<EmailAttachment> Attachments { get; set; } = new();
    }

    public class EmailAttachment
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] Content { get; set; } = Array.Empty<byte>();
        public string ContentType { get; set; } = string.Empty;
    }
}
