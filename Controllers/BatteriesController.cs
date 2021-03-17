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
    public class BatteriesController : ControllerBase
    {
        private readonly RestAPIContext _context;

        public BatteriesController(RestAPIContext context)
        {
            _context = context;
        }

        // GET: api/Batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
        {
            return await _context.batteries.ToListAsync();
        }

        // GET: api/Batteries/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Battery>> GetBattery(long id)
        // {
        //     var battery = await _context.batteries.FindAsync(id);

        //     if (battery == null)
        //     {
        //         return NotFound();
        //     }

        //     return battery;
        // }

//----------------------------------- Retrieving the current status of a specific Battery -----------------------------------\\
        
        // GET: api/Batteries/id/Status
        [HttpGet("{id}/Status")]
        public async Task<ActionResult<string>> GetBatteryStatus([FromRoute] long id)
        {
            var battery = await _context.batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return Content("The status of battery " + battery.id + " is: " + battery.status);
        }

//----------------------------------- Changing the status of a specific Battery -----------------------------------\\
         
        // PUT: api/Batteries/id/Status        
        [HttpPut("{id}/Status")]
        public async Task<IActionResult> PutBattery([FromRoute] long id, Battery battery)
        {
            if (id != battery.id)
            {
                return BadRequest();
            }
            
            if (battery.status == "Active" || battery.status == "Inactive" || battery.status == "Intervention")
            {
                _context.Entry(battery).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return Content("Battery: " + battery.id + ", status as been change to: " + battery.status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BatteryExists(id))
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

        // POST: api/Batteries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Battery>> PostBattery(Battery battery)
        // {
        //     _context.batteries.Add(battery);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetBattery", new { id = battery.id }, battery);
        // }

        // DELETE: api/Batteries/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteBattery(long id)
        // {
        //     var battery = await _context.batteries.FindAsync(id);
        //     if (battery == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.batteries.Remove(battery);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        private bool BatteryExists(long id)
        {
            return _context.batteries.Any(e => e.id == id);
        }
    }
}
