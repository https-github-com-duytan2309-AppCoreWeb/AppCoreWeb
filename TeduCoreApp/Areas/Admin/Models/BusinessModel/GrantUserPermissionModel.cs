using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Areas.Admin.Models.BusinessModel
{
    public class GrantUserPermissionModel
    {
        public IEnumerable<AppUserActions> ListGrant { get; set; }
        public AppUserActions Grant { get; set; }
    }
}