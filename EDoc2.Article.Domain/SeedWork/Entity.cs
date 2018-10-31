using System;
using System.Collections.Generic;

namespace EDoc2.Article.Domain.SeedWork
{
    public abstract class Entity
    {
        private int? _requestHashCode;
        private int _id;

        public virtual int Id
        {
            get => _id;
            protected set => _id = value;
        }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(INotification domainEvent) => _domainEvents?.Remove(domainEvent);

        public void ClearDomainEvents() => _domainEvents?.Clear();

        public bool IsTransient() => Id == default(int);

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (Entity) obj;
            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (IsTransient()) return base.GetHashCode();

            if (_requestHashCode.HasValue)
                _requestHashCode = Id.GetHashCode() ^ 31;

            return _requestHashCode.Value;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Equals(left, null))
                return Equals(right, null);

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
