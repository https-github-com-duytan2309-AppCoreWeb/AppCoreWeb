using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Data.EF.Repositories
{
    public class WardRepository : EFRepository<Ward, int>, IWardRepository
    {
        public WardRepository(AppDbContext context) : base(context)
        {
        }
    }
}