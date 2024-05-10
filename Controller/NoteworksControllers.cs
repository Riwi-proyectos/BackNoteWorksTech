

using BackNoteWorksTech.Data;
using BackNoteWorksTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace BackNoteWorksTech
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteworksController : Controller
    {
        public readonly BaseContext _context; 

        public NoteworksController (BaseContext context)
        {
            _context = context;
        }

        // Fincion listar
        [HttpGet]
        public async Task <ActionResult<IEnumerable<NoteWork>>> GetNoteWorks()
        {
            return await _context.NoteWorks.ToListAsync();
        }

        // Funcion detalles
        [HttpGet ("{id}")]
        public async Task <ActionResult<NoteWork>> GetNoteWork(int id)
        {
            var notework = await _context.NoteWorks.FindAsync(id);

            if (notework == null)
            {
                return NotFound();
            }

            return notework;
        }
        
        // Funcion crear
        [HttpPost]
        public async Task <ActionResult<NoteWork>> PostNoteWork([Bind("title,Content,CategorieId")]NoteWork notework)
        {
            notework.Status = "Activo";
            notework.UpdateDate = DateTime.Now;
            
            _context.NoteWorks.Add(notework);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetNoteWorks", new {id = notework.Id}, notework);
        }

        // Funcion eliminar
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotework(int id)
        {
            var notework = await _context.NoteWorks.FindAsync(id);

            if (notework == null)
            {
                return NotFound();
            }
            notework.Status = "Inactivo";

            _context.NoteWorks.Update(notework);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetNoteWorks", new {id = notework.Id}, notework);
        }

        // Funcion actualizar
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotework(int id, NoteWork notework)
        {

            var existingNotework = await _context.NoteWorks.FindAsync(id);

            if (existingNotework == null)
            {
                return NotFound();
            }

            // Actualiza las propiedades de la entidad con los valores del objeto notework
            existingNotework.Title = notework.Title;
            existingNotework.Content = notework.Content;
            // Actualiza más propiedades según sea necesario

            try
            {
                _context.NoteWorks.Update(notework);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Maneja excepciones de concurrencia si es necesario
                throw;
            }

            return CreatedAtAction("GetNoteWork", new {id = existingNotework.Id}, existingNotework);
        }
    


        [HttpPut("changeStatus/{id}")]
        public async Task<IActionResult> UpdateNotework(int id)
        {
            var existingNotework = await _context.NoteWorks.FindAsync(id);
            if (existingNotework == null)
            {
                return NotFound();
            }

            existingNotework.Status = "Oculto";    

            try
            {            
                _context.NoteWorks.Update(existingNotework);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return CreatedAtAction("GetNoteWork", new {id = existingNotework.Id}, existingNotework);
        }
    }

}