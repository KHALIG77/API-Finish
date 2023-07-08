using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Shop.UI.Models;

namespace Shop.UI.Controllers
{
	public class ProductController : Controller
	{
		HttpClient _client;
		public ProductController()
		{
			_client = new HttpClient();
		}
		public async Task<IActionResult> Index()
		{
			var token = HttpContext.Request.Cookies["login-token"];
			_client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);
			using (var response = await _client.GetAsync("https://localhost:7065/api/Product/all"))
			{
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var data = JsonConvert.DeserializeObject<List<ProductGetAllItemResponse>>(content);

					return View(data);
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				{
					return RedirectToAction("login", "account");
				}
			};
			return View("error");

		}
		public async Task<IActionResult> Create()
		{
            var token = HttpContext.Request.Cookies["login-token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            ViewBag.Brands = await _getBrands();
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductCreateRequest product)
		{
            var token = HttpContext.Request.Cookies["login-token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            if (!ModelState.IsValid)
			{
                using (var brandResponse = await _client.GetAsync("https://localhost:7065/api/Brands/all"))
                {
                    if (brandResponse.IsSuccessStatusCode)
                    {
                        var brandContent = await brandResponse.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<List<BrandGetAllItemResponce>>(brandContent);
                        ViewBag.Brands = data;
                        return View();
                    }
                    else if (brandResponse.StatusCode == System.Net.HttpStatusCode.Forbidden || brandResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("login", "account");
                    }
                };

            }


            MultipartFormDataContent content= new MultipartFormDataContent();

			var fileContent = new StreamContent(product.ImageFile.OpenReadStream());
			fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(product.ImageFile.ContentType);

			content.Add(fileContent, "ImageFile", product.ImageFile.FileName);
			content.Add(new StringContent(product.Name), "Name");
			content.Add(new StringContent(product.SalePrice.ToString()), "SalePrice");
			content.Add(new StringContent(product.CostPrice.ToString()), "CostPrice");
			content.Add(new StringContent(product.DiscountPercent.ToString()), "DiscountPercent");
			content.Add(new StringContent(product.BrandId.ToString()), "BrandId");

			using (var response = await _client.PostAsync("https://localhost:7065/api/Product",content))
			{
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("index");
				}
				else if(response.StatusCode==System.Net.HttpStatusCode.BadRequest)
				{
					var erros =  JsonConvert.DeserializeObject<ErrorResponseModel>( await response.Content.ReadAsStringAsync());
					foreach (var err in erros.Errors)
					{
						ModelState.AddModelError(err.Key, err.Message);
					}
					ViewBag.Brands = await _getBrands();
					return View();
                }
                return View("error");

            }
		
		}
		public async Task<IActionResult> Edit(int id)
		{
            var token = HttpContext.Request.Cookies["login-token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

			using (var response=await _client.GetAsync($"https://localhost:7065/api/Product/{id}"))
			{
				if(response.IsSuccessStatusCode)
				{
					var responseContent= await response.Content.ReadAsStringAsync();
					ProductGetResponse data=JsonConvert.DeserializeObject<ProductGetResponse>(responseContent);
					var vm = new ProductUpdateRequest
					{
                           BrandId=data.Brand.Id,
						   Name=data.Name,
						   DiscountPercent=data.DiscountPercent,
						   SalePrice=data.SalePrice,
						   CostPrice=data.CostPrice
						   
					};
					ViewBag.ImgUrl = data.ImageUrl;
                    ViewBag.Brands = await _getBrands();
                    return View(vm);

                }
				

			}
			return View("error");



		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id ,ProductUpdateRequest product)
		{
            var token = HttpContext.Request.Cookies["login-token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            if (!ModelState.IsValid)
            {
                     ViewBag.Brands= await _getBrands();
			         return View();
            }


            MultipartFormDataContent content = new MultipartFormDataContent();
			if (product.ImageFile!=null)
			{
                var fileContent = new StreamContent(product.ImageFile.OpenReadStream());
                fileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(product.ImageFile.ContentType);
                content.Add(fileContent, "ImageFile", product.ImageFile.FileName);
            }

           
            content.Add(new StringContent(product.Name), "Name");
            content.Add(new StringContent(product.SalePrice.ToString()), "SalePrice");
            content.Add(new StringContent(product.CostPrice.ToString()), "CostPrice");
            content.Add(new StringContent(product.DiscountPercent.ToString()), "DiscountPercent");
            content.Add(new StringContent(product.BrandId.ToString()), "BrandId");

            using (var response = await _client.PutAsync($"https://localhost:7065/api/Product/{id}", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var erros = JsonConvert.DeserializeObject<ErrorResponseModel>(await response.Content.ReadAsStringAsync());
                    foreach (var err in erros.Errors)
                    {
                        ModelState.AddModelError(err.Key, err.Message);
                    }
                    ViewBag.Brands = await _getBrands();
                    return View();
                }
                return View("error");

            }
        }
		private async Task<List<BrandGetAllItemResponce>> _getBrands()
		{
            

            List<BrandGetAllItemResponce> brands = new List<BrandGetAllItemResponce>();
            using (var brandResponse = await _client.GetAsync("https://localhost:7065/api/Brands/all"))
            {
                if (brandResponse.IsSuccessStatusCode)
                {
                    var brandContent = await brandResponse.Content.ReadAsStringAsync();
                   brands = JsonConvert.DeserializeObject<List<BrandGetAllItemResponce>>(brandContent);
                 
                }
                

            };
            return brands;
        }
    }
}
