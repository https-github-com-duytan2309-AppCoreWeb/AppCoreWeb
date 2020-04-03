using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeduCoreApp.Data.Entities
{
    [Table("AppUserRoles")]
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public AppUserRole() : base()
        {
        }

        public AppUserRole(string discriminator) : base()
        {
            Discriminator = discriminator;
        }

        [StringLength(128)]
        public string Discriminator { get; set; }

        public virtual AppRole AppRoles { get; set; }

        public virtual AppUser AppUsers { get; set; }
    }
}