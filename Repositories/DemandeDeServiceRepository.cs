using freelanceProjectEgypt03.Interfaces;
using freelanceProjectEgypt03.Models;
using freelanceProjectEgypt03.data;

using System;
using freelanceProjectEgypt03.data.freelanceProjectEgypt03.Data;
using Microsoft.EntityFrameworkCore;

namespace freelanceProjectEgypt03.Repositories
{
    public class DemandeDeServiceRepository : IRepository<DemandeDeService>
    {
        private readonly AppDbContext _context;

        public DemandeDeServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddAsync(DemandeDeService entity)
        {
            _context.DemandeDeServices.Add(entity);
            await _context.SaveChangesAsync();
            return "Success";
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.DemandeDeServices.FindAsync(id);
            if (entity == null) return false;

            _context.DemandeDeServices.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<DemandeDeService>> GetAllAsync()
        {
            return await _context.DemandeDeServices
                .Include(d => d.client)
                .Include(d => d.service)
                .ToListAsync();
        }

        public async Task<DemandeDeService> GetByIdAsync(int id)
        {
            return await _context.DemandeDeServices
                .Include(d => d.client)
                .Include(d => d.service)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<string> UpdateAsync(int id, DemandeDeService entity)
        {
            var existing = await _context.DemandeDeServices.FindAsync(id);
            if (existing == null) return "Not Found";

            // Update fields
            existing.date = entity.date;
            existing.priorite = entity.priorite;
            existing.PreferedContactMethode = entity.PreferedContactMethode;
            existing.description = entity.description;
            existing.preferedDateAndTime = entity.preferedDateAndTime;
            existing.location = entity.location;
            existing.budget = entity.budget;
            existing.AdditionalServices = entity.AdditionalServices;
            existing.ClientId = entity.ClientId;
            existing.ServiceId = entity.ServiceId;

            await _context.SaveChangesAsync();
            return "Updated";
        }
    }
}


