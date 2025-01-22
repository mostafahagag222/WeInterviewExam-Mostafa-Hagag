using Autofac;
using ConsoleApplication.Infrastructure.Common;
using ConsoleApplication.Infrastructure.Repositories;
using ConsoleApplication.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using WeInterviewExam.Core.Context;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Repositories;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Presentation
{
    public static class Configurations
    {
        public static ContainerBuilder Configure()
        {
            var builder = new ContainerBuilder();

            // Register UnitOfWork
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            // Regular registration for UserRepository and Func registration for UserRepository
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, IUserRepository>>(_ =>
                    context => context.Resolve<IUserRepository>())
                .As<Func<IComponentContext, IUserRepository>>()
                .InstancePerLifetimeScope();

// Regular registration for BlockRepository and Func registration for BlockRepository
            builder.RegisterType<BlockRepository>()
                .As<IBlockRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, IBlockRepository>>(_ =>
                    context => context.Resolve<IBlockRepository>())
                .As<Func<IComponentContext, IBlockRepository>>()
                .InstancePerLifetimeScope();

// Regular registration for BuildingRepository and Func registration for BuildingRepository
            builder.RegisterType<BuildingRepository>()
                .As<IBuildingRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, IBuildingRepository>>(_ =>
                    context => context.Resolve<IBuildingRepository>())
                .As<Func<IComponentContext, IBuildingRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CabineRepository>()
                .As<ICabinRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, ICabinRepository>>(_ =>
                    context => context.Resolve<ICabinRepository>())
                .As<Func<IComponentContext, ICabinRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CableRepository>()
                .As<ICableRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, ICableRepository>>(_ =>
                    context => context.Resolve<ICableRepository>())
                .As<Func<IComponentContext, ICableRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ChannelRepository>()
                .As<IChannelRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, IChannelRepository>>(_ =>
                    context => context.Resolve<IChannelRepository>())
                .As<Func<IComponentContext, IChannelRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CityRepository>()
                .As<ICityRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, ICityRepository>>(_ =>
                    context => context.Resolve<ICityRepository>())
                .As<Func<IComponentContext, ICityRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FlatRepository>()
                .As<IFlatRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, IFlatRepository>>(_ =>
                    context => context.Resolve<IFlatRepository>())
                .As<Func<IComponentContext, IFlatRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GovernrateRepository>()
                .As<IGovernrateRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, IGovernrateRepository>>(_ =>
                    context => context.Resolve<IGovernrateRepository>())
                .As<Func<IComponentContext, IGovernrateRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NetworkElementHierarchyPathRepository>()
                .As<INetworkElementHierarchyPathRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, INetworkElementHierarchyPathRepository>>(_ =>
                    context => context.Resolve<INetworkElementHierarchyPathRepository>())
                .As<Func<IComponentContext, INetworkElementHierarchyPathRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NetworkElementRepository>()
                .As<INetworkElementRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, INetworkElementRepository>>(_ =>
                    context => context.Resolve<INetworkElementRepository>())
                .As<Func<IComponentContext, INetworkElementRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NetworkElementTypeRepository>()
                .As<INetworkElementTypeRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, INetworkElementTypeRepository>>(_ =>
                    context => context.Resolve<INetworkElementTypeRepository>())
                .As<Func<IComponentContext, INetworkElementTypeRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FtaProblemTypeRepository>()
                .As<IFtaProblemTypeRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, IFtaProblemTypeRepository>>(_ =>
                    context => context.Resolve<IFtaProblemTypeRepository>())
                .As<Func<IComponentContext, IFtaProblemTypeRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StaProblemTypeRepository>()
                .As<IStaProblemTypeRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, IStaProblemTypeRepository>>(_ =>
                    context => context.Resolve<IStaProblemTypeRepository>())
                .As<Func<IComponentContext, IStaProblemTypeRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SectorRepository>()
                .As<ISectorRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, ISectorRepository>>(_ =>
                    context => context.Resolve<ISectorRepository>())
                .As<Func<IComponentContext, ISectorRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StationRepository>()
                .As<IStationRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, IStationRepository>>(_ =>
                    context => context.Resolve<IStationRepository>())
                .As<Func<IComponentContext, IStationRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SubscribtionRepository>()
                .As<ISubscribtionRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, ISubscribtionRepository>>(_ =>
                    context => context.Resolve<ISubscribtionRepository>())
                .As<Func<IComponentContext, ISubscribtionRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TowerRepository>()
                .As<ITowerRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, ITowerRepository>>(_ =>
                    context => context.Resolve<ITowerRepository>())
                .As<Func<IComponentContext, ITowerRepository>>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ZoneRepository>()
                .As<IZoneRepository>()
                .InstancePerLifetimeScope();

            builder.Register<Func<IComponentContext, IZoneRepository>>(_ =>
                    context => context.Resolve<IZoneRepository>())
                .As<Func<IComponentContext, IZoneRepository>>()
                .InstancePerLifetimeScope();


            builder
                .RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<GovernrateService>()
                .As<IGovernrateService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ZoneService>()
                .As<IZoneService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<StationService>()
                .As<IStationService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CabinService>()
                .As<ICabinService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<BlockService>()
                .As<IBlockService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<SubscriptionService>()
                .As<ISubscriptionService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ProblemTypeService>()
                .As<IProblemTypeService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<NetworkElementTypesService>()
                .As<INetworkElementTypesService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ChannelService>()
                .As<IChannelService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<NetworkElementHierarchyPathService>()
                .As<INetworkElementHierarchyPathService>()
                .InstancePerLifetimeScope();

            // Register the DbContext
            builder.Register(_ =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<OutagesDbContext>();
                    optionsBuilder.UseSqlServer(
                        "server=.;DataBase=we_interview_exam;Integrated Security=true;Encrypt=false");
                    return new OutagesDbContext(optionsBuilder.Options);
                })
                .AsSelf()
                .InstancePerLifetimeScope();

            return builder;
        }
    }
}