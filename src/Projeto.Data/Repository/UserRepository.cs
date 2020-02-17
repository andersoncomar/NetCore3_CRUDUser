using Projeto.Business.Interfaces;
using Projeto.Business.Models;
using Projeto.Data.Context;

namespace Projeto.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ProjetoContext projetoContext) : base(projetoContext) { }
    }
}
