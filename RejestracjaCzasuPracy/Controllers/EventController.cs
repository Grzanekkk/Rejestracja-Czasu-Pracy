using DatabaseConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RejestracjaCzasuPracy.Controllers
{
    public class EventController : Controller
    {
        TimeManager timeManager = new TimeManager();
        UserManager userManager = new UserManager();

        public ActionResult GetAllUserEvents(string memberID)
        {
            userManager.GetUserEvents(memberID);
            return null;
        }
    }
}
