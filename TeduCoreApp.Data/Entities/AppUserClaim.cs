using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeduCoreApp.Data.Entities
{
    [Table("AppUserClaims")]
    public class AppUserClaim : IdentityUserClaim<Guid>
    {
        public AppUserClaim() : base()
        {
        }

        public virtual AppUser AppUser { get; set; }
    }
}