using System;
using System.Collections.Generic;

namespace Individual_project.Models
{
  public sealed class EmailMessage
  {
    public EmailMessage(string messageId)
    {
      MessageId = messageId;
      CreatedAt = DateTime.Now;
      Recipients = new List<EmailAddress>();
      Subject = string.Empty;
      Body = string.Empty;
    }

    public string MessageId { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<EmailAddress> Recipients { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }
  }
}