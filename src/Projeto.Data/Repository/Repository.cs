using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Projeto.Business.Interfaces;
using Projeto.Business.Models;
using Projeto.Data.Context;

namespace Projeto.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity: Entity, new()
    {
        protected readonly ProjetoContext _projetoContext;
        protected IMongoCollection<TEntity> DbSet;

        public Repository(ProjetoContext projetoContext)
        {
            this._projetoContext = projetoContext;
        }

        public Task Add(TEntity entity)
        {
            ConfigDbSet();
            return DbSet.InsertOneAsync(entity);
        }

        public Task Delete(Guid id)
        {
            ConfigDbSet();
            return DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
        }

        public void Dispose()
        {
            _projetoContext?.Dispose();
        }

        public async Task<IEnumerable<TEntity>> Find(FilterDefinition<TEntity> filter)
        {
            ConfigDbSet();
            var all = await DbSet.FindAsync(filter);
            return all.ToList();
        }

        public async Task<List<TEntity>> GetAll()
        {
            ConfigDbSet();
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public List<TEntity> GetAll(int page)
        {
            ConfigDbSet();

            int page_size = 3;
            int skips = page_size * (page - 1);


            var all = DbSet.Find(Builders<TEntity>.Filter.Empty).Skip(skips).Limit(page_size);

            return all.ToList();
        }

        public async Task<TEntity> GetId(Guid id)
        {
            ConfigDbSet();
            
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }

        public Task Update(TEntity entity)
        {
            ConfigDbSet();
            return DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id), entity);
        }

        private void ConfigDbSet()
        {
            DbSet = _projetoContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}
