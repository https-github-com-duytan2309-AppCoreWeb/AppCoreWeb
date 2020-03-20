using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Data.EF.Repositories
{
    public class ProvinceRepository : EFRepository<Province, int>, IProvinceRepository
    {
        public ProvinceRepository(AppDbContext context) : base(context)
        {
        }
    }
}