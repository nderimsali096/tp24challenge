using TP24.Data;
using TP24.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TP24.Repositories
{
    public interface IReceivableRepository
    {
        Task Add(ReceivablePayload receivable);
        Task<List<ReceivablePayload>> GetAllReceivables();
    }

    public class ReceivableRepository : IReceivableRepository
    {
        private readonly IDataContext _dbContext;

        public ReceivableRepository(IDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(ReceivablePayload receivable)
        {
            await _dbContext.Receivables.AddAsync(receivable);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ReceivablePayload>> GetAllReceivables()
        {
            return await _dbContext.Receivables.ToListAsync();
        }
    }
}
