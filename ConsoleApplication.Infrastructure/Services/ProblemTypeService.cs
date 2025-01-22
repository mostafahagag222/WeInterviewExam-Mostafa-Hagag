using WeInterviewExam.Core.Entities;
using WeInterviewExam.Core.Interfaces.Common;
using WeInterviewExam.Core.Interfaces.Services;

namespace ConsoleApplication.Infrastructure.Services;

public class ProblemTypeService(IUnitOfWork unitOfWork) : IProblemTypeService
{
    public async Task CreateProblemTypesAsync()
    {
        Console.WriteLine("Creating problem types");
        var staProblemTypes = new List<string>()
        {
            "حريق",
            "ضغط عالي",
            "استهلاك عالي",
            "مديونية",
            "تلف عداد",
            "سرقة تيار",
            "امطار",
            "كسر ماسورة مياه",
            "كسر ماسورة غاز",
            "تحديث و احلال",
            "صيانة",
            "كابل مقطوع",
            "توصيل كابل"
        }.Select(x => new StaProblemType()
        {
            Name = x
        });

        var ftaProblemTypes = new List<string>()
        {
            "حريق",
            "ضغط عالي",
            "استهلاك عالي",
            "مديونية",
            "تلف عداد",
            "سرقة تيار",
            "امطار",
            "كسر ماسورة مياه",
            "كسر ماسورة غاز",
            "تحديث و احلال",
            "صيانة",
            "كابل مقطوع",
            "توصيل كابل"
        }.Select(x => new FtaProblemType()
        {
            Name = x
        }).ToList();
        if (!await unitOfWork.FtaProblemTypeRepository.ExistsAsync(x => true))
            unitOfWork.FtaProblemTypeRepository.AddRange(ftaProblemTypes);
        if(!await unitOfWork.StaProblemTypeRepository.ExistsAsync(x => true))
            unitOfWork.StaProblemTypeRepository.AddRange(staProblemTypes);
        await unitOfWork.SaveChangesAsync();
    }
}