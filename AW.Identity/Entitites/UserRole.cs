using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Adwaer.Entity;

namespace Adwaer.Identity.Entitites
{
    public class UserRole : IdentityUserRole<Guid>, IEntity<Guid>
    {
        public string GetId()
        {
            return Id.ToString();
        }

        public Guid Id { get; set; }
        public SimpleCustomerAccount User { get; set; }
    }
}
