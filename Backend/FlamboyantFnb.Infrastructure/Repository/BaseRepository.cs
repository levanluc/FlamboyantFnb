using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using FlamboyantFnb.Domain.Entities;
using FlamboyantFnb.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

//using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlamboyantFnb.Infrastructure.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly FnbDbContext _context;
        public BaseRepository(FnbDbContext context)
        {
            _context = context;
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
            return _context.Set<T>();
        }

        public virtual IQueryable<T> GetAllActive()
        {
            return _context.Set<T>().Where(e => !e.IsDeleted);
        }

        public virtual IQueryable<T> GetByIdActive(int id)
        {
            return _context.Set<T>().Where(e => e.Id == id && !e.IsDeleted);
        }

        public virtual Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            // Scan all items
            return _context.Set<T>().ToListAsync();
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
