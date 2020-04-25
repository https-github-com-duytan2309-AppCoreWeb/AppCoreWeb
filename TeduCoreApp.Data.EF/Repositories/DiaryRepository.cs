using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Data.EF.Repositories
{
    public class DiaryRepository : EFRepository<Diary, int>, IDiaryRespository
    {
        public DiaryRepository(AppDbContext context) : base(context)
        {
        }
    }
}