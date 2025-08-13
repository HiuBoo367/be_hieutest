using App.Xuatnhapcanh.Dal.EntityClasses;
using HieuTest.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HieuTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames = new List<VideoGame>
        {
            new VideoGame { Id = 1, Title = "The Legend of Zelda: Breath of the Wild", Platform = "Nintendo Switch", Deverloper = "Nintendo", Publisher = "Nintendo" },
            new VideoGame { Id = 2, Title = "God of War", Platform = "PlayStation 4", Deverloper = "Santa Monica Studio", Publisher = "Sony Interactive Entertainment" },
            new VideoGame { Id = 3, Title = "Hiếu", Platform = "Multi-platform", Deverloper = "Mojang Studios", Publisher = "Xbox Game Studios" }
        };
        [HttpGet]
        public ActionResult<List<VideoGame>> GetVideoGames()
        {
            return Ok(videoGames);
        }

        // GET api/videogame/5
        [HttpGet("{id:int}")]
        public ActionResult<VideoGame> GetVideoGameById(int id)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id);
            return game is null ? NotFound() : Ok(game);
        }

        // POST api/videogame
        [HttpPost]
        public ActionResult<VideoGame> CreateVideoGame([FromBody] VideoGame newGame)
        {
            if (newGame is null)
                return BadRequest("Body không được rỗng.");

            if (string.IsNullOrWhiteSpace(newGame.Title) ||
                string.IsNullOrWhiteSpace(newGame.Platform))
                return BadRequest("Title và Platform là bắt buộc.");

            // Sinh Id mới (tự tăng)
            var nextId = videoGames.Count == 0 ? 1 : videoGames.Max(g => g.Id) + 1;
            newGame.Id = nextId;

            videoGames.Add(newGame);

            // Trả 201 Created + Location trỏ tới GET theo id
            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);
        }



        [HttpPut("{id}")]
        public IActionResult UpdateVideoGame(int id, VideoGame updatedGame)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id);
            if (game is null)
                return NotFound();

            game.Title = updatedGame.Title;
            game.Platform = updatedGame.Platform;
            game.Deverloper = updatedGame.Deverloper;
            game.Publisher = updatedGame.Publisher;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVideoGame(int id)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id);
            if (game is null)
                return NotFound();

            videoGames.Remove(game);
            return NoContent();
        }
    }
}
