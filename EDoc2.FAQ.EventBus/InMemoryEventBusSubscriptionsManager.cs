using EDoc2.FAQ.EventBus.Abstractions;
using EDoc2.FAQ.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.EventBus
{
    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }

        public bool IsEmpty => !_handlers.Keys.Any();

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            if (!HasSubscriptionsForEvent(eventName))
                _handlers.Add(eventName, new List<SubscriptionInfo>());

            var handlerType = typeof(TH);
            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
                throw new InvalidOperationException($"Handler Type {handlerType.Name} already registered for '{eventName}'");

            _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
            _eventTypes.Add(typeof(T));
        }

        public void Clear() => _handlers.Clear();

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            var eventName = GetEventKey<T>();
            return GetHandlersForEvent(eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
        {
            return _handlers[eventName];
        }

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent => HasSubscriptionsForEvent(GetEventKey<T>());

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var handlerToRemove = FindSubscription<T, TH>();
            if (handlerToRemove == null) return;

            RemoveSubscription<T>(handlerToRemove);
        }

        #region privates

        private SubscriptionInfo FindSubscription<T, TH>()
        {
            var eventName = GetEventKey<T>();
            var handlerType = typeof(TH);

            if (!_handlers.ContainsKey(eventName)) return null;
            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }

        private void RemoveSubscription<T>(SubscriptionInfo subscription)
        {
            if (subscription == null) throw new ArgumentNullException(nameof(subscription));

            var eventName = GetEventKey<T>();
            if (!_handlers.ContainsKey(eventName)) return;

            _handlers[eventName].Remove(subscription);

            if (_handlers[eventName].Any()) return;

            _handlers.Remove(eventName);
            var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
            if (eventType != null) _eventTypes.Remove(eventType);

            RaiseOnEventRemoved(eventName);
        }

        private void RaiseOnEventRemoved(string eventName)
        {
            OnEventRemoved?.Invoke(this, eventName);
        }

        #endregion
    }
}
