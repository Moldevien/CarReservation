using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>> GetFilteredAsync(CarFilter filter, bool isAdmin);
    }
}
