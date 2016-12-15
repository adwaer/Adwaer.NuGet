using System.Collections.Generic;

namespace Adwaer.Identity
{
    public interface IGetRolesAction<in T>
    {
        IList<string> Execute(T user);
    }
}