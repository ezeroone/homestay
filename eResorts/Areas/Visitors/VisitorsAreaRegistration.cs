using System.Web.Mvc;

namespace eResorts.Areas.Visitors
{
    public class VisitorsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Visitors";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Visitors_default",
            //    "Visitors/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            context.MapRoute(
             "Visitors_default",
             "Visitors/{controller}/{action}/{id}",
             new { action = "Index", id = UrlParameter.Optional },
             new string[] { "eResorts.Areas.Visitors.Controllers" }
         );
        }
    }
}
