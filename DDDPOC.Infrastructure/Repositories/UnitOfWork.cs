using DDDPOC.Infrastructure.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDPOC.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Members

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

        }

        #endregion

        #region IUnitOfWork Members     
        public async Task<int> SaveChangesAsync()
        {
            //SetIEntityFields();

            return await _context.SaveChangesAsync();
        }
        public int SaveChanges()
        {
            //SetIEntityFields();
            return _context.SaveChanges();
        }

        #endregion

        #region Private Methods



        #endregion
    }
}
