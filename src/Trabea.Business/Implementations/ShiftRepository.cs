using Trabea.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabea.Business.Interfaces;
using Trabea.DataAccess.Models;

namespace Trabea.Business.Implementations {
    public class ShiftRepository : CrudRepository<Shift, long>, IShiftRepository{
        private readonly TrabeaContext _dbContext;
        public ShiftRepository(TrabeaContext dbContext) : base(dbContext) {
            _dbContext = dbContext;
        }
    }
}
