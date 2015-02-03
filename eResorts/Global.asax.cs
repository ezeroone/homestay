using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using eResorts.Controllers;
using eResorts.Infrastructure;
using eZeroOne.Domain;

namespace eResorts
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //ModelBinders.Binders.Add(typeof(DateTime), new BaseController.DateTimeBinder());

            AreaRegistration.RegisterAllAreas();
           
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //ControllerBuilder.Current.SetControllerFactory(new eZeroOne.eResorts.NinjectControllerFactory());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
            WindsorWrapper();
        }

       
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //dispose of the datacontxt you created when the request began
            var dataContext = HttpContext.Current.Items[typeof(eResortsEntities)];
            if (dataContext != null && dataContext is IDisposable)
            {
                (dataContext as IDisposable).Dispose();
                HttpContext.Current.Items[typeof(eResortsEntities)] = null;
            }
        }
        private static void WindsorWrapper()
        {
            WindsorContainerWrapper.Container = new WindsorContainer();
            WindsorContainerWrapper.Container.Register(Component.For<eResortsEntities>().LifeStyle.PerWebRequest);

            WindsorContainerWrapper.Container.Install(new LoggerInstaller(),
                                                    new RepositoriesInstaller(),
                                                    new ControllersInstaller());

            var controllerFactory = new WindsorControllerFactory(WindsorContainerWrapper.Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}