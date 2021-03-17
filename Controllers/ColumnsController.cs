using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly RestAPIContext _context;

        public ColumnsController(RestAPIContext context)
        {
            _context = context;
        }

        // GET: api/Columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Column>>> GetColumns()
        {
            return await _context.columns.ToListAsync();
        }

        // GET: api/Columns/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Column>> GetColumn(long id)
        // {
        //     var column = await _context.columns.FindAsync(id);

        //     if (column == null)
        //     {
        //         return NotFound();
        //     }

        //     return column;
        // }

//----------------------------------- Retrieving the current status of a specific Column -----------------------------------\\
        
        // GET: api/Columns/id/Status
        [HttpGet("{id}/Status")]
        public async Task<ActionResult<Column>> GetColumnStatus([FromRoute] long id)
        {
            var column = await _context.columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            return Content("The status of column " + column.id + " is: " + column.status);
        }


//----------------------------------- Changing the status of a specific Column -----------------------------------\\
        
        // PUT: api/Columns/id/Status        
        [HttpPut("{id}/Status")]
        public async Task<IActionResult> PutColumn([FromRoute] long id, Column column)
        {
            if (id != column.id)
            {
                return BadRequest();
            }
            
            if (column.status == "Active" || column.status == "Inactive" || column.status == "Intervention")
            {
                _context.Entry(column).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return Content("Column: " + column.id + ", status as been change to: " + column.status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColumnExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Content("Valid status: Intervention, Inactive, Active. Try again!  ");
        }

        
        // POST: api/Columns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Column>> PostColumn(Column column)
        // {
        //     _context.columns.Add(column);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetColumn", new { id = column.id }, column);
        // }

        // DELETE: api/Columns/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteColumn(long id)
        // {
        //     var column = await _context.columns.FindAsync(id);
        //     if (column == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.columns.Remove(column);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        private bool ColumnExists(long id)
        {
            return _context.columns.Any(e => e.id == id);
        }
    }
}
