using System;
using EDoc2.Article.Domain.SeedWork;

namespace EDoc2.Article.Domain.AggregatesModel.CategoryAggregate
{
    public class Category : Entity, IAggregateRoot
    {
        public string IdentityGuid { get; private set; }
        public string Name { get; private set; }

        public Category(string identity, string name)
        {
            IdentityGuid = !string.IsNullOrWhiteSpace(identity) ? identity : throw new ArgumentNullException(nameof(identity));
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
        }
    }
}
