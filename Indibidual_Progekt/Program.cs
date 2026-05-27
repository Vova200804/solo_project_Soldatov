using Individual_project.ConsoleUI;

namespace Indibidual_Progekt
{
  internal class Program
  {
    static void Main(string[] args)
    {
      AppMenu appMenu;

      appMenu = new AppMenu();

      if (args.Length > 0 && args[0] == "demo")
      {
        appMenu.RunDemoOnly();
        return;
      }

      appMenu.Run();
    }
  }
}
