using System.Globalization;
using System.IO;
using System.Text;
using Individual_project.Models;
using Individual_project.Utils;

namespace Individual_project.FileOutput
{
  public sealed class EmailTextExporter
  {
    public string ExportToFile(EmailMessage message)
    {
      string outputFolder;
      string filePath;
      string content;

      outputFolder = OutputFolder.EnsureOutputFolder();
      filePath = Path.Combine(outputFolder, message.MessageId + ".txt");

      content = BuildText(message);
      File.WriteAllText(filePath, content, Encoding.UTF8);

      return filePath;
    }

    public string BuildText(EmailMessage message)
    {
      StringBuilder builder;
      string createdAtText;
      int recipientCount;
      int recipientIndex;

      builder = new StringBuilder();
      createdAtText = message.CreatedAt.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

      builder.AppendLine("=== EMAIL MESSAGE ===");
      builder.AppendLine("Id: " + message.MessageId);
      builder.AppendLine("CreatedAt: " + createdAtText);
      builder.AppendLine();

      builder.AppendLine("Recipients:");
      recipientCount = message.Recipients.Count;

      for (recipientIndex = 0; recipientIndex < recipientCount; recipientIndex++)
      {
        builder.AppendLine("  - " + message.Recipients[recipientIndex].Name + " <" + message.Recipients[recipientIndex].Address + ">");
      }

      builder.AppendLine();
      builder.AppendLine("Subject: " + message.Subject);
      builder.AppendLine();
      builder.AppendLine("Body:");
      builder.AppendLine(message.Body);

      return builder.ToString();
    }
  }
}

