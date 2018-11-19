using MediatR;
using System.Collections.Generic;

namespace EDoc2.FAQ.Core.Domain.SeedWork
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }

        private List<INotification> _domainEvents;

        /// <summary>
        /// 领域事件
        /// 使用 MediatR（内存级中介者） 实现
        /// </summary>
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification @event)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(@event);
        }

        public void RemoveDomainEvent(INotification @event) => _domainEvents?.Remove(@event);

        public void ClearDomainEvent() => _domainEvents?.Clear();

        public virtual bool IsTransient()
        {
            return Id.Equals(default(int));
        }
    }

    public abstract class Entity<TKey> : Entity
    {
        /// <summary>
        /// 将Id的默认类型（int）变为用户指定
        /// </summary>
        public new TKey Id { get; set; }

        public override bool IsTransient()
        {
            return Id.Equals(default(TKey));
        }
    }
}
