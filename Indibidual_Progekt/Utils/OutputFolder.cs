using System.IO;

namespace Individual_project.Utils
{
  public static class OutputFolder
  {
    public static string EnsureOutputFolder()
    {
      string folderPath;

      folderPath = Path.Combine(Directory.GetCurrentDirectory(), "output");

      if (!Directory.Exists(folderPath))
      {
        Directory.CreateDirectory(folderPath);
      }

      return folderPath;
    }
  }
}

