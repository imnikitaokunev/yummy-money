namespace Domain.Common
{
    public abstract class Entity<T> : BaseEntity
    {
        public T Id { get; set; }
    }
}
