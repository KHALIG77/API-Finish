using Shop.Services.DTOs.Common;
using Shop.Services.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shop.Services.Interfaces
{
    public interface IProductService
    {
        CreatedEntityDTO Create (ProductPostDTO productPostDTO);
        void Edit(int id ,ProductPutDTO productPutDTO);
        void Delete(int id);
        List<ProductGetAllItemDTO> GetAll();
        ProductGetDTO GetById(int id);
    }
}
