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

    public class EmployeesController : ControllerBase
    {
        private readonly RestAPIContext _context;

        public EmployeesController(RestAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> CheckIfEmployee()
        {
            return await _context.employees.ToListAsync();
        }

        //-----------------------------------------------------------------------------------------\\

        // GET: api/Employees/valid/{email}
        [HttpGet("{email}")]
        public Boolean CheckIfEmployee(string email)
        {
            var employees = _context.employees.ToList();
            var isValid = false;

            foreach (Employee employee in employees)
            {
                if (employee.email == email)
                {
                    isValid = true;
                }
            }

            return isValid;
        }





        private bool EmployeesExists(string email)
        {
            return _context.employees.Any(e => e.email == email);
        }
    }
}