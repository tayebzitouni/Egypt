using freelanceProjectEgypt03.Interfaces;
using freelanceProjectEgypt03.Models;
using freelanceProjectEgypt03.data;
using System;
using freelanceProjectEgypt03.data.freelanceProjectEgypt03.Data;
using Microsoft.EntityFrameworkCore;

namespace freelanceProjectEgypt03.Repositories
{
    public class PartnerRepository : IPartnerRepository
    {
        private readonly AppDbContext _context;

        public PartnerRepository(AppDbContext context) => _context = context;

        public async Task<string> AddAsync(Partner entity)
        {
            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            _context.Partners.Add(entity);
            await _context.SaveChangesAsync();
            return "Success";
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Partners.FindAsync(id);
            if (entity == null) return false;
            _context.Partners.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Partner>> GetAllAsync() =>
     await _context.Partners
         .Include(p => p.service)  
         .ToListAsync();

        public async Task<Partner> GetByIdAsync(int id) =>
     await _context.Partners
         .Include(p => p.service)  
         .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<string> UpdateAsync(int id, Partner entity)
        {
            var existing = await _context.Partners.FindAsync(id);
            if (existing == null) return "Not Found";
            existing.Name = entity.Name;
            existing.email = entity.email;
            existing.PhoneNumber = entity.PhoneNumber;
            existing.Password = entity.Password;
            existing.Location = entity.Location;
            existing.Description = entity.Description;
             existing.ServiceId = entity.ServiceId;
            
            existing.StartDate = entity.StartDate;
            await _context.SaveChangesAsync();
            return "Updated";
        }

        public async Task<List<Partner>> GetPartnersByServiceIdAsync(int serviceId)
        {
            return await _context.Partners
                .Where(p => p.ServiceId == serviceId)
                .Include(p => p.service) 
                .ToListAsync();
        }

    }
}
