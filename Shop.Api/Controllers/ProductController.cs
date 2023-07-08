using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Services.DTOs.ProductDTOs;
using Shop.Core.Entities;
using Shop.Core.Repositories;
using Shop.Services.Interfaces;

namespace Shop.Api.Controllers
{

    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {

            _productService = productService;
        }
        [HttpPost("")]

        public ActionResult<ProductPostDTO> Create([FromForm] ProductPostDTO postDTO)
        {
            return StatusCode(201, _productService.Create(postDTO));
        }
        [HttpGet("all")]

        public ActionResult<List<ProductGetAllItemDTO>> GetAll()
        {

            return Ok(_productService.GetAll());
        }
        [HttpPut("{id}")]

        public ActionResult<ProductPutDTO> Update(int id, [FromForm] ProductPutDTO productPutDTO)
        {
            _productService.Edit(id, productPutDTO);
            return NoContent();
        }
        [HttpGet("{id}")]
        public ActionResult<ProductGetDTO> Get(int id)
        {

            return StatusCode(201, _productService.GetById(id));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return NoContent();
        }



    }
}
