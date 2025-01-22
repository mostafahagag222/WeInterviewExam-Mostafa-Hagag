using ConsoleApplication.Infrastructure.Common;
using WeInterviewExam.Core.Context;
using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Repositories;

namespace ConsoleApplication.Infrastructure.Repositories;

public class GovernrateRepository(OutagesDbContext context) : GenericRepository<Governrate>(context), IGovernrateRepository
{
    
}