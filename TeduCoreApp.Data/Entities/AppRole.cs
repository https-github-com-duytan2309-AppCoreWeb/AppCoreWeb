using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeduCoreApp.Data.Entities
{
    [Table("AppRoles")]
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() : base()
        {
            AppUserRoles = new HashSet<AppUserRole>();
        }

        public AppRole(string roleName) : base(roleName)
        {
            Name = roleName;
        }

        public AppRole(string name, string description) : base(name)
        {
            Description = description;
        }

        [StringLength(250)]
        public string Description { get; set; }

        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}