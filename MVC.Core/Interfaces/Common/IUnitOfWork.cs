using MVC.Core.Dtos;
using MVC.Core.Interfaces.Repositories;

namespace MVC.Core.Interfaces.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        IUserRepository UserRepository { get; }
        IFtaProblemTypeRepository FtaProblemTypeRepository { get; }
        IChannelRepository ChannelRepository { get; }
        INetworkElementTypeRepository NetworkElementTypeRepository { get; }
        ICuttingDownHeaderRepository CuttingDownHeaderRepository { get; }
        ICuttingDownDetailRepository CuttingDownDetailRepository { get; }
        IHiearchyPathRepository HiearchyPathRepository { get; }
        INetworkElementRepository NetworkElementRepository { get; }
        Task<List<CuttingsForAddDto>> GetCuttingsForAddAsync(int elementId);
    }
}