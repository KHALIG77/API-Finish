using AutoMapper;
using Shop.Services.DTOs.BrandDTOs;
using Shop.Services.DTOs.ProductDTOs;
using Shop.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Shop.Services.Profiles
{
    public class Mapper:Profile
    {
       

        public Mapper(IHttpContextAccessor accessor)
        {
            var uriBuilder = new UriBuilder(accessor.HttpContext.Request.Scheme, accessor.HttpContext.Request.Host.Host, accessor.HttpContext.Request.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }
            string baseUrl = uriBuilder.Uri.AbsoluteUri;

            CreateMap<Product, ProductGetDTO>().ForMember(x=>x.ImageUrl, m => m.MapFrom(s => baseUrl + $"uploads/products/{s.ImageUrl}"));
            CreateMap<ProductPostDTO, Product>();
            CreateMap<BrandPostDTO, Brand>();
            //Productdan ProoductgetAllItem yaradanda icindeki spesifik bir propa menimsetme elemek ucun member islenir.
            //Birinci hissesinde yaranacah obyektin hansi propertisi secilir o gosderilir, sorada ikinci hissede hansi obyetkden yaradilirsa o obyetktin propunun sertine uygun beraberlesir
            CreateMap<Product,ProductGetAllItemDTO>().ForMember(dest=>dest.HasDiscount,m=>m.MapFrom(s=>s.DiscountPercent>0))
                .ForMember(x => x.ImageUrl, m => m.MapFrom(s => baseUrl + $"uploads/products/{s.ImageUrl}"));

            CreateMap<Brand, BrandGetDTO>();
            CreateMap<Brand, BrandGetAllItemDTO>();
            CreateMap<Brand,BrandInProductGetDTO>();
           
        }
    }
}
