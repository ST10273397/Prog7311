﻿using Prog7311POE.Models;
using Prog7311POE.Data;
using Microsoft.EntityFrameworkCore;

namespace Prog7311POE.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync() => await _context.Products.ToListAsync();

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetByFarmerIdAsync(int farmerId)
        {
            return await _context.Products.Where(p => p.FarmerId == farmerId).ToListAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<List<Product>> SearchByNameAsync(string searchTerm)
        {
            return await _context.Products.Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}%")).ToListAsync();
        }

        public async Task<List<Product>> SearchByCategoryAsync(string searchTerm)
        {
            return await _context.Products.Where(p => EF.Functions.Like(p.Category, $"%{searchTerm}%")).ToListAsync();
        }

        public async Task<List<Product>> SearchByDateAsync(DateTime startDateRange, DateTime endDateRange)
        {
            return await _context.Products.Where(p => p.productionDate >= startDateRange && p.productionDate <= endDateRange).ToListAsync();
        }


    }
}
