namespace Domain.Common.Interfaces
{
    public interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
