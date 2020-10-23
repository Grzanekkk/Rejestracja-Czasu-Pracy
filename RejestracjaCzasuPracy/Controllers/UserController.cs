using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseConnection;

namespace RejestracjaCzasuPracy.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserManager userManager = new UserManager();
       
        [HttpGet("[action]")]
        public ActionResult GetAllUSers()
        {
            var users = userManager.GetAllUsers();

            if (users != null)
                return Ok(users);
            return BadRequest();
        }

        [HttpGet("[action]")]
        public ActionResult GetUser(string memberID)
        {
            User currnetUser = userManager.GetUserWithID(memberID);

            if (currnetUser != null)
                return Ok(currnetUser);
            return BadRequest();
        }

        public string EmailOwner(string name, string email)
        {
            return $"{email} belongs to {name}";
        }
    }
}
