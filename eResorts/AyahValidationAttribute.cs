using System.Web.Mvc;
using AreYouAHuman;

namespace eResorts
{
    public class AyahValidationAttribute : ActionFilterAttribute 
    {
        protected AyahServiceIntegration Service = new AyahServiceIntegration();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string sessionSecret = filterContext.HttpContext.Request.Form.Get("session_secret");
            if (!Service.ScoreResult(sessionSecret, "3117d926653b780d1c87d7b4b5c4814c01059762"))
            {
                filterContext.Controller.ViewData.ModelState.AddModelError("ayah", "please prove you are a human before proceeding...");
            }
            else
            {
                Service.RecordConversion(sessionSecret);
            }
            base.OnActionExecuting(filterContext);
        } 
    }
}