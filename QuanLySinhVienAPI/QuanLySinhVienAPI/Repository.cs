using QuanLySinhVienAPI.Entities;

namespace QuanLySinhVienAPI
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id);

    }
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public readonly AppDBContext _context;
        public Repository(AppDBContext context)
        {
            _context = context;
        }
        public async Task Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> GetById(int id)
        {
            var irepository = await _context.Set<TEntity>().FindAsync(id);
            if (irepository == null) throw new Exception("Null");
            return irepository;
        }

        public async Task Update(TEntity entity)
        {
           _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
