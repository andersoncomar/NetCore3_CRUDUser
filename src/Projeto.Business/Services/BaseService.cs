using FluentValidation;
using FluentValidation.Results;
using Projeto.Business.Interfaces;
using Projeto.Business.Models;
using Projeto.Business.Notifications;

namespace Projeto.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        public BaseService(INotifier notifier)
        {
            this._notifier = notifier;
        }

        protected void Notifier(ValidationResult validationResult)
        {
            foreach (ValidationFailure error in validationResult.Errors)
            {
                this.Notifier(error.ErrorMessage);
            }
        }

        protected void Notifier(string message)
        {
            this._notifier.Handle(new Notification(message));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV: AbstractValidator<TE> where TE: Entity
        {
            ValidationResult validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            this.Notifier(validator);

            return false;
        }
    }
}
