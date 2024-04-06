using MovieList.DAL.Interfaces;
using MovieList.DAL.Repository;
using System.Transactions;

namespace MovieList.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {     
        private readonly ApplicationDbContext _dbContext;
        private Dictionary<Type, object>? _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            _repositories ??= new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_dbContext);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public int SaveChanges() => _dbContext.SaveChanges();

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();

        public TransactionScope BeginTransaction() => new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {                        
                    _repositories?.Clear();
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }      
    }
}
