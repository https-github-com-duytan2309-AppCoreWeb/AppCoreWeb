using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Areas.Admin.Models.ManagerRole
{
    public class UserClaimsViewModel
    {
        public UserClaimsViewModel()
        {
            Cliams = new List<UserClaims>();
        }

        public string UserId { get; set; }

        public List<UserClaims> Cliams { get; set; }
    }
}