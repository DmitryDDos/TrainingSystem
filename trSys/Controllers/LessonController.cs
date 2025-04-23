using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trSys.Interfaces;
using trSys.Models;

namespace trSys.Controllers
{
    [ApiController]
    [Route("api/modules/{moduleId}/[controller]")]
    [Authorize(Roles = "Admin")]
    public class LessonController : BaseController<Lesson>
    {
        public LessonController(IRepository<Lesson> lessonRepository) : base(lessonRepository)
        {
        }

        // Переопределяем методы для более строгой авторизации
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override Task<ActionResult<Lesson>> Create(Lesson entity)
            => base.Create(entity);

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public override Task<IActionResult> Update(int id, Lesson entity)
            => base.Update(id, entity);

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override Task<IActionResult> Delete(int id)
            => base.Delete(id);
    }
}
