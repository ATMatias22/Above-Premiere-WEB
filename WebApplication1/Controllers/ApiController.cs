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

        [Route("check")]
        [HttpPost]
        public IActionResult CheckUser([FromBody] JsonElement body)
        {
            User getUser = JsonConvert.DeserializeObject<User>(body.ToString());
            try
            {
                User userFound = UserDAO.getInstance().loginUser(getUser);
                return Ok(new { valid = true, title = "Logged in", message = "Successfully logged in", user = userFound });
            }
            catch (Exception e)
            {
                return Ok(new { valid = false, title = "Login failed", message = e.Message, user = "null" });
            }
        }




        [Route("register")]
        [HttpPost]
        public IActionResult RegisterUser([FromBody] JsonElement body)
        {
            User getUser = JsonConvert.DeserializeObject<User>(body.ToString());
            try
            {
                User registeredUser =  UserDAO.getInstance().registerUser(getUser);
                return Ok(new { valid = true, title = "Registered", message = "Registered Successfully" });
            }
            catch (Exception e)
            {
                return Ok(new { valid = false, title = "Registration failed", message = e.Message });
            }
        }

    }
}
