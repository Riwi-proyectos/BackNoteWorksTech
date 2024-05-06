using BackNoteWorksTech.Data;
using BackNoteWorksTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackNoteWorksTech
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriesController : Controller
    {
        public readonly BaseContext _context;

        public CategoriesController (BaseContext context)
        {
            _context = context;
        }

        // Fincion listar
        [HttpGet]
        public async Task <ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // Funcion detalles
        [HttpGet ("{id}")]
        public async Task <ActionResult<Category>> GetCategories(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // Funcion crear
        [HttpPost]
        public async Task <ActionResult<Category>> PostCategory ([Bind("Name")] Category data)
        {
            Category category = new Category(){
                Name = data.Name,
                UpdateDate = DateTime.Now,
                Status = "Activo"
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCategories", new {id = category.Id}, category);
        }

        // Funcion eliminarr
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Funcion actualizar
        [HttpPut("{id}")]

        public async Task<IActionResult> updateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var existingCategory = await _context.Categories.FindAsync(id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            // Actualiza las propiedades de la entidad con los valores del objeto notework
            existingCategory.Name = category.Name;
            existingCategory.Status = category.Status;
            
            try 
            {
                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Maneja excepciones de concurrencia si es necesario
                throw;
            }

            return NoContent();
        }
    }
}