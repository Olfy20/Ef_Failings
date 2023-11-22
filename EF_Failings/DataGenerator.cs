namespace EF_Failings;

public class DataGenerator
{
    private readonly Random _random = new();

    public List<TestEf> GenerateTestEntities(int n)
    {
        var entities = new List<TestEf>();

        for (var i = 0; i < n; i++)
        {
            var testEf = new TestEf
            {
                TransactionDate = DateTime.Now,
                SomeInfo = _random.Next(1, 100)
            };

            entities.Add(testEf);
        }

        return entities;
    }
}