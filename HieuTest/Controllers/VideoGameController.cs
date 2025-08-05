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

        [HttpGet("{id}")]
        public IActionResult GetCustomsAnswerOptionById(string id)
        {
            CustomsAnswerOptionManager manager = new CustomsAnswerOptionManager();
            var entity = manager.SelectOne("5912a5f2-4978-4d8e-bae0-a06abcb5a228");

            // Có thể entity rỗng nếu không tìm thấy
            if (entity == null || string.IsNullOrEmpty(entity.Id))
                return NotFound();

            var dto = new CustomsAnswer
            {
                Id = entity.Id,
                QuestionId = entity.QuestionId,
                Content= entity.Content
            };
            return Ok(dto);
        }



        //[HttpPost]
        //public ActionResult<VideoGame> AddVideoGame(VideoGame newGame)
        //{
        //    if (newGame is null)
        //        return BadRequest();
        //    newGame.Id = videoGames.Max(g => g.Id) + 1;
        //    videoGames.Add(newGame);
        //    return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame);

        //}


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
