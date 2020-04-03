using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.System;

namespace TeduCoreApp.Application.Implementation
{
    public class UserRolesService : IUserRolesService
    {
        public Task<List<AppRoleViewModel>> CheckPermissionByIdUser(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppRoleViewModel>> GetAllAsync()
        {
            //return await _userManager.A.ProjectTo<AppUserViewModel>().ToListAsync();
            throw new NotImplementedException();
        }
    }
}