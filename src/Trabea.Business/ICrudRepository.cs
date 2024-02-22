using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabea.Business.Repositories
{
    public interface ICrudRepository<TEntity, ID>
    {
        List<TEntity> GetAll();
        TEntity GetById(ID id);
        TEntity Save(TEntity entity);
        TEntity Delete(ID id);
        IDbContextTransaction BeginTransaction();
    }
}
