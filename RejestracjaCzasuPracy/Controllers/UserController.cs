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
<<<<<<< Updated upstream
       
        [HttpGet("[action]")]
        public ActionResult GetAllUSers()
=======
        User currentUser;

        public string GetAllUSers()
>>>>>>> Stashed changes
        {
            List<User> allUsers = userManager.GetAllUsers();

<<<<<<< Updated upstream
            if (allUsers != null)
                return Ok(allUsers);
            return BadRequest();
=======
        public string GetUser(string memberID)
        {
            currentUser = userManager.GetUserWithID(memberID);

            return $"{currentUser.firstName} {currentUser.surName}";
>>>>>>> Stashed changes
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
