namespace Adwaer.Entity
{
    public interface IEntity
    {
    }

    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}