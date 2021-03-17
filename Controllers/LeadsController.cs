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
    public class LeadsController : ControllerBase
    {
        private readonly RestAPIContext _context;

        public LeadsController(RestAPIContext context)
        {
            _context = context;
        }

        // DateTime today = DateTime.Today;
        // DateTime DaysAgo = DateTime.Today.AddDays(-30);
        // GET: api/Leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lead>>> GetLeads()
        {
            return await _context.leads.ToListAsync();
        }

        
        // GET: api/Leads/5
        [HttpGet("30days")]
        public async Task<ActionResult<IEnumerable<Lead>>> GetLead()
        {
            // var lead = await _context.leads.Where(leads => leads.created_at >= DaysAgo).ToListAsync();

            // if (lead == null)
            // {
            //     return NotFound();
            // }

            return await _context.leads.Where(leads => leads.created_at >= DateTime.Today.AddDays(-30)).ToListAsync();
            // return lead;
        }

        
    }
}
