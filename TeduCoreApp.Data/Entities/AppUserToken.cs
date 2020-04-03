using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeduCoreApp.Data.Entities
{
    [Table("AppUserTokens")]
    public class AppUserToken : IdentityUserToken<Guid>
    {
        public AppUserToken() : base()
        {
        }
    }
}