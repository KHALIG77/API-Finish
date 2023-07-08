using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Core.Entities;
using Shop.Core.Repositories;
using Shop.Services.DTOs.BrandDTOs;
using Shop.Services.DTOs.Common;
using Shop.Services.Exceptions;
using Shop.Services.Interfaces;

namespace Shop.Services.Implimentations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository,IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public CreatedEntityDTO Create(BrandPostDTO postDTO)
        {
            if(_brandRepository.IsExist(x=>x.Name==postDTO.Name)) {throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name is already exist"); }
            Brand brand = _mapper.Map<Brand>(postDTO);
            _brandRepository.Add(brand);
            _brandRepository.Commit();
            return new CreatedEntityDTO {Id=brand.Id};
        }

        public void Delete(int id)
        {
            Brand brand=_brandRepository.Get(x=>x.Id == id);

            if (brand == null) throw new RestException(System.Net.HttpStatusCode.NotFound, "Brand not found");

            _brandRepository.Remove(brand);
            _brandRepository.Commit();
        }

        public void Edit(int id, BrandPutDTO putDTO)
        {
            Brand brand = _brandRepository.Get(x => x.Id == id);

            if (brand == null) throw new RestException(System.Net.HttpStatusCode.NotFound, "Brand not found");
            if (brand.Name!=putDTO.Name && _brandRepository.IsExist(x => x.Name == putDTO.Name)) { throw new RestException(System.Net.HttpStatusCode.BadRequest, "Name", "Name is already exist"); }
            brand.Name=putDTO.Name;
            _brandRepository.Commit();

        }

        public List<BrandGetAllItemDTO> GetAll()
        {
            var data = _brandRepository.GetAll(x=>true);
            return _mapper.Map<List<BrandGetAllItemDTO>>(data);
        }
        public BrandGetDTO GetById(int id)
        {
            Brand brand = _brandRepository.Get(x => x.Id == id,"Products");

            if (brand == null) throw new RestException(System.Net.HttpStatusCode.NotFound, "Brand not found");

            return _mapper.Map<BrandGetDTO>(brand);
        }
    }
}
