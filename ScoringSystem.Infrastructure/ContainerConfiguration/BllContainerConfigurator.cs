using Autofac;
using ScoringSystem.BLL.Interfaces;
using ScoringSystem.BLL.Services;

namespace ScoringSystem.Infrastructure.ContainerConfiguration
{
    public class BllContainerConfigurator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BankService>().As<IBankService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<AddressService>().As<IAddressService>();
            builder.RegisterType<HealthService>().As<IHealthService>();
            builder.RegisterType<BankAccountService>().As<IBankAccountService>();
            builder.RegisterType<UserRolesService>().As<IUserRolesService>();

        }
    }
}
