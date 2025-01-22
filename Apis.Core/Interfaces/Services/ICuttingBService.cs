using Apis.Core.Entities;

namespace Apis.Core.Interfaces.Services;

public interface ICuttingBService
{
    Task<bool> GenerateCableCuttingsAsync();
}