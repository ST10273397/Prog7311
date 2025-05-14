using Prog7311POE.Models;
using Prog7311POE.Data;
using Microsoft.EntityFrameworkCore;

namespace Prog7311POE.Services
{
    public class EmployeeService
    {
        private readonly AppDbContext _context;
        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();

        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id) => await _context.Employees.FindAsync(id);

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null) 
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Employee?> GetByIdWithFarmersAsync(int id)
        {
            return await _context.Employees.Include(e => e.Farmers).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employee?> GetByIdWithFarmersAndProductsAsync(int id)
        {
            return await _context.Employees.Include(e => e.Farmers).ThenInclude(f => f.Products).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Employees.AnyAsync(e => e.Id == id);
        }

        public async Task<List<Employee>> SearchByNameAsync(string searchTerm)
        {
            return await _context.Employees.Where(e => e.FullName.Contains(searchTerm.ToLower())).ToListAsync();
        }

    }
}
