namespace Adwaer.DependencyInversion
{
    public interface IDiContainer
    {
        T Resolve<T>();
    }
}
