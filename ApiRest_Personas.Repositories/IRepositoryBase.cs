using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiRest_Personas.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<List<T>> FindAll();
        Task<EntityEntry<T>> Create(T entity);
        Task<EntityEntry<T>> Update(T entity);
        Task<EntityEntry<T>> Delete(T entity);
        Task<ValueTask<T>> GetById(long id);
    }
}
