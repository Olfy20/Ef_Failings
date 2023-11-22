using EF_Failings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        var generator = new DataGenerator();
        var options = GetOptions();
        
        using (var context = new TestContext(options))
        {
            new TableCreator().RecreateTableIfNotExistOrContainsNRecords(context, 3000);

            var generatedEntitiesCount = !context.TestEf.Any() ? 2000 : 1000;
            /* if uncomment will throw exception every time if count() == 2000
             
             var newest = context.TestEf
                .OrderByDescending(e => e.Id)
                .FirstOrDefault();*/
            context.TestEf.AddRange(generator.GenerateTestEntities(generatedEntitiesCount));
            context.SaveChanges();
        }

        Console.WriteLine("was fine this time");
    }

    private static DbContextOptions<TestContext> GetOptions()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
 
        IConfiguration config = builder.Build();
        var connectionString = config["ConnectionString:SqlServer"];
        
        var options = new DbContextOptionsBuilder<TestContext>()
            .UseSqlServer(connectionString)
            .Options;
        return options;
    }
}