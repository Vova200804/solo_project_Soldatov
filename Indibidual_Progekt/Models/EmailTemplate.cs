using System.Collections.Generic;
using Individual_project.PatternPrototype;

namespace Individual_project.Models
{
  public sealed class EmailTemplate : IPrototype<EmailTemplate>
  {
    public EmailTemplate(string templateKey, string subject, string body)
    {
      TemplateKey = templateKey;
      Subject = subject;
      Body = body;
      DefaultRecipients = new List<EmailAddress>();
    }

    public string TemplateKey { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public List<EmailAddress> DefaultRecipients { get; set; }

    public EmailTemplate Clone()
    {
      EmailTemplate copy;

      copy = new EmailTemplate(TemplateKey, Subject, Body);
      copy.DefaultRecipients = CloneRecipients(DefaultRecipients);

      return copy;
    }

    private static List<EmailAddress> CloneRecipients(List<EmailAddress> recipientList)
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