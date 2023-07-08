using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Entities;
using Shop.Core.Repositories;
using Shop.Data.Helper;
using Shop.Services.DTOs.Common;
using Shop.Services.DTOs.ProductDTOs;
using Shop.Services.Exceptions;
using Shop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Implimentations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        private readonly IHttpContextAccessor _accessor;

        public ProductService(IProductRepository productRepository,IMapper mapper,IBrandRepository brandRepository,IHttpContextAccessor accessor)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _brandRepository = brandRepository;
            _accessor = accessor;
        }
        public CreatedEntityDTO Create(ProductPostDTO productPostDTO)
        {
            List<RestExceptionError> errors = new List<RestExceptionError>();
            if (!_brandRepository.IsExist(x => x.Id == productPostDTO.BrandId))
            {
                errors.Add(new RestExceptionError("BrandId", "BrandId is not correct"));
            }
            if(_productRepository.IsExist(x=>x.Name==productPostDTO.Name))
            {
                errors.Add(new RestExceptionError("Name", "Name is already taken"));
            }
            if(errors.Count>0)
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest, errors);
            }
            var entity = _mapper.Map<Product>(productPostDTO);

            string rootPath=Directory.GetCurrentDirectory()+"/wwwroot";
            entity.ImageUrl=FileManager.Save(productPostDTO.ImageFile, rootPath,"uploads/products");

            _productRepository.Add(entity);
            _productRepository.Commit();

            return new CreatedEntityDTO {Id=entity.Id};
        }
       
        public void Delete(int id)
        {
            var entity=_productRepository.Get(x=>x.Id==id); 
            if(entity == null) 
            {
                throw new RestException(System.Net.HttpStatusCode.NotFound, "Product not found");
            }
            string rootPath = Directory.GetCurrentDirectory() + "/wwwroot";
            _productRepository.Remove(entity);
            _productRepository.Commit();

            FileManager.Delete(rootPath, "uploads/products", entity.ImageUrl);

        }

        public void Edit(int id,ProductPutDTO productPutDTO)
        {
            var entity = _productRepository.Get(x => x.Id == id);
            if (entity == null) throw new RestException(System.Net.HttpStatusCode.NotFound, "Product not found");

            List<RestExceptionError> errors = new List<RestExceptionError>();

            if (!_brandRepository.IsExist(x => x.Id == productPutDTO.BrandId))
                errors.Add(new RestExceptionError("BrandId", "BrandId is not correct"));

            if (entity.Name!=productPutDTO.Name && _productRepository.IsExist(x=>x.Name==productPutDTO.Name))
            {
                errors.Add(new RestExceptionError("Name", "Name is already taken"));
            }
            if(errors.Count>0) 
            {
                throw new RestException(System.Net.HttpStatusCode.BadRequest,errors);
            }

            entity.CostPrice = productPutDTO.CostPrice;
            entity.Name = productPutDTO.Name;
            entity.SalePrice = productPutDTO.SalePrice;
            entity.DiscountPercent = productPutDTO.DiscountPercent;
            entity.BrandId = productPutDTO.BrandId;

            string? oldFileName = null;
            string rootPath = Directory.GetCurrentDirectory()+"/wwwroot";
            if (productPutDTO.ImageFile != null)
            {
                oldFileName = entity.ImageUrl;
                entity.ImageUrl = FileManager.Save(productPutDTO.ImageFile, rootPath, "uploads/products");
            }
            _productRepository.Commit();

            if(oldFileName != null)
            {
                FileManager.Delete(rootPath, "uploads/products", entity.ImageUrl);
            }

        }

        public List<ProductGetAllItemDTO> GetAll()
        {
            var data = _productRepository.GetAll(x => true, "Brand");
            return _mapper.Map<List<ProductGetAllItemDTO>>(data);
        }

        public ProductGetDTO GetById(int id)
        {
            var entity=_productRepository.Get(x=>x.Id==id,"Brand");
            if (entity == null) throw new RestException(System.Net.HttpStatusCode.NotFound, "Product not found");
            return _mapper.Map<ProductGetDTO>(entity);
        }
    }
}
