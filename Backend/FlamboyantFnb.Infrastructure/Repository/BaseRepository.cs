using FlamboyantFnb.Domain.Context;
using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.Interfaces;
using FlamboyantFnb.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

//using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlamboyantFnb.Infrastructure.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly FnbDbContext _context;
        protected readonly FnbExecutionContext _executionContext;
        public BaseRepository(FnbDbContext context, FnbExecutionContext executionContext)
        {
            _context = context;
            _executionContext = executionContext;
        }
        public virtual async Task<T> AddAsync(T entity, bool flush = true, CancellationToken cancellationToken = default)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsDeleted = false;
            var rs = _context.Add(entity);
            if (flush)
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            return rs.Entity;
        }

        public virtual IQueryable<T> GetAll()
        {
            var query = _context.Set<T>().Where(e => true);
            if (typeof(T) is IMerchantId)
            {
                query = query.Where(e => ((IMerchantId)e).MerchantId == _executionContext.MerchantId);
            }
            return query;
        }

        public virtual IQueryable<T> GetAllActive()
        {
            var query = _context.Set<T>().Where(e => !e.IsDeleted);
            if (typeof(T) is IMerchantId)
            {
                query = query.Where(e => ((IMerchantId)e).MerchantId == _executionContext.MerchantId);
            }
            return query;
        }

        public virtual IQueryable<T> GetByIdActive(int id)
        {
            return _context.Set<T>().Where(e => e.Id == id && !e.IsDeleted);
        }

        public virtual Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = _context.Set<T>().Where(e => true);
            if (typeof(T) is IMerchantId)
            {
                query = query.Where(e => ((IMerchantId)e).MerchantId == _executionContext.MerchantId);
            }
            return query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> UpdateAsync(T entity, bool flush = true, CancellationToken cancellationToken = default)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            _context.Update(entity);
            if (flush)
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            return entity;
        }

        public virtual async Task<T> DeleteAsync(T entity, bool flush = true, CancellationToken cancellationToken = default)
        {
            entity.IsDeleted = true;
            _context.Update(entity);
            if (flush)
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            return entity;
        }
    }
}
