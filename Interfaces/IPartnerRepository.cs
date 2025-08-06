using freelanceProjectEgypt03.Models;

namespace freelanceProjectEgypt03.Interfaces
{
    public interface IPartnerRepository : IRepository<Partner>
    {
        Task<List<Partner>> GetPartnersByServiceIdAsync(int serviceId);
    }
}
