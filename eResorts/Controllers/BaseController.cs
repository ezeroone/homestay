using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eResorts.Controllers
{
    public class BaseController : Controller
    {
        public class DateTimeBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var uiCulture = System.Configuration.ConfigurationManager.AppSettings["UICulture"].ToString(CultureInfo.InvariantCulture);
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                var date = value.ConvertTo(typeof(DateTime), CultureInfo.GetCultureInfo(uiCulture));

                return date;
            }
        }

    }
}
