namespace Adwaer.Identity
{
    public interface IOnCreateUserAction<in T>
    {
        void Execute(T user);
    }
}
