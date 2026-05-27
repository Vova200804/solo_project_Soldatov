using Individual_project.Logic;
using Individual_project.Models;

namespace Individual_project.Factories
{
  public static class TemplateFactory
  {
    public static TemplateRegistry CreateDefaultRegistry()
    {
      TemplateRegistry registry;
      EmailTemplate passwordReset;
      EmailTemplate orderStatus;

      registry = new TemplateRegistry();

      passwordReset = new EmailTemplate(
        "password-reset",
        "Password reset",
        "Hello {name},\n\nUse this code to reset your password: {code}\n\nRegards,\nSupport team"
      );

      passwordReset.DefaultRecipients.Add(new EmailAddress("Client", "client@example.com"));

      orderStatus = new EmailTemplate(
        "order-status",
        "Order status update",
        "Hello {name},\n\nYour order {orderNumber} is now: {status}\n\nRegards,\nStore"
      );

      orderStatus.DefaultRecipients.Add(new EmailAddress("Client", "client@example.com"));

      registry.Register(passwordReset.TemplateKey, passwordReset);
      registry.Register(orderStatus.TemplateKey, orderStatus);

      return registry;
    }
  }
}
