using DempAPI.DbContexts;
using DempAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DempAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPatch("{employeeId}")]
        public IActionResult Patch(int employeeId, [FromBody] JsonPatchDocument<Employee> patch)
        {
            var fromDb = _context.Employee.FirstOrDefault(e => e.Id == employeeId);

            patch.ApplyTo(fromDb, ModelState);

            var isValid = TryValidateModel(fromDb);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            _context.Update(fromDb);
            _context.SaveChanges();
            var model = new
            {
                patched = fromDb
            };
            return Ok(model);

            /*//TODO: Comment or uncomment this block.
            The JsonPatchDocument to use for testing out the method
            {
               "op": "replace",       
               "path": "/email",       
               "value": "Test@gmail.com"
            }
           /*/
        }
    }
}
