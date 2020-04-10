using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Data.EF.Repositories
{
    public class ShipCodeRepository : EFRepository<ShipCode, long>, IShipCodeRepository
    {
        public ShipCodeRepository(AppDbContext context) : base(context)
        {
        }
    }
}