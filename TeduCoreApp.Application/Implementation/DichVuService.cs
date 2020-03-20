using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.DichVu;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.Utilities.Dtos;
using TeduCoreApp.Utilities.Helpers;
using System;

namespace TeduCoreApp.Application.Implementation
{
    public class DichVuService : IDichVuService
    {
        private readonly IRepository<DichVu, int> _dichvuRepository;
        private readonly IRepository<Tag, string> _tagRepository;
        private readonly IRepository<DichVuTag, int> _dichvuTagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DichVuService(IRepository<DichVu, int> dichvuRepository,
            IRepository<DichVuTag, int> dichvuTagRepository,
            IRepository<Tag, string> tagRepository,
            IUnitOfWork unitOfWork)
        {
            _dichvuRepository = dichvuRepository;
            _dichvuTagRepository = dichvuTagRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }

        public DichVuViewModel Add(DichVuViewModel dichvuVm)
        {
            var dichvu = Mapper.Map<DichVuViewModel, DichVu>(dichvuVm);

            if (!string.IsNullOrEmpty(dichvu.Tags))
            {
                var tags = dichvu.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.DichVuTag
                        };
                        _tagRepository.Add(tag);
                    }

                    var dichvuTag = new DichVuTag { TagId = tagId };
                    dichvu.DichVuTags.Add(dichvuTag);
                }
            }
            _dichvuRepository.Add(dichvu);
            return dichvuVm;
        }

        public void Delete(int id)
        {
            _dichvuRepository.Remove(id);
        }

        public List<DichVuViewModel> GetAll()
        {
            return _dichvuRepository.FindAll(c => c.DichVuTags)
                .ProjectTo<DichVuViewModel>().ToList();
        }


        public PagedResult<DichVuViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var query = _dichvuRepository.FindAll(x => x.Status == Status.Active);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));
            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);


            int totalRow = query.Count();

            query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);

            var data = query.ProjectTo<DichVuViewModel>().ToList();

            var paginationSet = new PagedResult<DichVuViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public DichVuViewModel GetById(int id)
        {
            return Mapper.Map<DichVu, DichVuViewModel>(_dichvuRepository.FindById(id));
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(DichVuViewModel dichvu)
        {
            _dichvuRepository.Update(Mapper.Map<DichVuViewModel, DichVu>(dichvu));
            if (!string.IsNullOrEmpty(dichvu.Tags))
            {
                string[] tags = dichvu.Tags.Split(',');
                foreach (string t in tags)
                {
                    var tagId = TextHelper.ToUnsignString(t);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tag = new Tag
                        {
                            Id = tagId,
                            Name = t,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tag);
                    }
                    _dichvuTagRepository.RemoveMultiple(_dichvuTagRepository.FindAll(x => x.Id == dichvu.Id).ToList());
                    DichVuTag dichvuTag = new DichVuTag
                    {
                        DichVuId = dichvu.Id,
                        TagId = tagId
                    };
                    _dichvuTagRepository.Add(dichvuTag);
                }
            }
        }

        public List<DichVuViewModel> GetLastest(int top)
        {
            return _dichvuRepository.FindAll(x => x.Status == Status.Active).OrderByDescending(x => x.DateCreated)
                .Take(top).ProjectTo<DichVuViewModel>().ToList();
        }

        public List<DichVuViewModel> GetHotProduct(int top)
        {
            return _dichvuRepository.FindAll(x => x.Status == Status.Active && x.HotFlag == true)
                .OrderByDescending(x => x.DateCreated)
                .Take(top)
                .ProjectTo<DichVuViewModel>()
                .ToList();
        }

        public List<DichVuViewModel> GetListPaging(int page, int pageSize, string sort, out int totalRow)
        {
            var query = _dichvuRepository.FindAll(x => x.Status == Status.Active);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<DichVuViewModel>().ToList();
        }

        public List<string> GetListByName(string name)
        {
            return _dichvuRepository.FindAll(x => x.Status == Status.Active
            && x.Name.Contains(name)).Select(y => y.Name).ToList();
        }

        public List<DichVuViewModel> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _dichvuRepository.FindAll(x => x.Status == Status.Active
            && x.Name.Contains(keyword));

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateCreated);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<DichVuViewModel>()
                .ToList();
        }

        public List<DichVuViewModel> GetRelatedDichVus(int id, int top)
        {
            return _dichvuRepository.FindAll(x => x.Status == Status.Active
                && x.Id != id)
            .OrderByDescending(x => x.DateCreated)
            .Take(top)
            .ProjectTo<DichVuViewModel>()
            .ToList();
        }

        public List<TagViewModel> GetListTagById(int id)
        {
            return _dichvuTagRepository.FindAll(x => x.DichVuId == id, c => c.Tag)
                .Select(y => y.Tag)
                .ProjectTo<TagViewModel>()
                .ToList();
        }

        public void IncreaseView(int id)
        {
            var product = _dichvuRepository.FindById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
        }

        public List<DichVuViewModel> GetListByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from p in _dichvuRepository.FindAll()
                        join pt in _dichvuTagRepository.FindAll()
                        on p.Id equals pt.DichVuId
                        where pt.TagId == tagId && p.Status == Status.Active
                        orderby p.DateCreated descending
                        select p;

            totalRow = query.Count();

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var model = query
                .ProjectTo<DichVuViewModel>();
            return model.ToList();
        }

        public TagViewModel GetTag(string tagId)
        {
            return Mapper.Map<Tag, TagViewModel>(_tagRepository.FindSingle(x => x.Id == tagId));
        }

        public List<DichVuViewModel> GetList(string keyword)
        {
            var query = !string.IsNullOrEmpty(keyword) ?
                _dichvuRepository.FindAll(x => x.Name.Contains(keyword)).ProjectTo<DichVuViewModel>()
                : _dichvuRepository.FindAll().ProjectTo<DichVuViewModel>();
            return query.ToList();
        }

        public List<TagViewModel> GetListTag(string searchText)
        {
            return _tagRepository.FindAll(x => x.Type == CommonConstants.ProductTag
            && searchText.Contains(x.Name)).ProjectTo<TagViewModel>().ToList();
        }
    }
}