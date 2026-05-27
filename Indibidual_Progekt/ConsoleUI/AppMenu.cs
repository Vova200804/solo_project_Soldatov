using System;
using System.Collections.Generic;
using Individual_project.Factories;
using Individual_project.FileOutput;
using Individual_project.Logic;
using Individual_project.Models;

namespace Individual_project.ConsoleUI
{
  public sealed class AppMenu
  {
    private const int ExitOption = 0;
    private const int ListTemplatesOption = 1;
    private const int CreateMessageOption = 2;
    private const int CloneTemplateOption = 3;
    private const int RunDemoOption = 4;

    private readonly TemplateRegistry templateRegistry;
    private readonly EmailGenerator emailGenerator;
    private readonly EmailTextExporter emailTextExporter;

    public AppMenu()
    {
      templateRegistry = TemplateFactory.CreateDefaultRegistry();
      emailGenerator = new EmailGenerator(templateRegistry);
      emailTextExporter = new EmailTextExporter();
    }

    public void Run()
    {
      int option;

      do
      {
        PrintMenu();
        option = ReadInt("Select option: ");

        Console.WriteLine();

        if (option == ListTemplatesOption)
        {
          ListTemplates();
        }
        else if (option == CreateMessageOption)
        {
          CreateMessageFromTemplate();
        }
        else if (option == CloneTemplateOption)
        {
          CloneTemplateAndCreateMessage();
        }
        else if (option == RunDemoOption)
        {
          RunDemoScenarios();
        }
        else if (option == ExitOption)
        {
          Console.WriteLine("Bye.");
        }
        else
        {
          Console.WriteLine("Unknown option.");
        }

        Console.WriteLine();
      } while (option != ExitOption);
    }

    public void RunDemoOnly()
    {
      RunDemoScenarios();
    }

    private static void PrintMenu()
    {
      Console.WriteLine("=== EMAIL TEMPLATE CONSTRUCTOR ===");
      Console.WriteLine("1. List templates");
      Console.WriteLine("2. Create email message from template");
      Console.WriteLine("3. Clone template and create message");
      Console.WriteLine("4. Run demo scenarios");
      Console.WriteLine("0. Exit");
    }

    private void ListTemplates()
    {
      List<string> keyList;
      int keyCount;
      int keyIndex;

      keyList = templateRegistry.ListKeys();

      Console.WriteLine("Templates:");
      keyCount = keyList.Count;

      for (keyIndex = 0; keyIndex < keyCount; keyIndex++)
      {
        Console.WriteLine("  - " + keyList[keyIndex]);
      }
    }

    private void CreateMessageFromTemplate()
    {
      string templateKey;
      string messageId;
      EmailMessage message;
      string filePath;

      templateKey = ReadString("Template key: ");
      messageId = ReadString("Message id (example MSG-001): ");

      if (!templateRegistry.HasTemplate(templateKey))
      {
        Console.WriteLine("Template not found.");
        return;
      }

      message = emailGenerator.CreateMessageFromTemplate(templateKey, messageId);
      FillPlaceholders(message);

      filePath = emailTextExporter.ExportToFile(message);

      Console.WriteLine();
      Console.WriteLine(emailTextExporter.BuildText(message));
      Console.WriteLine("Saved: " + filePath);
    }

    private void CloneTemplateAndCreateMessage()
    {
      string templateKey;
      EmailTemplate templateCopy;
      string messageId;
      EmailMessage message;
      string filePath;

      templateKey = ReadString("Template key for cloning: ");

      if (!templateRegistry.HasTemplate(templateKey))
      {
        Console.WriteLine("Template not found.");
        return;
      }

      templateCopy = emailGenerator.CloneTemplate(templateKey);
      EditTemplateCopy(templateCopy);

      messageId = ReadString("Message id (example MSG-002): ");

      message = new EmailMessage(messageId);
      message.Subject = templateCopy.Subject;
      message.Body = templateCopy.Body;
      message.Recipients = templateCopy.DefaultRecipients;

      FillPlaceholders(message);
      filePath = emailTextExporter.ExportToFile(message);

      Console.WriteLine();
      Console.WriteLine(emailTextExporter.BuildText(message));
      Console.WriteLine("Saved: " + filePath);
    }

    private static void EditTemplateCopy(EmailTemplate template)
    {
      string newSubject;
      string newBody;

      Console.WriteLine("Cloned template. You can change subject/body.");

      newSubject = ReadString("New subject (empty to keep): ");

      if (newSubject.Length > 0)
      {
        template.Subject = newSubject;
      }

      newBody = ReadString("New body (empty to keep): ");

      if (newBody.Length > 0)
      {
        template.Body = newBody;
      }
    }

