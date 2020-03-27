using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Prueba.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiRest_Personas.Repository
{
    public abstract class RepositoryBase<T> :IRepositoryBase<T> where T : class
    {
        protected MasterContext _repositoryContext { get; set; }

        public RepositoryBase(MasterContext repositoryContext)
        {
            this._repositoryContext = repositoryContext;
        }

        public async Task<List<T>> FindAll()
        {
            return await _repositoryContext.Set<T>().ToListAsync();
        }

        public async Task<EntityEntry<T>> Create(T entity)
        {
            var result = await _repositoryContext.Set<T>().AddAsync(entity);
            await _repositoryContext.SaveChangesAsync();
            return result;
        }

        public async Task<EntityEntry<T>> Update(T entity)
        {
            _repositoryContext.Entry(entity).State = EntityState.Modified;

            try
            {
                var user = await _repositoryContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return _repositoryContext.Set<T>().Update(entity);
        }

        public async Task<EntityEntry<T>> Delete(T entity)
        {
            var result = _repositoryContext.Set<T>().Remove(entity);
            await _repositoryContext.SaveChangesAsync();

            return result;
        }

        public async Task<ValueTask<T>> GetById(long id)
        {
            return  _repositoryContext.Set<T>().FindAsync(id);
        }
    }
}
