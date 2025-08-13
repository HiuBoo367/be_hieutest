using HieuTest.Manager;
using Microsoft.AspNetCore.Mvc;
using App.Xuatnhapcanh.Dal.EntityClasses;

namespace HieuTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomsAnswerController : ControllerBase
    {
        // GET api/CustomsAnswer
        [HttpGet]
        public IActionResult GetAll()
        {
            var manager = new CustomsAnswerOptionManager();
            var entities = manager.SelectAll();

            var result = entities.Select(e => new CustomsAnswer
            {
                Id = e.Id,
                QuestionId = e.QuestionId,
                Content = e.Content
            }).ToList();

            return Ok(result);
        }

        // GET api/CustomsAnswer/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var manager = new CustomsAnswerOptionManager();
            var entity = manager.SelectOne(id); // dùng id truyền vào

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

        

        // POST api/CustomsAnswer
        [HttpPost]
        public IActionResult Create([FromBody] CustomsAnswer model)
        {
            if (model == null)
                return BadRequest("Payload rỗng.");

            if (string.IsNullOrWhiteSpace(model.QuestionId) || string.IsNullOrWhiteSpace(model.Content))
                return BadRequest("Thiếu dữ liệu bắt buộc: QuestionId, Content.");

            var manager = new CustomsAnswerOptionManager();

            var entity = new CustomsAnswerOptionEntity
            {
                Id = string.IsNullOrWhiteSpace(model.Id) ? Guid.NewGuid().ToString() : model.Id,
                QuestionId = model.QuestionId,
                Content = model.Content
            };

            var ok = manager.Insert(entity);
            if (!ok) return StatusCode(500, "Insert thất bại.");

            var dto = new CustomsAnswer
            {
                Id = entity.Id,
                QuestionId = entity.QuestionId,
                Content = entity.Content
            };

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, dto);
        }

        // PUT api/CustomsAnswer/{id}
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] CustomsAnswer model)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Thiếu id.");

            if (model == null)
                return BadRequest("Payload rỗng.");

            var manager = new CustomsAnswerOptionManager();
            var existing = manager.SelectOne(id);

            if (existing == null || string.IsNullOrEmpty(existing.Id))
                return NotFound();

            // cập nhật các trường cho entity
            existing.QuestionId = string.IsNullOrWhiteSpace(model.QuestionId) ? existing.QuestionId : model.QuestionId;
            existing.Content = string.IsNullOrWhiteSpace(model.Content) ? existing.Content : model.Content;

            var ok = manager.Update(existing);
            if (!ok) return StatusCode(500, "Update thất bại.");

            return NoContent();
        }

        // DELETE api/CustomsAnswer/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Thiếu id.");

            var manager = new CustomsAnswerOptionManager();
            var existing = manager.SelectOne(id);
            if (existing == null || string.IsNullOrEmpty(existing.Id))
                return NotFound();

            var ok = manager.Delete(id);
            if (!ok) return StatusCode(500, "Delete thất bại.");

            return NoContent();
        }
    }
}