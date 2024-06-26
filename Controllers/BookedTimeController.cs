using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaunaApi.Models;

namespace SaunaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookedTimeController : ControllerBase
    {
        private readonly SaunaApiDbContext _context;

        public BookedTimeController(SaunaApiDbContext context)
        {
            _context = context;
        }

        // GET: api/BookedTime
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookedTime>>> GetBookedTimes()
        {
          if (_context.BookedTimes == null)
          {
              return NotFound();
          }
            return await _context.BookedTimes.ToListAsync();
        }

        // GET: api/BookedTime/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookedTime>> GetBookedTime(int id)
        {
          if (_context.BookedTimes == null)
          {
              return NotFound();
          }
            var bookedTime = await _context.BookedTimes.FindAsync(id);

            if (bookedTime == null)
            {
                return NotFound();
            }

            return bookedTime;
        }

        // PUT: api/BookedTime/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<BookedTime>> PutBookedTime(int id, BookedTimePut bookedTimePut, string access_token)
        {
            var bookedTime = await _context.BookedTimes.FindAsync(id);
            var user = _context.Users.Where(user => user.Access_token == access_token).FirstOrDefault();
            if(bookedTime.User_id != user.Id)
            {
                return BadRequest();
            }
            if (id != bookedTime.Id)
            {
                return BadRequest();
            }
            
            _context.Entry(bookedTime).State = EntityState.Modified;
            bookedTime.Booked_time = bookedTimePut.Booked_time;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookedTimeExists(id))
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

        // POST: api/BookedTime
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookedTime>> PostBookedTime(BookedTimePost bookedTimePost)
        {
            var bookedTime = new BookedTime();
            if (_context.BookedTimes == null)
            {
                return Problem("Entity set 'SaunaApiDbContext.BookedTimes'  is null.");
            }
            var user = await _context.Users.FindAsync(bookedTimePost.User_id);
            var checkTime = _context.BookedTimes.Where(time => time.Booked_time == bookedTimePost.Booked_time).FirstOrDefault();
            if(user != null && checkTime == null)
            {
                bookedTime.User_id = bookedTimePost.User_id;
                bookedTime.Booked_time = bookedTimePost.Booked_time;
                _context.BookedTimes.Add(bookedTime);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBookedTime", new { id = bookedTime.Id }, bookedTime);
            }
            else if(user == null)
            {
                return Problem("Not registered user, login or register first.");
            }
            return Problem("This time is already booked");
        }

        // DELETE: api/BookedTime/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookedTime>> DeleteBookedTime(BookedTimeDelete bookedTimeDelete)
        {
            if (_context.BookedTimes == null)
            {
                return NotFound();
            }
            var user = _context.Users.Where(user => user.Access_token == bookedTimeDelete.Access_token).FirstOrDefault();
            var bookedTime = await _context.BookedTimes.FindAsync(bookedTimeDelete.Id);
            if(user == null)
            {
                return Problem("No such user.");
            }
            
            if (bookedTime == null)
            {
                return Problem("No such time.");
            }
            if(user.Id != bookedTime.User_id)
            {
                return Problem("No such time related with this user.");
            }
            _context.BookedTimes.Remove(bookedTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookedTimeExists(int id)
        {
            return (_context.BookedTimes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
