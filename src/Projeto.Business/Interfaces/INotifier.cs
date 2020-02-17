using System.Collections.Generic;
using Projeto.Business.Notifications;

namespace Projeto.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
