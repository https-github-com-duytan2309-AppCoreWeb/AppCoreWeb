using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IRecruitmentService
    {
        void Add(RecruitmentViewModel contactVm);

        void Update(RecruitmentViewModel contactVm);

        void Delete(int id);

        List<RecruitmentViewModel> GetAll();

        PagedResult<RecruitmentViewModel> GetAllPaging(string keyword, int page, int pageSize);

        RecruitmentViewModel GetById(int id);

        void SaveChanges();
    }
}