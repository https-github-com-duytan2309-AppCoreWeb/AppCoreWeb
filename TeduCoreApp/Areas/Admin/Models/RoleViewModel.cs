using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public virtual AppRole AppRole { get; set; }
        //public virtual IEnumerable<AppUserRoles> AppUserRoles { get; set; }
    }
}