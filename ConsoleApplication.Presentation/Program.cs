namespace ConsoleApplication.Presentation;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = Configurations.Configure();
        var container = builder.Build();

        await App.Run(container);
    }
}