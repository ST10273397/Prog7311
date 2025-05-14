using Prog7311POE.Models;
using Prog7311POE.Data;
using Microsoft.EntityFrameworkCore;

namespace Prog7311POE.Services
{
    public class FarmerService
    {
        private readonly AppDbContext _context;
        public FarmerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Farmer>> GetAllAsync() => await _context.Farmers.ToListAsync();

        public async Task AddAsync(Farmer farmer)
        {
            _context.Farmers.Add(farmer);
            await _context.SaveChangesAsync();
        }

        public async Task<Farmer?> GetByIdAsync(int id) => await _context.Farmers.FindAsync(id);

        public async Task UpdateAsync(Farmer farmer)
        {
            _context.Farmers.Update(farmer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer != null)
            {
                _context.Farmers.Remove(farmer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Farmer?> GetByIdWithProductsAsync(int id)
        {
            return await _context.Farmers.Include(f => f.Products).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Farmers.AnyAsync(e => e.Id == id);
        }

        public async Task<List<Farmer>> SearchByNameAsync(string searchTerm)
        {
            return await _context.Farmers.Where(f => EF.Functions.Like(f.FullName, $"%{searchTerm}%")).ToListAsync();
        }

    }
}
