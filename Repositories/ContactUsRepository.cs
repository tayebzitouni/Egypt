using freelanceProjectEgypt03.Interfaces;
using freelanceProjectEgypt03.Models;
using freelanceProjectEgypt03.data;
using System;
using freelanceProjectEgypt03.data.freelanceProjectEgypt03.Data;
using Microsoft.EntityFrameworkCore;

namespace freelanceProjectEgypt03.Repositories
{
    public class ContactUsRepository : IRepository<ContactUs>
    {
        private readonly AppDbContext _context;

        public ContactUsRepository(AppDbContext context) => _context = context;

        public async Task<string> AddAsync(ContactUs entity)
        {
            _context.ContactUsMessages.Add(entity);
            await _context.SaveChangesAsync();
            return "Success";
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ContactUsMessages.FindAsync(id);
            if (entity == null) return false;
            _context.ContactUsMessages.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ContactUs>> GetAllAsync() => await _context.ContactUsMessages.ToListAsync();

        public async Task<ContactUs> GetByIdAsync(int id) => await _context.ContactUsMessages.FindAsync(id);

        public async Task<string> UpdateAsync(int id, ContactUs entity)
        {
            var existing = await _context.ContactUsMessages.FindAsync(id);
            if (existing == null) return "Not Found";
            existing.Name = entity.Name;
            existing.Email = entity.Email;
            existing.Phone = entity.Phone;
            existing.priorite = entity.priorite;
            existing.PreferedContactMethode = entity.PreferedContactMethode;
            existing.description = entity.description;
            existing.date = entity.date;
            existing.Subject = entity.Subject;
            await _context.SaveChangesAsync();
            return "Updated";
        }
    }
}

