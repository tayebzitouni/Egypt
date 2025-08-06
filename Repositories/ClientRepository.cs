using freelanceProjectEgypt03.Interfaces;
using freelanceProjectEgypt03.Models;
using freelanceProjectEgypt03.data;
using System;
using freelanceProjectEgypt03.data.freelanceProjectEgypt03.Data;
using Microsoft.EntityFrameworkCore;

namespace freelanceProjectEgypt03.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context) => _context = context;

        public async Task<string> AddAsync(Client entity)
        {
            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);

            _context.Clients.Add(entity);
            await _context.SaveChangesAsync();
            return "Success";
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Clients.FindAsync(id);
            if (entity == null) return false;
            _context.Clients.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Client>> GetAllAsync() => await _context.Clients.ToListAsync();

        public async Task<Client> GetByIdAsync(int id) => await _context.Clients.FindAsync(id);

        public async Task<string> UpdateAsync(int id, Client entity)
        {
            var existing = await _context.Clients.FindAsync(id);
            if (existing == null) return "Not Found";
            existing.Name = entity.Name;
            existing.PhoneNumber = entity.PhoneNumber;
            existing.email = entity.email;
            existing.Password = entity.Password;
            existing.StartDate = entity.StartDate;
            await _context.SaveChangesAsync();
            return "Updated";
        }
    }
}

