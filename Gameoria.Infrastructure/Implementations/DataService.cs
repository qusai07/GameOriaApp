
using GameOria.Api.Repo.Interface;
using GameOria.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameOria.Infrastructure.Implementations
{
    public class DataService:IDataService
    {
        private readonly GameOriaDbContext _context;

        public DataService(GameOriaDbContext context)
        {
            _context = context;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(object key) where T : class
        {
            return await _context.Set<T>().FindAsync(key);
        }

        public async Task<T> CreateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync<T>(object key) where T : class
        {
            var entity = await _context.Set<T>().FindAsync(key);
            if (entity == null)
                return false;

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return _context.Set<T>().AsQueryable();
        }


        public IQueryable<T> GetQuery<T>() where T : class
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
