using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Areas.Admin.Models.ManagerRole
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public bool IsSelected { get; set; }
    }
}