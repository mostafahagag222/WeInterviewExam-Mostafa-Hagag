namespace WeInterviewExam.Core.Interfaces.Services;

public interface ISubscriptionService
{
    Task AddSubscriptionsAsync();
    Task AddCorporateSubscriptionsAsync();
}