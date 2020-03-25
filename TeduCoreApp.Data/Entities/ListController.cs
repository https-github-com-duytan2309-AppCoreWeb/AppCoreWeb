using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ListController")]
    public class ListController : DomainEntity<long>
    {
        public ListController()
        {
        }

        public ListController(long id, string controllerName, string discription, string discriptionAction)
        {
            Id = id;
            ControllerName = controllerName;
            Discription = discription;
            DiscriptionAction = discriptionAction;
        }

        public string ControllerName { set; get; }
        public string Discription { set; get; }

        public string DiscriptionAction { set; get; }

        public virtual ICollection<ListAction> ListActions { get; set; }
    }
}