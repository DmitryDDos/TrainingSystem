using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using trSys.Interfaces;

namespace trSys.Controllers
{
    public class BaseController<TEntity> : Controller where TEntity : class, IEntity
    {
        protected readonly IRepository<TEntity> _repository;

        public BaseController(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        // GET: /[controller]
        [HttpGet]
        public virtual async Task<IActionResult> Index()
        {
            var entities = await _repository.GetAllAsync();
            return View(entities);
        }

        // GET: /[controller]/Details/5
        [HttpGet("Details/{id}")]
        public virtual async Task<IActionResult> Details(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        // GET: /[controller]/Create
        [HttpGet("Create")]
        public virtual IActionResult Create()
        {
            return View();
        }

        // POST: /[controller]/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(entity);
                return RedirectToAction(nameof(Details), new { id = entity.Id });
            }
            return View(entity);
        }

        // GET: /[controller]/Edit/5
        [HttpGet("Edit/{id}")]
        public virtual async Task<IActionResult> Edit(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        // POST: /[controller]/Edit/5
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(entity);
                return RedirectToAction(nameof(Details), new { id = entity.Id });
            }
            return View(entity);
        }

        // GET: /[controller]/Delete/5
        [HttpGet("Delete/{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View(entity);
        }

        // POST: /[controller]/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
