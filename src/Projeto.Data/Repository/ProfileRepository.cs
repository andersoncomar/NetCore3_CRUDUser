using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using Projeto.Business.Interfaces;
using Projeto.Business.Models;
using Projeto.Data.Context;

namespace Projeto.Data.Repository
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(ProjetoContext projetoContext): base(projetoContext) { }
    }
}
