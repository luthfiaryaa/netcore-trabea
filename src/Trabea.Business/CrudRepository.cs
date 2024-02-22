using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.Common;
using System.Transactions;
using Trabea.DataAccess.Models;

namespace Trabea.Business.Repositories {
    public class CrudRepository<TEntity, ID> : ICrudRepository<TEntity, ID> where TEntity : class{

        private readonly TrabeaContext _dbContext;
        private DbSet<TEntity> _table;

        public CrudRepository(TrabeaContext dbContext) {
            _dbContext = dbContext;
            _table = dbContext.Set<TEntity>();
        }

        public virtual List<TEntity> GetAll() {
            return _table.ToList();
        }

        public TEntity Delete(ID id) {
            TEntity existing = _table.Find(id) ??
                throw new KeyNotFoundException(EntityNotFoundMessage);
            _table.Remove(existing);
            _dbContext.SaveChanges();
            return existing;
        }

        public virtual TEntity GetById(ID id) {
            return _table.Find(id) ?? throw new KeyNotFoundException(EntityNotFoundMessage);
        }
        
        public TEntity Save(TEntity entity) {
            if (_table.Contains(entity)) {
                _table.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            else {
                _table.Add(entity);
            }
            _dbContext.SaveChanges();
            return entity;
        }

        public IDbContextTransaction BeginTransaction() {
            var transaction = _dbContext.Database.BeginTransaction();

            if (transaction == null) {
                throw new TransactionException("Transaction start failed");
            }

            return transaction;

        }

        protected virtual string EntityNotFoundMessage => "Entity not found with this ID";
    }
}
