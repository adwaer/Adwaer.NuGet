using System.Collections.Generic;
using Adwaer.Entity;

namespace Adwaer.Identity.Entitites
{
    public class Customer : EntityBase<int>
    {
        public string DisplayName { get; set; }
        public ICollection<SimpleCustomerAccount> SimpleCutomerAccounts { get; set; }
    }
}
