namespace EDoc2.FAQ.Core.Domain.SeedWork
{
    public abstract class Entity<TPrimaryKey> 
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}
