using System.Collections.Generic;
using Individual_project.Models;

namespace Individual_project.Logic
{
  public sealed class EmailGenerator
  {
    private readonly TemplateRegistry templateRegistry;

    public EmailGenerator(TemplateRegistry templateRegistry)
    {
      this.templateRegistry = templateRegistry;
    }

    public EmailMessage CreateMessageFromTemplate(string templateKey, string messageId)
    {
      EmailTemplate template;
      EmailMessage message;

      template = templateRegistry.GetClone(templateKey);

      message = new EmailMessage(messageId);
      message.Subject = template.Subject;
      message.Body = template.Body;
      message.Recipients = CopyRecipients(template.DefaultRecipients);

      return message;
    }

    public EmailTemplate CloneTemplate(string templateKey)
    {
      EmailTemplate copy;

      copy = templateRegistry.GetClone(templateKey);

      return copy;
    }

    private static List<EmailAddress> CopyRecipients(List<EmailAddress> recipientList)
    {
      List<EmailAddress> copyList;
      int recipientCount;
      int recipientIndex;

      copyList = new List<EmailAddress>();
      recipientCount = recipientList.Count;

      for (recipientIndex = 0; recipientIndex < recipientCount; recipientIndex++)
      {
        copyList.Add(recipientList[recipientIndex].Clone());
      }

      return copyList;
    }
  }
}