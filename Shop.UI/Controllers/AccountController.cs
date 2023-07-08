using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.UI.Models;

namespace Shop.UI.Controllers
{
    public class AccountController : Controller
    {
        private HttpClient _client;
        public AccountController()
        {
            _client=new HttpClient();   
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest login,string returnUrl=null)
        { if(!ModelState.IsValid) 
            {
                return View(); 
            }

            StringContent content = new StringContent(JsonConvert.SerializeObject(login),System.Text.Encoding.UTF8,"application/json");
            using(var response= await _client.PostAsync("https://localhost:7065/api/Auth/login", content))
            {
                if(response.IsSuccessStatusCode)
                {
                    var responseContent= await response.Content.ReadAsStringAsync();
                    var token = "Bearer " + responseContent;
                    HttpContext.Response.Cookies.Append("login-token", token);
                    return returnUrl==null?RedirectToAction("index","home"):Redirect(returnUrl);
                }
                else if(response.StatusCode==System.Net.HttpStatusCode.BadRequest || response.StatusCode==System.Net.HttpStatusCode.NotFound) 
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                    return View();
                }
                else if (response.StatusCode==System.Net.HttpStatusCode.Forbidden|| response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("login", "account");
                }
            }
            return View("error");
        }
    }
}
