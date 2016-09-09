using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SeaChess.Data;

namespace SeaChess.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        // Get the current game board or create a new one
        [Route("/game/getgameboard/{clientState?}")]
        public IActionResult GetGameBoard([FromServices]ApplicationDbContext appContext, string clientState)
        {
            var startedGame = appContext.GameBoards.Where(game => game.State != GameState.Finished).OrderByDescending(game => game.CreatedOn).FirstOrDefault();

            if (startedGame == null)
            {
                startedGame = new Models.GameBoard()
                {
                    CreatedOn = DateTime.Now,
                    State = GameState.New,
                    Data = "GGGGGGGGGGGGGGGGGGGGGGGGGGG" // Empty board
                };

                appContext.GameBoards.Add(startedGame);
            }
            else if (!string.IsNullOrWhiteSpace(clientState))
            {
                startedGame.Data = clientState;
            }
            appContext.SaveChanges();

            return Content(startedGame.Data);
        }

        public IActionResult EndGame([FromServices]ApplicationDbContext appContext)
        {
            var startedGame = appContext.GameBoards.Where(game => game.State != GameState.Finished).OrderByDescending(game => game.CreatedOn).FirstOrDefault();

            if (startedGame != null)
            {
                startedGame.State = GameState.Finished;
                appContext.SaveChanges();
            }
            return Content("OK");
        }
    }
}
