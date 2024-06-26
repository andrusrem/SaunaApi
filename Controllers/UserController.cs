using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SaunaApi.Models;

namespace SaunaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SaunaApiDbContext _context;

        public UserController(SaunaApiDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, UpdateUser updateUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (id != user.Id)
            {
                return BadRequest();
            }
            if(updateUser.Email != null)
            {
                user.Email = updateUser.Email;
            }
            if(updateUser.Password != null)
            {
                user.Password = updateUser.Password;
            }

            _context.Entry(user).State = EntityState.Modified;
            var response = new ResponseUser();
            response.Username = user.Username;
            response.Email = user.Email;
            response.Firstname = user.Firstname;
            response.Lastname = user.Lastname;
            response.Password = user.Password;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content(response.ToJson());
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(RegisterUser registerUser)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'SaunaApiDbContext.Users'  is null.");
            }

            var user = new User();
            user.Username = registerUser.Username;
            user.Email = registerUser.Email;
            user.Firstname = registerUser.Firstname;
            user.Lastname = registerUser.Lastname;
            user.Password = registerUser.Password;
            user.Access_token = GenerateToken();
            user.Created_at = DateTime.Now;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
        // POST: api/User/get-token
        [HttpPost("{get-token}")]
        public async Task<ActionResult<User>> PostUser(Login login)
        {
            var response = new ResponseUser();
            if (_context.Users == null)
            {
                return Problem("Entity set 'SaunaApiDbContext.Users'  is null.");
            }

            var user = _context.Users.Where(user => user.Username == login.Username).FirstOrDefault();
            if(user == null || user.Password != login.Password)
            {
                return Problem("Username or Password is invalid, please try again.");
            }
            response.Access_token = user.Access_token;
            return Content(response.ToJson());
        }


        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public string GenerateToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }
    }
}
