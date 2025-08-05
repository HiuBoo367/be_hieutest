using HieuTest.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HieuTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomsAnswerController : ControllerBase
    {
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
                Content = entity.Content
            };
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateCustomsAnswer([FromBody] CustomsAnswer dto)
        {
            var manager = new CustomsAnswerOptionManager();
            var entity = new CustomsAnswer
            {
                Id = Guid.NewGuid().ToString(),
                QuestionId = dto.QuestionId,
                Content = dto.Content
            };
            manager.Insert(entity); // Giả sử bạn đã có hàm Insert trong Manager
            return CreatedAtAction(nameof(GetCustomsAnswerOptionById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomsAnswer(string id, [FromBody] CustomsAnswer dto)
        {
            var manager = new CustomsAnswerOptionManager();
            var entity = manager.SelectOne(id);
            if (entity == null)
                return NotFound();

            entity.QuestionId = dto.QuestionId;
            entity.Content = dto.Content;
            manager.Update(entity); // Giả sử bạn đã có hàm Update trong Manager
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomsAnswer(string id)
        {
            var manager = new CustomsAnswerOptionManager();
            var entity = manager.SelectOne(id);
            if (entity == null)
                return NotFound();

            manager.Delete(id); // Giả sử bạn đã có hàm Delete trong Manager
            return NoContent();
        }





    }
}
