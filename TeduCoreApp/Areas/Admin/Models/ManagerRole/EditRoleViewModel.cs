using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeduCoreApp.Areas.Admin.Models.ManagerRole
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            Users = new List<string>();
            //NonUsers = new List<string>();
        }

        public string Id { get; set; }

        public string RoleName { get; set; }

        public List<string> Users { get; set; }

        //public List<string> NonUsers { get; set; }
    }
}