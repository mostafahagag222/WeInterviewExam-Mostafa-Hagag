using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MVC.Core.Context;
using MVC.Core.Dtos;
using MVC.Core.Interfaces.Common;
using MVC.Core.Interfaces.Repositories;

namespace MVC.Infrastructure.Common
{
    public class UnitOfWork(
        OutagesDbContext context,
        Lazy<IUserRepository> userRepository,
        Lazy<IFtaProblemTypeRepository> ftaProblemRepository,
        Lazy<IChannelRepository> channelRepository,
        Lazy<INetworkElementTypeRepository> networkElementTypeRepository,
        Lazy<ICuttingDownHeaderRepository> cuttingDownHeaderRepository ,
        Lazy<ICuttingDownDetailRepository> cuttingDownDetailRepository,
        Lazy<IHiearchyPathRepository> hiearchyPathRepository,
        Lazy<INetworkElementRepository> networkElementRepository) : IUnitOfWork
    {
        private IDbContextTransaction _currentTransaction;

        public IUserRepository UserRepository => userRepository.Value;
        public IFtaProblemTypeRepository FtaProblemTypeRepository => ftaProblemRepository.Value;
        public IChannelRepository ChannelRepository => channelRepository.Value;
        public INetworkElementTypeRepository NetworkElementTypeRepository => networkElementTypeRepository.Value;
        public ICuttingDownHeaderRepository CuttingDownHeaderRepository => cuttingDownHeaderRepository.Value;
        public ICuttingDownDetailRepository CuttingDownDetailRepository => cuttingDownDetailRepository.Value;
        public IHiearchyPathRepository HiearchyPathRepository => hiearchyPathRepository.Value;
        public INetworkElementRepository NetworkElementRepository => networkElementRepository.Value;
        public async Task<List<CuttingsForAddDto>> GetCuttingsForAddAsync(int elementId)
        {
            return await context.CuttingsForAddDtos
                .FromSqlRaw($"fta.SP_Get_Cuttings_For_Element {elementId}")
                .ToListAsync();
        }


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