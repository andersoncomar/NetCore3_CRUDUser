using System;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using System.Linq;

namespace Projeto.Data.Context
{
    public class ProjetoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient client { get; set; }
        private readonly List<Func<Task>> _commands;
        private readonly IConfiguration _configuration;

        public ProjetoContext(IConfiguration configuration)
        {
            _configuration = configuration;

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();
        }

        public async Task<int> SaveChanges()
        {
            ConfigureMongo();

            using (Session = await client.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        private void ConfigureMongo()
        {
            if (client != null)
                return;

            // Configure mongo (You can inject the config, just to simplify)
            client = new MongoClient(_configuration["MongoSettings:Connection"]);

            Database = client.GetDatabase(_configuration["MongoSettings:DatabaseName"]);

        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();
            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }
    }
}
