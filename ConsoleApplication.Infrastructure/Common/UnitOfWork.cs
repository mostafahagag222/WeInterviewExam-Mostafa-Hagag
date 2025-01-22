﻿using Microsoft.EntityFrameworkCore.Storage;
using WeInterviewExam.Core.Context;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Repositories;

namespace ConsoleApplication.Infrastructure.Common
{
    public class UnitOfWork(
        OutagesDbContext context,
        Lazy<IBlockRepository> blockRepository,
        Lazy<IBuildingRepository> buildingRepository,
        Lazy<ICabinRepository> cabinRepository,
        Lazy<ICableRepository> cableRepository,
        Lazy<IChannelRepository> channelRepository,
        Lazy<ICityRepository> cityRepository,
        Lazy<IFlatRepository> flatRepository,
        Lazy<IGovernrateRepository> governrateRepository,
        Lazy<INetworkElementHierarchyPathRepository> networkElementHierarchyRepository,
        Lazy<INetworkElementRepository> networkElementRepository,
        Lazy<INetworkElementTypeRepository> networkElementTypeRepository,
        Lazy<IFtaProblemTypeRepository> ftaProblemTypeRepository,
        Lazy<IStaProblemTypeRepository> staProblemTypeRepository,
        Lazy<ISectorRepository> sectorRepository,
        Lazy<IStationRepository> stationRepository,
        Lazy<ISubscribtionRepository> subscriptionRepository,
        Lazy<ITowerRepository> towerRepository,
        Lazy<IUserRepository> userRepository,
        Lazy<IZoneRepository> zoneRepository
    ) : IUnitOfWork
    {
        private IDbContextTransaction _currentTransaction;

        public IBlockRepository BlockRepository => blockRepository.Value;
        public IBuildingRepository BuildingRepository => buildingRepository.Value;
        public ICabinRepository CabinRepository => cabinRepository.Value;
        public ICableRepository CableRepository => cableRepository.Value;
        public IChannelRepository ChannelRepository => channelRepository.Value;
        public ICityRepository CityRepository => cityRepository.Value;
        public IFlatRepository FlatRepository => flatRepository.Value;
        public IGovernrateRepository GovernrateRepository => governrateRepository.Value;
        public INetworkElementHierarchyPathRepository NetworkElementHierarchyRepository => networkElementHierarchyRepository.Value;
        public INetworkElementRepository NetworkElementRepository => networkElementRepository.Value;
        public INetworkElementTypeRepository NetworkElementTypeRepository => networkElementTypeRepository.Value;
        public IFtaProblemTypeRepository FtaProblemTypeRepository => ftaProblemTypeRepository.Value;
        public IStaProblemTypeRepository StaProblemTypeRepository => staProblemTypeRepository.Value;
        public ISectorRepository SectorRepository => sectorRepository.Value;
        public IStationRepository StationRepository => stationRepository.Value;
        public ISubscribtionRepository SubscriptionRepository => subscriptionRepository.Value;
        public ITowerRepository TowerRepository => towerRepository.Value;
        public IUserRepository UserRepository => userRepository.Value;
        public IZoneRepository ZoneRepository => zoneRepository.Value;

        // Save changes to the database
        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        // Begin a new database transaction
        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _currentTransaction = await context.Database.BeginTransactionAsync();
        }

        // Commit the transaction and rollback if an error occurs
        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        // Rollback the transaction
        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        // Dispose the database context
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
