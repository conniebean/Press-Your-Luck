using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PressYourLuck.Models;
using PressYourLuck.Helpers;

namespace PressYourLuck.Controllers
{
    public class PlayerController : Controller
    {
        private AuditContext _auditContext;
        public PlayerController(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Index(Player player)
        {
            if (ModelState.IsValid)
            {
                GameHelper.SavePlayerName(HttpContext, player.Name);
                CoinsHelper.SaveTotalCoins(HttpContext, player.Coins);

                Audit audit = new Audit()
                {
                    //cash in
                    CreatedDate = DateTime.Now,
                    PlayerName = player.Name,
                    AuditTypeID = 1,
                    Amount = player.Coins
                };
                _auditContext.Audits.Add(audit);
                _auditContext.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else 
            {
                return View(player);
            }
            
        }

        public JsonResult NameIsEmpty(string name)
        {
            if (name == "")
            {
                return Json("You must enter your name to continue!");
            }
            else
            {
                return Json(true);
            }
        }
        public JsonResult CoinsAreEmpty(double coins)
        {
            if (coins == 0)
            {
                return Json("You must enter your coins to continue!");
            }
            else if(coins > 10000)
            {
                return Json("Your coins must be between 1 and 10,000!");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
