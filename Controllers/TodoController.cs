using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

#region snippet_TodoController
namespace TodoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        #endregion snippet_TodoController

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.Todos.Count() == 0)
            {
                _context.Todos.AddRange(
                    new TodoItem { Name = "Install SDK/CLI Tools" },
                    new TodoItem { Name = "Install Runtime" },
                    new TodoItem { Name = "Install VS Code (important because open-source, cross platform development)" },
                    new TodoItem { Name = "Learn asp.net core" },
                    new TodoItem { Name = "Give out free lessons on asp.net core" }
                );
                _context.SaveChanges();
            }
        }

        #region snippet_GetAll
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return _context.Todos.ToList();
        }
        #endregion snippet_GetAll

        #region snippet_GetById
        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = _context.Todos.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        #endregion snippet_GetById

        #region snippet_Create
        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        #region snippet_CreateActionAttributes
        [ProducesResponseType(201)]     // Created
        [ProducesResponseType(400)]     // BadRequest
        #endregion snippet_CreateActionAttributes
        #region snippet_CreateAction
        [HttpPost]
        public ActionResult<TodoItem> Create([FromBody]TodoItem item)
        {
            _context.Todos.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }
        #endregion snippet_CreateAction
        #endregion snippet_Create

        #region snippet_Update
        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.Todos.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }
        #endregion snippet_Update

        #region snippet_Delete
        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
        #endregion snippet_Delete
    }
}
