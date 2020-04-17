using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IPermissionService
    {
        List<Permission> GetAllPermisions();
    }
}