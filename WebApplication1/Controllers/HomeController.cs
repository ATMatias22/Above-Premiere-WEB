using Above_Premiere.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Download()
        {
            return View();
        }





        [HttpPost]
        public IActionResult RegisterUser(string username, string password)
        {
            JObject obj;
            try
            {
                User registeredUser = UserDAO.getInstance().registerUser(username, password);
                obj = JObject.FromObject(new { valid = true, title = "Registered", message = "Registered Successfully", key = registeredUser.Key });
            }
            catch (Exception e)
            {
                obj = JObject.FromObject(new { valid = false, title = "Registration failed", message = e.Message });
            }

             ViewBag.msg = obj;
            return View("Register");
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
