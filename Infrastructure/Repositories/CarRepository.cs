using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        public readonly ApplicationDbContext _context;
        private readonly DbSet<Car> _dbSet;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Car>();
        }

        public async Task<IEnumerable<Car>> GetFilteredAsync(CarFilter filter, bool isAdmin)
        {
            var query = _context.Cars.AsQueryable();

            if (!isAdmin)
                query = query.Where(c => c.IsAvailable);

            if (!string.IsNullOrEmpty(filter.Brand))
                query = query.Where(c => c.Brand.Contains(filter.Brand));
            if (!string.IsNullOrEmpty(filter.Model))
                query = query.Where(c => c.Model.Contains(filter.Model));
            if (filter.MinPrice.HasValue)
                query = query.Where(c => c.PricePerDay >= filter.MinPrice);
            if (filter.MaxPrice.HasValue)
                query = query.Where(c => c.PricePerDay <= filter.MaxPrice);
            if (!string.IsNullOrEmpty(filter.Gas))
                query = query.Where(c => c.Gas.Contains(filter.Gas));
            if (filter.Passengers.HasValue)
                query = query.Where(c => c.Passengers >= filter.Passengers);
            if (filter.Volume.HasValue)
                query = query.Where(c => c.Volume >= filter.Volume);
            if (!string.IsNullOrEmpty(filter.Gear_box))
                query = query.Where(c => c.Gear_box.Contains(filter.Gear_box));

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<Car?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(Car entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Car entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