    private static void FillPlaceholders(EmailMessage message)
    {
      string name;
      string code;
      string orderNumber;
      string status;

      name = ReadString("Name for {name}: ");
      code = ReadString("Code for {code} (can be empty): ");
      orderNumber = ReadString("Order number for {orderNumber} (can be empty): ");
      status = ReadString("Status for {status} (can be empty): ");

      message.Subject = ReplaceToken(message.Subject, "{name}", name);
      message.Subject = ReplaceToken(message.Subject, "{code}", code);
      message.Subject = ReplaceToken(message.Subject, "{orderNumber}", orderNumber);
      message.Subject = ReplaceToken(message.Subject, "{status}", status);

      message.Body = ReplaceToken(message.Body, "{name}", name);
      message.Body = ReplaceToken(message.Body, "{code}", code);
      message.Body = ReplaceToken(message.Body, "{orderNumber}", orderNumber);
      message.Body = ReplaceToken(message.Body, "{status}", status);
    }

    private static string ReplaceToken(string text, string token, string value)
    {
      string result;

      result = text.Replace(token, value);

      return result;
    }

    private void RunDemoScenarios()
    {
      Console.WriteLine("Demo scenarios started.");
      Console.WriteLine();

      DemoListTemplates();
      DemoMessage(
        "password-reset",
        "MSG-001",
        "Alice",
        "481516",
        string.Empty,
        string.Empty
      );

      DemoMessage(
        "order-status",
        "MSG-002",
        "Bob",
        string.Empty,
        "ORD-77",
        "Ready"
      );

      DemoCloneAndMessage("password-reset", "MSG-003");

      Console.WriteLine();
      Console.WriteLine("Demo scenarios finished.");
    }

    private void DemoListTemplates()
    {
      Console.WriteLine("Scenario 1: list templates");
      ListTemplates();
      Console.WriteLine();
    }

    private void DemoMessage(
      string templateKey,
      string messageId,
      string name,
      string code,
      string orderNumber,
      string status
    )
    {
      EmailMessage message;
      string filePath;

      Console.WriteLine("Scenario: message from template " + templateKey);

      message = emailGenerator.CreateMessageFromTemplate(templateKey, messageId);

      message.Subject = ReplaceToken(message.Subject, "{name}", name);
      message.Subject = ReplaceToken(message.Subject, "{code}", code);
      message.Subject = ReplaceToken(message.Subject, "{orderNumber}", orderNumber);
      message.Subject = ReplaceToken(message.Subject, "{status}", status);

      message.Body = ReplaceToken(message.Body, "{name}", name);
      message.Body = ReplaceToken(message.Body, "{code}", code);
      message.Body = ReplaceToken(message.Body, "{orderNumber}", orderNumber);
      message.Body = ReplaceToken(message.Body, "{status}", status);

      filePath = emailTextExporter.ExportToFile(message);

      Console.WriteLine("Saved: " + filePath);
      Console.WriteLine();
    }

    private void DemoCloneAndMessage(string templateKey, string messageId)
    {
      EmailTemplate templateCopy;
      EmailMessage message;
      string filePath;

      Console.WriteLine("Scenario: clone template and modify");

      templateCopy = emailGenerator.CloneTemplate(templateKey);
      templateCopy.Subject = templateCopy.Subject + " (updated)";

      message = new EmailMessage(messageId);
      message.Subject = ReplaceToken(templateCopy.Subject, "{name}", "Charlie");
      message.Body = ReplaceToken(templateCopy.Body, "{name}", "Charlie");
      message.Body = ReplaceToken(message.Body, "{code}", "000000");
      message.Recipients = templateCopy.DefaultRecipients;

      filePath = emailTextExporter.ExportToFile(message);
      Console.WriteLine("Saved: " + filePath);
    }

    private static int ReadInt(string prompt)
    {
      string text;
      int value;
      bool isParsed;

      Console.Write(prompt);
      text = Console.ReadLine();

      if (text == null)
      {
        text = string.Empty;
      }

      isParsed = int.TryParse(text, out value);

      while (!isParsed)
      {
        Console.Write("Enter a number: ");
        text = Console.ReadLine();

        if (text == null)
        {
          text = string.Empty;
        }

        isParsed = int.TryParse(text, out value);
      }

      return value;
    }

    private static string ReadString(string prompt)
    {
      string text;

      Console.Write(prompt);
      text = Console.ReadLine();

      if (text == null)
      {
        text = string.Empty;
      }

      return text;
    }
  }
}
