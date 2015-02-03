
using eZeroOne.Service.Repository;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;

namespace eResorts.Infrastructure
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //registering the UnitOfWork implementation in the container

            container.Register(Component.For(typeof(IRepository))
                                        .ImplementedBy((typeof(Repository)))
                                        .LifestylePerWebRequest());

            container.Register(Component.For<IUnitOfWork>()
                                        .ImplementedBy<UnitOfWork>()
                                        .LifestylePerWebRequest());


            
            //container.Register(Component.For(typeof(IRepository<>))
            //                            .ImplementedBy((typeof(Repository<>)))
            //                            .LifestylePerWebRequest());


            ////registering the UnitOfWork implementation in the container
            //container.Register(Component.For<IUnitOfWorkService>()
            //                            .ImplementedBy<UnitOfWorkService>()
            //                            .LifestylePerWebRequest());


        }
    }
}