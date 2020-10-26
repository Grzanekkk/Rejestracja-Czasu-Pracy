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
            List<User> allUsers = userManager.GetAllUsers();

            if (allUsers != null)
                return Ok(allUsers);
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
    }
}
