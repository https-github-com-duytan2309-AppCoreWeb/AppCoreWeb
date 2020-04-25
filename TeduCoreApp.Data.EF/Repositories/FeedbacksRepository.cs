using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Data.EF.Repositories
{
    public class FeedbacksRepository : EFRepository<Feedbacks, int>, IFeedbacksRepository
    {
        public FeedbacksRepository(AppDbContext context) : base(context)
        {
        }
    }
}