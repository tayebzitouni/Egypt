using freelanceProjectEgypt03.Interfaces;
using freelanceProjectEgypt03.Models;
using Microsoft.EntityFrameworkCore;
using freelanceProjectEgypt03.data;
using System;

using System.Collections.Generic;


using System.Threading.Tasks;
using freelanceProjectEgypt03.data.freelanceProjectEgypt03.Data;

namespace freelanceProjectEgypt03.Repositories
{
    public class ServiceRepository : IRepository<Service>
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context) => _context = context;

        public async Task<string> AddAsync(Service entity)
        {
            _context.Services.Add(entity);
            await _context.SaveChangesAsync();
            return "Success";
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Services.FindAsync(id);
            if (entity == null) return false;

            _context.Services.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Service>> GetAllAsync() =>
            await _context.Services
                .Include(s => s.Partners) 
                .ToListAsync();

        public async Task<Service> GetByIdAsync(int id) =>
            await _context.Services
                .Include(s => s.Partners)  
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task<string> UpdateAsync(int id, Service entity)
        {
            var existing = await _context.Services.FindAsync(id);
            if (existing == null) return "Not Found";

            existing.Title = entity.Title;
            existing.Description = entity.Description;
            existing.DurationMin = entity.DurationMin;
            existing.DurationMax = entity.DurationMax;
            existing.DurationUnit = entity.DurationUnit;
            existing.details = entity.details;
            existing.Price = entity.Price;

            await _context.SaveChangesAsync();
            return "Updated";
        }
    }
}
