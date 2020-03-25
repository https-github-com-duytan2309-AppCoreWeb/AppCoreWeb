using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ListAction")]
    public class ListAction : DomainEntity<long>
    {
        public ListAction()
        {
            AspNetUserActions = new List<AppUserActions>();
        }

        public ListAction(long id, string controllerName, string discription, long idController)
        {
            Id = id;
            ActionName = controllerName;
            Discription = discription;
            IdController = idController;
        }

        public string ActionName { set; get; }
        public string Discription { set; get; }

        public long IdController { set; get; }

        [ForeignKey("IdController")]
        public ListController ListController { get; set; }

        public virtual ICollection<AppUserActions> AspNetUserActions { get; set; }
    }
}