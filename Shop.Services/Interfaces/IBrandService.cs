using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Services.DTOs.BrandDTOs;
using Shop.Services.DTOs.Common;

namespace Shop.Services.Interfaces
{
    public  interface IBrandService
    {
        CreatedEntityDTO Create(BrandPostDTO postDTO);
        void Edit(int id,BrandPutDTO putDTO);
        List<BrandGetAllItemDTO> GetAll(); 
        void Delete(int id);    
        BrandGetDTO GetById(int id);
    }
}
