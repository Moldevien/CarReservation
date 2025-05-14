using Domain.Interfaces;

namespace Application.Services
{
    public class Service<T> : IService<T> where T : class
    {
        public readonly IRepository<T> _Repository;

        public Service(IRepository<T> Repository) => _Repository = Repository;

        public async Task<IEnumerable<T>> GetAllAsync() => await _Repository.GetAllAsync();

        public async Task<T?> GetByIdAsync(int id) => await _Repository.GetByIdAsync(id);

        public async Task AddAsync(T entity) => await _Repository.AddAsync(entity);

        public async Task DeleteAsync(int id) => await _Repository.DeleteAsync(id);

        public async Task UpdateAsync(T entity) => await _Repository.UpdateAsync(entity);
    }
}