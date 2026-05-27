using System.Collections.Generic;
using Individual_project.Models;

namespace Individual_project.Logic
{
  public sealed class TemplateRegistry
  {
    private readonly Dictionary<string, EmailTemplate> templateByKey;

    public TemplateRegistry()
    {
      templateByKey = new Dictionary<string, EmailTemplate>();
    }

    public void Register(string key, EmailTemplate template)
    {
      templateByKey[key] = template;
    }

    public bool HasTemplate(string key)
    {
      bool exists;

      exists = templateByKey.ContainsKey(key);

      return exists;
    }

    public EmailTemplate GetClone(string key)
    {
      EmailTemplate original;
      EmailTemplate copy;

      original = templateByKey[key];
      copy = original.Clone();

      return copy;
    }

    public List<string> ListKeys()
    {
      List<string> keyList;

      keyList = new List<string>(templateByKey.Keys);
      keyList.Sort();

      return keyList;
    }
  }
}