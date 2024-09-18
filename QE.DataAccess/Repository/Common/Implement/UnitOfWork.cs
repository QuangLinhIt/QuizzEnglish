using Microsoft.EntityFrameworkCore.Storage;
using QE.DataAccess.Context;
using QE.DataAccess.Repository.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QE.DataAccess.Repository.Common.Implement
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private IDbContextTransaction _dbTransaction;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task BeginTransactionAsync()
        {
            _dbTransaction = await _applicationDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                if(_dbTransaction != null)
                {
                    await _dbTransaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                await _dbTransaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _dbTransaction.DisposeAsync();
                _dbTransaction = null!;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbTransaction.RollbackAsync();
            await _dbTransaction.DisposeAsync();
            _dbTransaction = null!;
        }

        public async Task<int> SaveChangesTransactionAsync()
        {
            return await _applicationDbContext.SaveChangesAsync();
        }

    }
}
