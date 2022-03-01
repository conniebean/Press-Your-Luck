using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PressYourLuck.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PressYourLuck.Models.ViewModels;
using Newtonsoft.Json;
using PressYourLuck.Models;

namespace PressYourLuck.Controllers
{
    public class GameController : Controller
    {
        private AuditContext _auditContext;
        public GameController(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Tile> tileList = GameHelper.GetCurrentGame(HttpContext);

            if (tileList.Count <= 0)
            {
                tileList = GameHelper.GenerateNewGame();

                GameHelper.SaveCurrentGame(HttpContext, tileList);
                return View(tileList);
            }
            else
            {
                return View(tileList);
            }
        }

        [HttpPost]
        public IActionResult Index(GameViewModel gameViewModel)
        {

            double currentBet = gameViewModel.CurrentBet;
            double totalCoins = CoinsHelper.GetTotalCoins(HttpContext);
            if (currentBet != double.Parse("0.00") && currentBet <= totalCoins)
            {
                
                CoinsHelper.SaveCurrentBet(HttpContext, currentBet);
                CoinsHelper.SaveOriginalBet(HttpContext, currentBet);
                totalCoins = totalCoins - currentBet;
                CoinsHelper.SaveTotalCoins(HttpContext, totalCoins);

                return RedirectToAction("Index", "Game");
                
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            
        }
        
        public IActionResult Reveal(int id)
        {

            List<Tile> currentGame = GameHelper.GetCurrentGame(HttpContext);

            double multiplier = double.Parse(currentGame[id].Value);

            GameHelper.PickTileAndUpdateGame(HttpContext, id, currentGame, multiplier);
            if (currentGame[id].Value == "0.00")
            {
                TempData["Message"] = "Oh no! You've busted out. Better luck next time!";
            }
            else if (double.Parse(currentGame[id].Value) > double.Parse("0.00"))
            {
                TempData["Message"] = $"Congrats! You've found a {currentGame[id].Value} multiplier! " +
                    $"All remaining values have doubled. Will you press your luck?";
            }

            if (CoinsHelper.GetCurrentBet(HttpContext) == double.Parse("0.00") &&
                CoinsHelper.GetTotalCoins(HttpContext) == double.Parse("0.00"))
            {
                Audit audit = new Audit()
                {
                    //lose
                    CreatedDate = DateTime.Now,
                    PlayerName = GameHelper.GetPlayerName(HttpContext),
                    AuditTypeID = 4,
                    Amount = CoinsHelper.GetOriginalBet(HttpContext)
                };
                _auditContext.Audits.Add(audit);
                _auditContext.SaveChanges();

            }

            return RedirectToAction("Index", "Game");
        }

        public IActionResult TryAgain()
        {
            GameViewModel gameViewModel = new GameViewModel();
            if (gameViewModel.CurrentBet == 0)
            {
                GameHelper.ClearCurrentGame(HttpContext);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult TakeTheCoins()
        {
            if (CoinsHelper.GetTotalCoins(HttpContext) >= 0)
            {
                double currentBet = CoinsHelper.GetCurrentBet(HttpContext);
                double totalCoins = CoinsHelper.GetTotalCoins(HttpContext);

                double newTotal = currentBet + totalCoins;

                CoinsHelper.SaveTotalCoins(HttpContext, newTotal);
                Audit audit = new Audit()
                {
                    //win
                    CreatedDate = DateTime.Now,
                    PlayerName = GameHelper.GetPlayerName(HttpContext),
                    AuditTypeID = 3,
                    Amount = newTotal
                };
                _auditContext.Audits.Add(audit);
                _auditContext.SaveChanges();


                TempData["Message"] = $"BIG WINNER! You chased out for {newTotal.ToString("C2")} coins!  Care to press your luck again?";
                GameHelper.ClearCurrentGame(HttpContext);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                Response.Cookies.Delete("playerName");
                Response.Cookies.Delete("playerCoins");
                return RedirectToAction("Index", "Player");
            }
        }

    }
}
