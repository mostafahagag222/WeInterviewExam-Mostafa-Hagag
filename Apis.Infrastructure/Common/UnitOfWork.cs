using Apis.Core.Context;
using Apis.Core.Interfaces.Common;
using Apis.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apis.Infrastructure.Common
{
    public class UnitOfWork(
        OutagesDbContext context,
        Lazy<ICabinRepository> cabinRepository,
        Lazy<ICableRepository> cableRepository,
        Lazy<ICuttingDownARepository> cuttingDownARepository,
        Lazy<ICuttingDownBRepository> cuttingDownBRepository,
        Lazy<IStaProblemTypeRepository> staProblemTypeRepository
    ) : IUnitOfWork
    {
        private IDbContextTransaction _currentTransaction;

        public ICabinRepository CabinRepository => cabinRepository.Value;
        public ICableRepository CableRepository => cableRepository.Value;
        public ICuttingDownARepository CuttingDownARepository => cuttingDownARepository.Value;
        public ICuttingDownBRepository CuttingDownBRepository => cuttingDownBRepository.Value;
        public IStaProblemTypeRepository StaProblemTypeRepository => staProblemTypeRepository.Value;


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
