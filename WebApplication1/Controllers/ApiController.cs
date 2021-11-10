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
                User userFound = UserDAO.getInstance().loginUser(getUser.Name, getUser.Password);
                return Ok(new { valid = true, title = "Logueado", message = "Logueado correctamente", user = userFound });
            }
            catch (Exception e)
            {
                return Ok(new { valid = false, title = "Error al loguearse", message = e.Message, user = "null" });
            }
        }

        [Route("register")]
        [HttpPost]
        public IActionResult RegisterUser([FromBody] JsonElement body)
        {
            User getUser = JsonConvert.DeserializeObject<User>(body.ToString());
            try
            {
                User registeredUser =  UserDAO.getInstance().registerUser(getUser.Name, getUser.Password);
                return Ok(new { valid = true, title = "Registrado", message = "Registrado Correctamente", key = registeredUser.Key });
            }
            catch (Exception e)
            {
                return Ok(new { valid = false, title = "Error al registrarse", message = e.Message, });
            }
        }

        [Route("getAllUser")]
        [HttpGet]
        public IActionResult allUser()
        {
            List<User> users = UserDAO.getInstance().getAllUser();
            return Ok(new { data = users });
        }
    }
}
