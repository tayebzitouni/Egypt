using freelanceProjectEgypt03.Models;

namespace freelanceProjectEgypt03.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<string> AddAsync(T entity);
        Task<string> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);


    }
}
