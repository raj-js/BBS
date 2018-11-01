using System.Threading.Tasks;

namespace EDoc2.FAQ.Notification
{
    public interface INotifyHandler<T> where T : Notify
    {
        Task Handle(T notify);
    }
}
