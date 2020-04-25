using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Application.Implementation
{
    public class RecruitmentService : IRecruitmentService
    {
        private IRecruitmentRepository _recruitmentRepository;
        private IUnitOfWork _unitOfWork;

        public RecruitmentService(IRecruitmentRepository recruitmentRepository,
            IUnitOfWork unitOfWork)
        {
            _recruitmentRepository = recruitmentRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(RecruitmentViewModel recruitmentkVm)
        {
            var page = Mapper.Map<RecruitmentViewModel, Recruitment>(recruitmentkVm);
            _recruitmentRepository.Add(page);
        }

        public void Delete(int id)
        {
            _recruitmentRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<RecruitmentViewModel> GetAll()
        {
            return _recruitmentRepository.FindAll().ProjectTo<RecruitmentViewModel>().ToList();
        }

        public PagedResult<RecruitmentViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _recruitmentRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<RecruitmentViewModel>()
            {
                Results = data.ProjectTo<RecruitmentViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public RecruitmentViewModel GetById(int id)
        {
            return Mapper.Map<Recruitment, RecruitmentViewModel>(_recruitmentRepository.FindById(id));
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(RecruitmentViewModel feedbackVm)
        {
            var page = Mapper.Map<RecruitmentViewModel, Recruitment>(feedbackVm);
            _recruitmentRepository.Update(page);
        }
    }
}