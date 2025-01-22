namespace Apis.Core.Interfaces.Services;

public interface ICuttingAService
{
    Task<bool> GenerateCabinCuttingsAsync();
}