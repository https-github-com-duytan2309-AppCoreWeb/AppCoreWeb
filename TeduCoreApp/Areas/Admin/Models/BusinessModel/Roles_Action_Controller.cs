using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduCoreApp.Data.EF;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Areas.Admin.Models.BusinessModel
{
    public class Roles_Action_Controller
    {
        private readonly AppDbContext _context;

        public IEnumerable<ListAction> listAction { get; set; }

        public IEnumerable<ListController> listController { get; set; }

        public ListAction Action { get; set; }

        public ListController Controller { get; set; }

        public IEnumerable<AppRole> listRoles { get; set; }

        public RoleViewModel listRoleViewModel { get; set; }
    }
}