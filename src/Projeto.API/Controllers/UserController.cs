using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Projeto.API.ViewModels;
using Projeto.Business.Interfaces;
using Projeto.Business.Models;

namespace Projeto.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : MainController
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository,
                              IProfileRepository profileRepository,
                              IUserService userService,
                              IMapper mapper,
                              INotifier notifier): base(notifier)
        {
            this._userRepository = userRepository;
            this._profileRepository = profileRepository;
            this._userService = userService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Buscar todos os Usuários Cadastrados.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<UserViewModel>>(await _userRepository.GetAll());
        }

        /// <summary>
        /// Buscar todos os Usuários Cadastrados por Página. Por padrão sempre é 1.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("perpages")]
        [HttpGet]
        public IEnumerable<UserViewModel> GetAllPaginate([FromQuery(Name = "page")] int page)
        {
            if (page  == 0)
                page = 1;

            return _mapper.Map<IEnumerable<UserViewModel>>(_userRepository.GetAll(page));
        }

        /// <summary>
        /// Buscar Usuário.
        /// </summary>
        /// <param name="id">Campo em Formato GUID</param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserViewModel>> GetId(Guid id)
        {
            var userViewModel = await GetUser(id);

            if (userViewModel == null) return NotFound();

            return userViewModel;
        }

        /// <summary>
        /// Adiciona novo Usuário.
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserViewModel>> Add(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userService.Add(_mapper.Map<User>(userViewModel));

            return CustomResponse(userViewModel);
        }

        /// <summary>
        /// Altera Usuário se existente
        /// </summary>
        /// <param name="id">Campo em Formato GUID</param>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserViewModel>> Update(Guid id, UserViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                NotifierError("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(userViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userService.Update(_mapper.Map<User>(userViewModel));

            return CustomResponse(userViewModel);
        }

        /// <summary>
        /// Deleta Usuário se existente
        /// </summary>
        /// <param name="id">Campo em Formato GUID</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UserViewModel>> Delete(Guid id)
        {
            var userViewModel = await GetUser(id);

            if (userViewModel == null) return NotFound();

            await _userService.Delete(id);

            return CustomResponse(userViewModel);
        }
       
        private async Task<UserViewModel> GetUser(Guid id)
        {
            return _mapper.Map<UserViewModel>(await _userRepository.GetId(id));
        }
    }
}
