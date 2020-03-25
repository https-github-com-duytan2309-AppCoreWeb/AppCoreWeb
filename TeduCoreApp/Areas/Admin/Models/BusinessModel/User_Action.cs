using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Areas.Admin.Models.BusinessModel
{
    public class User_Action
    {
        public IEnumerable<ListAction> listAction { get; set; }
        public IEnumerable<AppUser> listUser { get; set; }
        public ListAction Action { get; set; }
        public AppUser User { get; set; }
        public AppUserActions AppUserActions { get; set; }
    }
}