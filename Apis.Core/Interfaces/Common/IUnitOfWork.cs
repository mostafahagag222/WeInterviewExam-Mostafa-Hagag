using Apis.Core.Interfaces.Repositories;

namespace Apis.Core.Interfaces.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        ICabinRepository CabinRepository { get; }
        ICableRepository CableRepository { get; }
        ICuttingDownARepository CuttingDownARepository { get; }
        ICuttingDownBRepository CuttingDownBRepository { get; }
        IStaProblemTypeRepository StaProblemTypeRepository { get; }
    }
}