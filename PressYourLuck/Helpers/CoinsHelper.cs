﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PressYourLuck.Helpers
{
    
    public static class CoinsHelper
    {
        private static CookieOptions options = new CookieOptions()
        {
            Expires = DateTime.Now.AddDays(30)
        };
        /*
         * Consider using this helper to Get and Set the Current Bet and the original bet
         * (both in session variables), as well as adding a Get and Set for the player's
         * total number of coins (which we'll store in Cookies)
         * 
         * HINT: Remember that HttpContext as well as Response and Request objects are not
         * available from here, so you may need to pass those in from your controller.
         * 
         * I've coded the first one for you and have created placeholders for the rest.
         * 
         */
        public static void SaveCurrentBet(HttpContext httpContext, double bet)
        {
            httpContext.Session.SetString("current-bet", bet.ToString("N2"));
        }


        //Return the current bet stored in session
        public static double GetCurrentBet(HttpContext httpContext)
        {
            return double.Parse(httpContext.Session.GetString("current-bet") ?? "0");
            
        }

        //Save the original bet into session
        public static void SaveOriginalBet(HttpContext httpContext, double bet)
        {
            httpContext.Session.SetString("original-bet", bet.ToString("N2"));
        }

        //Get the original bet from session
        public static double GetOriginalBet(HttpContext httpContext)
        {
            //Code goes here
            return double.Parse(httpContext.Session.GetString("original-bet"));
        }

        //Save the players total number of coins into a cookie.  Don't forget to
        // persist the cookie (Chapter 9!)
        public static void SaveTotalCoins(HttpContext httpContext, double coins)
        {
            httpContext.Response.Cookies.Append("playerCoins", coins.ToString("N2"), options);
        }

        //Get the players total number of coins from a cookie.
        public static double GetTotalCoins(HttpContext httpContext)
        {
            return double.Parse(httpContext.Request.Cookies["playerCoins"] ?? "0");
        }


    }
}
