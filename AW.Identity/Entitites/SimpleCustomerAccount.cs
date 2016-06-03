using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Adwaer.Identity.Entitites
{
    public class SimpleCustomerAccount : IdentityUser<Guid, IdentityUserLogin<Guid>, UserRole, IdentityUserClaim<Guid>>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }

        [Index("IX_Unique_UserName", 1, IsUnique = true), MaxLength(255)]
        public override string UserName { get; set; }
        public Customer Customer { get; set; }
    }
}
