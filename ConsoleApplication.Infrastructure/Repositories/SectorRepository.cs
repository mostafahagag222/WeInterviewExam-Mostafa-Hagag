using ConsoleApplication.Infrastructure.Common;
using WeInterviewExam.Core.Context;
using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Repositories;

namespace ConsoleApplication.Infrastructure.Repositories;

public class SectorRepository(OutagesDbContext context) : GenericRepository<Sector>(context), ISectorRepository
{
    
}