using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using Projeto.Business.Models;

namespace Projeto.Business.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<bool> Add(User user);
        Task<bool> Update(User user);
        Task<bool> Delete(Guid id);
    }
}
