namespace EF_Failings;
using Microsoft.EntityFrameworkCore;

public class TestContext : DbContext
{
    public TestContext(DbContextOptions<TestContext> options) : base(options)
    {
    }

    public DbSet<TestEf> TestEf { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestEf>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TransactionDate).IsRequired();
            entity.Property(e => e.SomeInfo).IsRequired();
        });
    }
}