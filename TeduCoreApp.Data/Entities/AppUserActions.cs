using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("AppUserActions")]
    public class AppUserActions : DomainEntity<long>
    {
        public AppUserActions()
        {
        }

        public AppUserActions(Guid idUser, long idAction, string discription, bool isAllowed)
        {
            IdUser = idUser;
            IdAction = idAction;
            Description = discription;
            IsAllowed = isAllowed;
        }

        public Guid IdUser { set; get; }
        public long IdAction { set; get; }
        public string Description { set; get; }
        public bool IsAllowed { set; get; }
    }
}