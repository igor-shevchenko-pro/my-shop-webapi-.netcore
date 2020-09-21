using AutoMapper;
using MyShop.ApiModels.Models;
using MyShop.Core.Entities;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Managers.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Configurations
{
    public class DataMapperConfig : Profile
    {
        private readonly string _fileServerAddress;
        private readonly string _routeToGetFile;

        public DataMapperConfig()
        {
            _fileServerAddress = ConfigurationBuilderManager.Current.GetConfiguration("FileStore:BaseUrl");
            _routeToGetFile = ConfigurationBuilderManager.Current.GetConfiguration("FileStore:GetFile");

            // Category
            CreateMap<Category, CategoryGetFullApiModel>()
                .ForMember(x => x.SeoKeywords, xc => xc.MapFrom(c => SeoKeywordsFromStringToList(c.SeoKeywords)));
            CreateMap<Category, CategoryGetMinApiModel>();
            CreateMap<CategoryAddApiModel, Category>()
                .ForMember(x => x.SeoKeywords, xc => xc.MapFrom(c => SeoKeywordsFromListToString(c.SeoKeywords)));

            // Brand
            CreateMap<Brand, BrandGetFullApiModel>()
                .ForMember(x => x.SeoKeywords, xc => xc.MapFrom(c => SeoKeywordsFromStringToList(c.SeoKeywords)));
            CreateMap<Brand, BrandGetMinApiModel>();
            CreateMap<BrandAddApiModel, Brand>()
                .ForMember(x => x.SeoKeywords, xc => xc.MapFrom(c => SeoKeywordsFromListToString(c.SeoKeywords)));

            // Product
            CreateMap<Product, ProductGetFullApiModel>();
            CreateMap<Product, ProductGetMinApiModel>();
            CreateMap<ProductAddApiModel, Product>();

            // Supplier
            CreateMap<Supplier, SupplierGetFullApiModel>()
                .ForMember(x => x.Products, y => y.MapFrom(xy => xy.SupplierProducts.Select(x => x.Product))); 
            CreateMap<Supplier, SupplierGetMinApiModel>();
            CreateMap<SupplierAddApiModel, Supplier>();

            // FileEntity
            CreateMap<FileEntity, FileEntityGetFullApiModel>()
                .ForMember(d => d.Url, c => c.MapFrom(dc => GetPhotoUrlById(dc.Id)));
            CreateMap<FileEntity, FileEntityGetMinApiModel>()
                .ForMember(d => d.Url, c => c.MapFrom(dc => GetPhotoUrlById(dc.Id)));
            CreateMap<FileEntityAddApiModel, FileEntity>();

            // ImageGallery
            CreateMap<ImageGallery, ImageGalleryGetFullApiModel>();
            CreateMap<ImageGallery, ImageGalleryGetMinApiModel>();
            CreateMap<ImageGalleryAddApiModel, ImageGallery>();

            // UserProfile
            CreateMap<UserProfile, UserProfileGetFullApiModel>();
            CreateMap<UserProfile, UserProfileGetMinApiModel>();
            CreateMap<UserProfileAddApiModel, UserProfile>();

            //User
            CreateMap<User, UserGetFullApiModel>()
                .ForMember(d => d.Roles, c => c.MapFrom(dc => dc.UserRoles.Select(x => x.Role)));
            CreateMap<User, UserGetMinApiModel>();
            CreateMap<UserAddApiModel, User>();

            //Role
            CreateMap<Role, RoleGetFullApiModel>()
                .ForMember(x => x.Users, y => y.MapFrom(xy => xy.UserRoles.Select(z => z.User)));
            CreateMap<Role, RoleGetMinApiModel>();
            CreateMap<RoleAddApiModel, Role>();
        }

        public static IMapper GetMapper()
        {
            var mapplineConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DataMapperConfig());
            });

            IMapper mapper = mapplineConfig.CreateMapper();

            return mapper;
        }


        // Support methods
        private List<string> SeoKeywordsFromStringToList(string seoKeywords)
        {
            var result = new List<string>();

            if (seoKeywords != null)
            {
                result = seoKeywords.Split(';').ToList();
            }

            return result;
        }

        private string SeoKeywordsFromListToString(List<string> seoKeywords)
        {
            var result = String.Join(";", seoKeywords);
            return result;
        }

        private string GetPhotoUrlById(string fileEntityId)
        {
            return string.IsNullOrEmpty(fileEntityId) ? "" : $"{_fileServerAddress}{_routeToGetFile}{fileEntityId}";
        }
    }
}