﻿using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Data.EF.Repositories
{
    public class RecruitmentRepository : EFRepository<Recruitment, int>, IRecruitmentRepository
    {
        public RecruitmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}