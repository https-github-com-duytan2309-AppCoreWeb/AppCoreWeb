﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Application.ViewModels.Blog;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Application.ViewModels.DichVu;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Application.ViewModels.System;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(c => new ProductCategory(c.Name, c.Description, c.ParentId, c.HomeOrder, c.Image, c.HomeFlag,
                c.SortOrder, c.Status, c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<DichVuCategoryViewModel, DichVuCategory>()
                .ConstructUsing(c => new DichVuCategory(c.Name, c.Description, c.ParentId, c.MenuId, c.HomeOrder, c.Image, c.HomeFlag,
                c.SortOrder, c.Status, c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<BlogCategoryViewModel, BlogCategory>()
                .ConstructUsing(c => new BlogCategory(c.Name, c.Description, c.ParentId, c.MenuId, c.HomeOrder, c.Image, c.HomeFlag,
                c.SortOrder, c.Status, c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<DichVuViewModel, DichVu>()
                .ConstructUsing(c => new DichVu(c.Name, c.CategoryId, c.Image, c.Description, c.Content, c.HomeFlag, c.HotFlag, c.Tags, c.Status,
                c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<BlogViewModel, Blog>()
                .ConstructUsing(c => new Blog(c.Name, c.CategoryId, c.Image, c.Description, c.Content, c.HomeFlag, c.HotFlag, c.Tags, c.Status,
                c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<ProductTrademarkViewModel, ProductTrademark>()
                .ConstructUsing(c => new ProductTrademark(c.Name, c.Description, c.HomeOrder, c.Image, c.HomeFlag,
                c.SortOrder, c.Status, c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<MenuViewModel, Menu>()
                .ConstructUsing(c => new Menu(c.Name, c.Description, c.Url, c.Target, c.ParentId, c.HomeOrder,
                c.SortOrder, c.SeoAlias));

            CreateMap<ProductViewModel, Product>()
                .ConstructUsing(c => new Product(c.Name, c.CategoryId, c.Image, c.Price, c.OriginalPrice,
                c.PromotionPrice, c.Description, c.Content, c.HomeFlag, c.HotFlag, c.Tags, c.Unit, c.Status,
                c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription, c.VAT, c.Code));

            CreateMap<AppUserViewModel, AppUser>()
                .ConstructUsing(c => new AppUser(c.Id.GetValueOrDefault(Guid.Empty), c.FullName, c.UserName,
                c.Email, c.PhoneNumber, c.Avatar, c.Status));

            CreateMap<PermissionViewModel, Permission>()
                .ConstructUsing(c => new Permission(c.RoleId, c.FunctionId, c.CanCreate, c.CanRead, c.CanUpdate, c.CanDelete));

            CreateMap<BillViewModel, Bill>()
                .ConstructUsing(c => new Bill(c.Id, c.CustomerName, c.CustomerAddress,
                c.CustomerMobile, c.CustomerMessage, c.BillStatus,
                c.PaymentMethod, c.Status, c.CustomerId, c.Code, c.ShipCodeId, c.AdminId));

            CreateMap<BillDetailViewModel, BillDetail>()
                .ConstructUsing(c => new BillDetail(c.Id, c.BillId, c.ProductId,
                c.Quantity, c.Price, c.ColorId, c.SizeId, c.PromotionPrice));

            CreateMap<ContactViewModel, ContactDetails>()
                .ConstructUsing(c => new ContactDetails(c.Id, c.Name, c.Phone, c.Email, c.Website, c.Address, c.Other, c.Lng, c.Lat, c.Status));

            CreateMap<FeedbackViewModel, Feedbacks>()
                .ConstructUsing(c => new Feedbacks(c.Id, c.Name, c.Email, c.Message, c.Status));

            CreateMap<PageViewModel, Page>()
                .ConstructUsing(c => new Page(c.Id, c.Name, c.Alias, c.Content, c.Status, c.HomeOrder,
                c.SortOrder, c.SeoAlias));

            CreateMap<AnnouncementViewModel, Announcement>()
                .ConstructUsing(c => new Announcement(c.Title, c.Content, c.UserId, c.Status));

            CreateMap<AnnouncementUserViewModel, AnnouncementUser>()
                .ConstructUsing(c => new AnnouncementUser(c.AnnouncementId, c.UserId, c.HasRead));

            CreateMap<StreetViewModel, Street>()
                    .ConstructUsing(c => new Street(c.Id, c.Code, c.Name, c.Rank, c.WardId, c.DistrictId, c.Status));

            CreateMap<WardViewModel, Ward>()
                    .ConstructUsing(c => new Ward(c.Id, c.Code, c.Name, c.Rank, c.ProvinceId, c.DistrictId, c.Status));

            CreateMap<DistrictViewModel, District>()
                    .ConstructUsing(c => new District(c.Id, c.Code, c.Name, c.Rank, c.ProvinceId, c.Status));

            CreateMap<ProvinceViewModel, Province>()
                    .ConstructUsing(c => new Province(c.Id, c.Code, c.Name, c.Rank, c.Status));

            CreateMap<ShipCodeViewModel, ShipCode>()
                    .ConstructUsing(c => new ShipCode(c.Id, c.Carriers, c.DeliveryTime, c.CollectionFee, c.Total, c.ZipCode, c.IdAddress));

            CreateMap<RecruitmentViewModel, Recruitment>()
                    .ConstructUsing(c => new Recruitment(c.Id, c.Name, c.Image, c.Description, c.Content, c.Status, c.SeoPageTitle, c.SeoAlias, c.SeoKeywords, c.SeoDescription));

            CreateMap<ColorViewModel, Color>()
                       .ConstructUsing(c => new Color(c.Id, c.Name, c.Code));

            CreateMap<SizeViewModel, Size>()
                       .ConstructUsing(c => new Size(c.Id, c.Name));
        }
    }
}