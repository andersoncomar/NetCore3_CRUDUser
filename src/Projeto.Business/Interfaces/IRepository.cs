using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Projeto.Business.Models;

namespace Projeto.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Guid id);

        Task<IEnumerable<TEntity>> Find(FilterDefinition<TEntity> filter);
        Task<TEntity> GetId(Guid id);

        Task<List<TEntity>> GetAll();
        List<TEntity> GetAll(int page = 1);
    }
}
