using MeterReader.Models;
using Microsoft.EntityFrameworkCore;

namespace MeterReader.Data
{
    public class MeterReaderContext : DbContext
    {
        public MeterReaderContext(DbContextOptions<MeterReaderContext> options) : base(options)
        {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<MeterReadingData> MeterReadingData { get; set; }
    }
}
