using Autofac;
using ScoringSystem.DAL.Context;
using ScoringSystem.DAL.Repository;
using ScoringSystem.DAL.UnitOfWork;

namespace ScoringSystem.Infrastructure.ContainerConfiguration
{
    class DalContainerConfigurator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<ScoringSystemContext>().AsSelf().SingleInstance();
            builder.RegisterGeneric(typeof(SqlRepository<>)).As(typeof(IRepository<>));
            builder.RegisterType<RepositoryFactory>().As<IRepositoryFactory>();
        }
    }
}
