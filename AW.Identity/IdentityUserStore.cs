using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Adwaer.Identity.Entitites;
using SimpleCustomerAccount = Adwaer.Identity.Entitites.SimpleCustomerAccount;

namespace Adwaer.Identity
{
    public class IdentityUserStore :
        UserStore
            <SimpleCustomerAccount, IdentityRole<Guid, UserRole>, Guid, IdentityUserLogin<Guid>, UserRole,
                IdentityUserClaim<Guid>>
    {
        public IdentityUserStore(DbContext ctx) : base(ctx)
        {
        }

        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="user"/>
        public override Task CreateAsync(SimpleCustomerAccount user)
        {
            if (user.Customer == null)
            {
                user.Customer = new Customer
                {
                    DisplayName = user.UserName
                };
            }

            Context
                .Set<SimpleCustomerAccount>()
                .Add(user);

            //if (!user.Roles.Any())
            //{
            //    user.Roles.Add(new UserRole
            //    {
            //        RoleId = Guid.Empty
            //    });
            //}

            Context.Entry(user).State = EntityState.Added;
            return Context.SaveChangesAsync();
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="user"/>
        public override Task UpdateAsync(SimpleCustomerAccount user)
        {
            return Context.SaveChangesAsync();
        }

        /// <summary>
        /// Mark an entity for deletion
        /// </summary>
        /// <param name="user"/>
        public override Task DeleteAsync(SimpleCustomerAccount user)
        {
            throw new NotImplementedException();
            //user.IsDeleted = true;
            user.UserName = $"{user.UserName}_{user.Id}";

            return UpdateAsync(user);
        }

        /// <summary>
        /// Find a user by id
        /// </summary>
        /// <param name="userId"/>
        /// <returns/>
        public override async Task<SimpleCustomerAccount> FindByIdAsync(Guid userId)
        {
            SimpleCustomerAccount user =
                (await Context.Set<SimpleCustomerAccount>().FirstOrDefaultAsync(u => u.Id == userId));
            return user;
        }

        /// <summary>
        /// Find a user by name
        /// </summary>
        /// <param name="userName"/>
        /// <returns/>
        public override async Task<SimpleCustomerAccount> FindByNameAsync(string userName)
        {
            return await Context
                .Set<SimpleCustomerAccount>()
                .FirstOrDefaultAsync(ac => ac.Email == userName);
        }

        public override Task<IList<string>> GetRolesAsync(SimpleCustomerAccount user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            IList<string> roles = new string[0];
            return Task.FromResult(roles);
        }

        public override Task<IList<Claim>> GetClaimsAsync(SimpleCustomerAccount user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            IList<Claim> claims = new Claim[0];
            return Task.FromResult(claims);
        }
    }
}
