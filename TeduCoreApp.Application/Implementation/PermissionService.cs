using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.System;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Application.Implementation
{
    public class PermissionService : IPermissionService
    {
        private IPermissionRepository _permissionRepository;

        private PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public List<Permission> GetAllPermisions()
        {
            return _permissionRepository.FindAll().ToList();
        }
    }
}