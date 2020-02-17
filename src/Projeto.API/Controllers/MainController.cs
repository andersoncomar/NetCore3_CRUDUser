using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Projeto.Business.Interfaces;
using Projeto.Business.Notifications;

namespace Projeto.API.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        public MainController(INotifier notifier)
        {
            this._notifier = notifier;
        }

        protected bool OperationValid()
        {
            return !_notifier.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifierErrorModelInvalid(modelState);
            return CustomResponse();
        }

        protected void NotifierErrorModelInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifierError(errorMsg);
            }
        }

        protected void NotifierError(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }
    }
}
