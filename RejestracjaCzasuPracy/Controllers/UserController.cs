using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RejestracjaCzasuPracy.Controllers
{
    public class UserController : Controller
    {
        public string GetAllUSers()
        {
            return "All users"; 
        }

        public string GetUser(int id)
        {
            return $"User with id = {id}";
        }
    }
}
