using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Projeto.Business.Interfaces;
using Projeto.Business.Models;
using Projeto.Business.Models.Validations;

namespace Projeto.Business.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;

        public UserService(IUserRepository userRepository,
                            IProfileRepository profileRepository,
                            INotifier notifier): base(notifier)
        {
            this._userRepository = userRepository;
            this._profileRepository = profileRepository;
        }

        public async Task<bool> Add(User user)
        {
            if (!ExecuteValidation(new UserValidation(), user) ||
                !ExecuteValidation(new ProfileValidation(), user.Profile)) return false;

            var filter = new FilterDefinitionBuilder<Profile>().Lt(p => p.Documento, user.Profile.Documento);

            if (this._profileRepository.Find(filter).Result.Any())
            {
                Notifier("Já existe um User com este Documento informado.");
                return false;
            }

            if (!string.IsNullOrEmpty(user.Profile.Documento)) user.Profile.Id = Guid.NewGuid();

            await this._userRepository.Add(user);
            return true;
        }

        public async Task<bool> Update(User user)
        {
            if (!ExecuteValidation(new UserValidation(), user)) return false;

            var filter = new FilterDefinitionBuilder<Profile>().Lt(p => p.Documento, user.Profile.Documento);

            if (this._profileRepository.Find(filter).Result.Any())
            {
                Notifier("Já existe um User com este documento infomado.");
                return false;
            }

            await _userRepository.Update(user);
            return true;
        }

        public async Task UpdateProfile(Profile profile)
        {
            if (!ExecuteValidation(new ProfileValidation(), profile)) return;

            await this._profileRepository.Update(profile);
        }

        public async Task<bool> Delete(Guid id)
        {
            await this._userRepository.Delete(id);
            return true;
        }

        public void Dispose()
        {
            this._userRepository?.Dispose();
            this._profileRepository?.Dispose();
        }
    }
}
