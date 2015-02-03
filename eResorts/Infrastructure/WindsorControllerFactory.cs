using System;
using System.Web.Mvc;
using Castle.MicroKernel;
using System.Web.Routing;
using System.Web;

namespace eResorts.Infrastructure
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _Kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            _Kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            _Kernel.ReleaseComponent(instance: controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                string errorMsg = string.Format("The controller for path '{0}' could not be found", requestContext.HttpContext.Request.Path);
                throw new HttpException(404, errorMsg);
            }

            return (IController)_Kernel.Resolve(controllerType);
        }
    }
}