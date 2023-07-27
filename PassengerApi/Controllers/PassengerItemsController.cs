using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassengerAPI.Models;

namespace PassengerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerItemsController : ControllerBase
    {
        private readonly PassengerContext _context;

        public PassengerItemsController(PassengerContext context)
        {
            _context = context;
        }

        // GET: api/PassengerItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PassengerItemDTO>>> GetPassengerItems()
        {
            return await _context.PassengerItems
              .Select(x => ItemToDTO(x))
              .ToListAsync();
        }

        // GET: api/PassengerItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PassengerItemDTO>> GetPassengerItem(long id)
        {
            var passengerItem = await _context.PassengerItems.FindAsync(id);

            if (passengerItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(passengerItem);
        }

        // PUT: api/PassengerItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassengerItem(long id, PassengerItemDTO passengerDTO)
        {
            if (id != passengerDTO.Id)
            {
                return BadRequest();
            }

            var passengerItem = await _context.PassengerItems.FindAsync(id);
            if (passengerItem == null)
            {
                return NotFound();
            }

            passengerItem.Id = passengerDTO.Id;
            passengerItem.LastName = passengerDTO.LastName;
            passengerItem.FirstName = passengerDTO.FirstName;
            passengerItem.Nationality = passengerDTO.Nationality;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PassengerItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/PassengerItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PassengerItemDTO>> PostPassengerItem(PassengerItemDTO passengerDTO)
        {
            var passengerItem = new PassengerItem
            {
                Id = passengerDTO.Id,
                LastName = passengerDTO.LastName,
                FirstName = passengerDTO.FirstName,
                Nationality = passengerDTO.Nationality,
            };

            _context.PassengerItems.Add(passengerItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPassengerItem),
                new { id = passengerItem.Id },
                ItemToDTO(passengerItem));
        }

        // DELETE: api/PassengerItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassengerItem(long id)
        {
            //if (_context.PassengerItems == null)
            //{
            //    return NotFound();
            // }
            var passengerItem = await _context.PassengerItems.FindAsync(id);
            if (passengerItem == null)
            {
                return NotFound();
            }

            _context.PassengerItems.Remove(passengerItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassengerItemExists(long id)
        {
            return (_context.PassengerItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static PassengerItemDTO ItemToDTO(PassengerItem passengerItem) =>
            new PassengerItemDTO
            {
                Id = passengerItem.Id,
                LastName = passengerItem.LastName,
                FirstName = passengerItem.FirstName,
                Nationality = passengerItem.Nationality,
            };
    }
}