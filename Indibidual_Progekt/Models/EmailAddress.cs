namespace Individual_project.Models
{
  public sealed class EmailAddress
  {
    public EmailAddress(string name, string address)
    {
      Name = name;
      Address = address;
    }

    public string Name { get; set; }

    public string Address { get; set; }

    public EmailAddress Clone()
    {
      EmailAddress copy;

      copy = new EmailAddress(Name, Address);

      return copy;
    }
  }
}