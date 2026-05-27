using System;
using System.Collections.Generic;
using Individual_project.Factories;
using Individual_project.Logic;

namespace Individual_project.ConsoleUI
{
  public sealed class AppMenu
  {
    private const int ExitOption = 0;
    private const int ListTemplatesOption = 1;

    private readonly TemplateRegistry templateRegistry;

    public AppMenu()
    {
      templateRegistry = TemplateFactory.CreateDefaultRegistry();
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

    private static void PrintMenu()
    {
      Console.WriteLine("=== EMAIL TEMPLATE CONSTRUCTOR ===");
      Console.WriteLine("1. List templates");
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
  }
}
