using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PressYourLuck.Models;
using PressYourLuck.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PressYourLuck.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AuditContext _auditContext;
        public HomeController(ILogger<HomeController> logger, AuditContext auditContext)
        {
            _auditContext = auditContext;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {

            if (!HttpContext.Request.Cookies.ContainsKey("playerName"))
            {
                return RedirectToAction("Index", "Player");
            }
            else
            {


                return View();
            }



        }

        public RedirectToActionResult Delete()
        {
            if (CoinsHelper.GetTotalCoins(HttpContext) == 0.00)
            {
                TempData["Message"] = "You've lost all your coins and must enter more to keep playing.";
            }
            else
            {
                TempData["Message"] = $"You cashed out for {CoinsHelper.GetTotalCoins(HttpContext).ToString("C2")}.";
            }

            if (CoinsHelper.GetCurrentBet(HttpContext) != double.Parse("0.00") &&
                CoinsHelper.GetTotalCoins(HttpContext) != double.Parse("0.00"))
            {
                Audit audit = new Audit()
                {
                    //cash out
                    CreatedDate = DateTime.Now,
                    PlayerName = GameHelper.GetPlayerName(HttpContext),
                    AuditTypeID = 2,
                    Amount = CoinsHelper.GetTotalCoins(HttpContext)
                };
                _auditContext.Audits.Add(audit);
                _auditContext.SaveChanges();

            }


            Response.Cookies.Delete("playerName");
            Response.Cookies.Delete("playerCoins");
            GameHelper.ClearCurrentGame(HttpContext);
            return RedirectToAction("Index", "Player");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}