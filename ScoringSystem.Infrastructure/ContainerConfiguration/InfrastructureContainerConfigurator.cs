using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.Infrastructure.ContainerConfiguration
{
    public class InfrastructureContainerConfigurator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<BllContainerConfigurator>();
            builder.RegisterModule<DalContainerConfigurator>();
        }
    }
}
