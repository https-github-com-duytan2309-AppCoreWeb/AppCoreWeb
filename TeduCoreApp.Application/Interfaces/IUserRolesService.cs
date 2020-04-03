using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.System;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IUserRolesService
    {
        Task<List<AppRoleViewModel>> GetAllAsync();

        Task<List<AppRoleViewModel>> CheckPermissionByIdUser(Guid UserId);
    }
}