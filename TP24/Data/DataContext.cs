using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TP24.Models;

namespace TP24.Data
{
    public interface IDataContext
    {
        DbSet<ReceivablePayload> Receivables { get; set; }
        Task<int> SaveChangesAsync();
    }


    public class DataContext : DbContext, IDataContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<ReceivablePayload> Receivables { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
