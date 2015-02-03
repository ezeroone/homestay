using System.Web.Mvc;

namespace eResorts.Areas.Clients
{
    public class ClientsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Clients";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Clients_default",
            //    "Clients/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
            context.MapRoute(
              "Clients_default",
              "Clients/{controller}/{action}/{id}",
              new { action = "AddProperty", id = UrlParameter.Optional },
              new string[] { "eResorts.Areas.Clients.Controllers" }
          );
        }
    }
}
