using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PressYourLuck.Models;
using PressYourLuck.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PressYourLuck.Helpers
{
    public static class GameHelper
    {
        private static CookieOptions options = new CookieOptions()
        {
            Expires = DateTime.Now.AddDays(30)
        };
        //This creates a collection of 12 tiles with randomly generated values
        public static List<Tile> GenerateNewGame()
        {
            var tileList = new List<Tile>();
            Random r = new Random();
            for (int i = 0; i < 12; i++)
            {
                double randomValue = 0;
                if (r.Next(1,4) != 1)
                {
                    randomValue = (r.NextDouble() + 0.5) * 2;
                }

                var tile = new Tile()
                {
                    TileIndex = i,
                    Visible = false,
                    Value = randomValue.ToString("N2")
                };

                tileList.Add(tile);
            }
            return tileList;
        }

        /*
        * There are MANY other helpers you may want to create here.  I've created some
        *  placeholder as well as hints for others you may find useful:
        *
        * 
        * HINT: Remember that your HttpContext is not available in this class so you may
        * need to pass it into methods that need it!
        * 
        */


        // - GetCurrentGame - If there is no current game state in session Generate a New Game
        // and store it in session, otherwise deserialize the List<Tile> from session
        public static List<Tile> GetCurrentGame(HttpContext httpContext)
        {
            var tileList = new List<Tile>();

            var tileListJson = httpContext.Session.GetString("tile-list");

            if (string.IsNullOrWhiteSpace(tileListJson))
            {
                return tileList;
            }
            else
            {
                tileList = JsonConvert.DeserializeObject<List<Tile>>(tileListJson);
            }
            return tileList;
        }

        // - SaveCurrentGame - Save the current state of the game to session. 
        public static void SaveCurrentGame(HttpContext httpContext, List<Tile> tile)
        {
            httpContext.Session.SetString("tile-list", JsonConvert.SerializeObject(tile));
        }

       /* - PickATileAndUpdateGame - code that contains the game logic as 
        * mentioned in Part 4 of the assignment. Hint: you'll need to pass in the
        * id of the selected tile as one of the parameters.
        */
        public static void PickTileAndUpdateGame(HttpContext httpContext, int id, List<Tile> tile, double multiplier)
        {
            double currentBet = CoinsHelper.GetCurrentBet(httpContext);
            double newBet = currentBet * multiplier;

            CoinsHelper.SaveCurrentBet(httpContext, newBet);

            if (multiplier == 0.00)
            {
                for (int i = 0; i < tile.Count; i++)
                {
                    tile[i].Visible = true;
                }
                SaveCurrentGame(httpContext, tile);
            }
            else
            {
                tile[id].Visible = true;
                for (int i = 0; i < tile.Count; i++ )
                {
                    if (tile[i] != tile[id])
                    {
                        tile[i].Value = (double.Parse(tile[i].Value) * 2).ToString("N2");
                    }
                }
                SaveCurrentGame(httpContext, tile);
            }
        }

        // - ClearCurrentGame - clear the current game state from session
        public static void ClearCurrentGame(HttpContext httpContext)
        {
            httpContext.Session.Remove("tile-list");
        }

        //- Finally, methods to serialize and deserialzie the Tile list.
        public static void SerializeTileList(HttpContext httpContext)
        {
            var tileListJson = httpContext.Session.GetString("tile-list");

            List<Tile> tiles = new List<Tile>();

            if (string.IsNullOrWhiteSpace(tileListJson))
            {
                tileListJson = JsonConvert.SerializeObject(tiles);
                httpContext.Session.SetString("tile-list", tileListJson);
            }
        }

        public static List<Tile> DeserializeTileList(ISession session) =>
            session.GetObject<List<Tile>>("tile-list") ?? new List<Tile>();
        

        public static void SavePlayerName(HttpContext httpContext, string name)
        {
            httpContext.Response.Cookies.Append("playerName", name, options);
        }

        public static string GetPlayerName(HttpContext httpContext)
        {
            return httpContext.Request.Cookies["playerName"];
        }
    }
}
