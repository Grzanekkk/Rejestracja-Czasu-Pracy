using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseConnection;

namespace RejestracjaCzasuPracy.Controllers
{
    public class UserController : Controller
    {
        UserManager userManager = new UserManager();
        User currentUser;

        public string GetAllUSers()
        {
            return "All users"; 
        }

        public string GetUser(string memberID)
        {
            currentUser = userManager.GetUserWithID(memberID);

            return $"{currentUser.firstName} {currentUser.surName}";
        }

        public string EmailOwner(string name, string email)
        {
            return $"{email} belongs to {name}";
        }
    }
}
