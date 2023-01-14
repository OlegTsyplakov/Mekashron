using Mekashron.Models;
using Mekashron.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using ServiceMekashron;
using System.Text.Json;


namespace Mekashron.Controllers
{
    public class AccountController : Controller
    {

        private readonly string _ip;


        public AccountController(IHttpContextAccessor context)
        {
            _ip = context?.HttpContext?.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString() ?? string.Empty;

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            model.IPs = _ip;

            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {

                var client = new ICUTechClient();
                JsonDocument jsonDocument;
                var response = await client.LoginAsync(model.UserName, model.Password, model.IPs);

                if (!string.IsNullOrEmpty(response.@return))
                {

                    try
                    {
                        jsonDocument = JsonDocument.Parse(response.@return);
                  
                        var jsonDeserialize = jsonDocument.Deserialize<ResponseError>();
                     
                        if (jsonDeserialize?.ResultCode<0)
                        {

                            ViewBag.Error = jsonDeserialize.ResultMessage;
                            return View(model);
                        }
                    }
                    catch
                    {
                        ViewBag.Error = "Invalid json file.";
                        return View(model);
                    }


                 ViewBag.Success = response.@return;
                }

            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.SignupIP = _ip;
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var client = new ICUTechClient();
                JsonDocument jsonDocument;
                var response = await client.RegisterNewCustomerAsync(model.Email, model.Password, model.FirstName, model.LastName, model.Mobile, model.CountryID, model.aID, model.SignupIP);
                if (!string.IsNullOrEmpty(response.@return))
                {
                    try
                    {
                        jsonDocument = JsonDocument.Parse(response.@return);
                        var jsonDeserialize = jsonDocument.Deserialize<ResponseError>();

                        if (jsonDeserialize?.ResultCode < 0)
                        {

                            ViewBag.Error = jsonDeserialize.ResultMessage;
                            return View(model);
                        }
                    }
                    catch
                    {
                        ViewBag.Error = "Invalid json file.";
                        return View(model);
                    }

                    ViewBag.Success = response.@return;
                }
            }

            return View(model);
        }
    }
}
