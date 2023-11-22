using Microsoft.EntityFrameworkCore;

namespace EF_Failings;

public class TableCreator
{
    private readonly string _createTableScript = $@"
                CREATE TABLE [dbo].[TestEf](
	            [id] [int] IDENTITY({(int.MinValue)},1) NOT NULL,
	            [transactiondate] [datetime] NOT NULL,
	            [someinfo] [int] NOT NULL,
             CONSTRAINT [PK_notifications] PRIMARY KEY CLUSTERED 
            (
	            [id] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY] 
";
    
    public void RecreateTableIfNotExistOrContainsNRecords(TestContext ctx, int n)
    {
        var rebuildTableScript = $@"
            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'TestEf')
            BEGIN
                {_createTableScript}
            END
            ELSE IF (SELECT COUNT(*) FROM TestEf) = {n}
            BEGIN
                DROP TABLE TestEf
                {_createTableScript}
            END
        ";
        
        ctx.Database.ExecuteSqlRaw(rebuildTableScript);
    }
}