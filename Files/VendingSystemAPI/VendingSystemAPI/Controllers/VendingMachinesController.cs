using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingSystemAPI.Models;

namespace VendingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendingMachinesController : ControllerBase
    {
        private readonly VendingSystemDbContext _context;

        public VendingMachinesController(VendingSystemDbContext context)
        {
            _context = context;
        }

        // GET: api/VendingMachines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendingMachine>>> GetVendingMachines()
        {
            // Обязательно Include, чтобы подтянулись названия компаний и статусов!
            return await _context.VendingMachines
                                 .Include(v => v.Company) // <-- Эта строка нужна
                                 .Include(v => v.Status)  // <-- И эта строка нужна
                                 .ToListAsync();
        }

        //// GET: api/VendingMachines
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<VendingMachine>>> GetVendingMachines()
        //{
        //    return await _context.VendingMachines.ToListAsync();
        //}

        // GET: api/VendingMachines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VendingMachine>> GetVendingMachine(int id)
        {
            var vendingMachine = await _context.VendingMachines.FindAsync(id);

            if (vendingMachine == null)
            {
                return NotFound();
            }

            return vendingMachine;
        }

        // PUT: api/VendingMachines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendingMachine(int id, VendingMachine vendingMachine)
        {
            if (id != vendingMachine.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendingMachine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendingMachineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VendingMachines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VendingMachine>> PostVendingMachine(VendingMachine vendingMachine)
        {
            _context.VendingMachines.Add(vendingMachine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendingMachine", new { id = vendingMachine.Id }, vendingMachine);
        }

        // DELETE: api/VendingMachines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendingMachine(int id)
        {
            var vendingMachine = await _context.VendingMachines.FindAsync(id);
            if (vendingMachine == null)
            {
                return NotFound();
            }

            _context.VendingMachines.Remove(vendingMachine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendingMachineExists(int id)
        {
            return _context.VendingMachines.Any(e => e.Id == id);
        }
    }
}
