using System.Collections.Generic;

namespace ApiPortalEtico.Application.Common.Models
{
    public class EmailMessage
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public List<EmailRecipient> To { get; set; } = new List<EmailRecipient>();
        public List<EmailRecipient> Cc { get; set; } = new List<EmailRecipient>();
        public List<EmailRecipient> Bcc { get; set; } = new List<EmailRecipient>();
        public List<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
        public bool IsHtml { get; set; } = true;
    }

    public class EmailRecipient
    {
        public string Address { get; set; }
        public string Name { get; set; }

        public EmailRecipient(string address, string name = null)
        {
            Address = address;
            Name = name;
        }
    }

    public class EmailAttachment
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }

        public EmailAttachment(string fileName, byte[] content, string contentType)
        {
            FileName = fileName;
            Content = content;
            ContentType = contentType;
        }
    }
}

