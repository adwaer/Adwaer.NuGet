using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using SimpleCustomerAccount = Adwaer.Identity.Entitites.SimpleCustomerAccount;

namespace Adwaer.Identity
{
    public class IdentityUserManager : UserManager<SimpleCustomerAccount, Guid>
    {
        public IdentityUserManager(IUserStore<SimpleCustomerAccount, Guid> store) 
            : base(store)
        {
            UserTokenProvider = new TotpSecurityStampBasedTokenProvider<SimpleCustomerAccount, Guid>();
            UserValidator = new UserValidator<SimpleCustomerAccount, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }
        
        public static IdentityUserManager Get(DbContext dncontext)
        {
            return new IdentityUserManager(new IdentityUserStore(dncontext));
        }
    }
}
