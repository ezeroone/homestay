using System;
using System.Web.Mvc;
using Ninject;

namespace eZeroOne.eResorts
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _nInjectKernel;

        public NinjectControllerFactory()
        {
            _nInjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_nInjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            _nInjectKernel.Bind<eZeroOne.Service.Repository.IRepository>().To<eZeroOne.Service.Repository.Repository>().InRequestScope();
            _nInjectKernel.Bind<eZeroOne.Service.Repository.IUnitOfWork>().To<eZeroOne.Service.Repository.UnitOfWork>().InRequestScope();


            //Register services with Ninject DI Container
            _nInjectKernel.Bind<eZeroOne.Service.Users.IUserService>().To<eZeroOne.Service.Users.UserService>().InRequestScope();
            _nInjectKernel.Bind<eZeroOne.Service.Common.ICommon>().To<eZeroOne.Service.Common.Common>().InRequestScope();
            _nInjectKernel.Bind<eZeroOne.Service.Visitors.IVisitor>().To<eZeroOne.Service.Visitors.Visitor>().InRequestScope();
            //_nInjectKernel.Bind<eZeroOne.Service.I.IItems>().To<eZeroOne.Service.Items.Items>().InRequestScope();
            _nInjectKernel.Bind<eZeroOne.Service.Customers.ICustomerService>().To<eZeroOne.Service.Customers.CustomerService>().InRequestScope();

            _nInjectKernel.Bind<eZeroOne.Service.Property.IHotelService>().To<eZeroOne.Service.Property.HotelService>().InRequestScope();   


        }
    }
}