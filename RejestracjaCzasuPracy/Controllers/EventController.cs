﻿using DatabaseConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace RejestracjaCzasuPracy.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        TimeManager timeManager = new TimeManager();
        UserManager userManager = new UserManager();


        #region GET


        [HttpGet("[action]")]
        public ActionResult GetUserEvents(string memberID)  // returns all user events
        {
            DataTable events = timeManager.GetUserEvents(memberID);

            if (events != null)
                return Ok(events);
            return BadRequest();
        }

        [HttpGet("[action]")]
        public ActionResult GetSummaryForAllUsers()
        {
            DataTable summary = timeManager.GetSummaryForAllUsers();

            if (summary != null)
                return Ok(summary);
            return BadRequest();
        }

        [HttpGet("[action]")]
        public ActionResult IsWorking(string memberID)
        {
            bool isWorking = timeManager.IsWorking(memberID);

            return Ok(isWorking);
        }

        [HttpGet("[action]")]
        public ActionResult WorkButton(string memberID)
        {
            if (timeManager.IsWorking(memberID))
            {
                if (timeManager.IsOnBreak(memberID))
                {
                    timeManager.FinishBreak(memberID);
                }

                timeManager.StopWorking(memberID);
            }
            else    // User is not working now
            {
                timeManager.StartWorking(memberID);
            }

            return Ok(timeManager.IsWorking(memberID));
        }

        [HttpGet("[action]")]
        public ActionResult BreakButton(string memberID)
        {
            if (timeManager.IsOnBreak(memberID))
            {
                timeManager.FinishBreak(memberID);
            }
            else
            {
                timeManager.StartBreak(memberID);
            }

            return Ok(timeManager.IsOnBreak(memberID));
        }

        [HttpGet("[action]")]
        public ActionResult IsOnBreak(string memberID)
        {
            return Ok(timeManager.IsOnBreak(memberID));
        }

        [HttpGet("[action]")]
        public ActionResult DeleteEvent(string eventID, string memberID)
        {
            timeManager.DeleteEvent(eventID);

            return GetUserEvents(memberID);
        }

        [HttpGet("[action]")]
        public void AddNewEvent(string memberID, int minutes)
        {
            if(minutes != 0)
            {
                timeManager.AddNewEvent(memberID, minutes);
            }
        }

        [HttpGet("[action]")]
        public void GoHome(string memberID)
        {
            User currentUser = userManager.GetUserWithID(memberID);

            timeManager.AddNewEvent(memberID, timeManager.CountBalanceFromNow(currentUser));
        }
        
        [HttpGet("[action]")]
        public ActionResult RefreshData(string memberID)
        {
            DataToRefreshWindow data = new DataToRefreshWindow(timeManager.GetUserEvents(memberID), timeManager.CountUserBalance(memberID));

            if (data != null)
                return Ok(data);
            return BadRequest();
        }


        #endregion GET

    }
    
    public class DataToRefreshWindow
    {
        public DataTable userEvents;
        public int balance;

        public DataToRefreshWindow(DataTable _userEvents, int _balance)
        {
            userEvents = _userEvents;
            balance = _balance;
        }
    }

}
