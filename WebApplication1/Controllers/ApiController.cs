using Above_Premiere.Modelo;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{

    [Route("/api")]
    public class ApiController : Controller
    {
        private UserDAO userDAO;


        public ApiController()
        {
            userDAO = new UserDAO();
        }

        [Route("ping")]
        [HttpGet]
        public string Ping()
        {
            return "Pong";
        }

        [Route("check")]
        [HttpPost]
        public IActionResult checkUser([FromBody] JsonElement body)
        {
            User json = JsonConvert.DeserializeObject<User>(body.ToString());
            try
            {
                User userFound = userDAO.loginUser(json.Name, json.Password);
                return Ok(new { error = false, json.Name, json.Password });
            }
            catch (Exception)
            {
                return Ok(new { error = true });
            }
        }
    }
}
