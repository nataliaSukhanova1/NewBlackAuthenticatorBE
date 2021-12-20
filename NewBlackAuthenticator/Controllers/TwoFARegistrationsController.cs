using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBlackAuthenticator.Data;
using NewBlackAuthenticator.Models;

namespace NewBlackAuthenticator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwoFARegistrationsController : ControllerBase
    {
        private readonly NBADBcontext _context;

        public TwoFARegistrationsController(NBADBcontext context)
        {
            _context = context;
        }
        private readonly static Random random = new Random();


        public string RandomTokenGenerator(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string (Enumerable.Repeat(characters, length).Select(s => s[random.Next(s.Length)]).ToArray());
         }

    // GET: api/TwoFARegistrations
    [HttpGet]
        public async Task<ActionResult<IEnumerable<TwoFARegistration>>> GetTwoFARegistrations()
        {
            string token = RandomTokenGenerator(32);
            Console.WriteLine(token);
            return await _context.TwoFARegistrations.ToListAsync();
            
        }

        // GET: api/TwoFARegistrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TwoFARegistration>> GetTwoFARegistration(string id)
        {
            var twoFARegistration = await _context.TwoFARegistrations.FindAsync(id);

            if (twoFARegistration == null)
            {
                return NotFound();
            }

            return twoFARegistration;
        }

        // PUT: api/TwoFARegistrations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTwoFARegistration(string id, TwoFARegistration twoFARegistration)
        {
            if (id != twoFARegistration.Id)
            {
                return BadRequest();
            }

            _context.Entry(twoFARegistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TwoFARegistrationExists(id))
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

        // POST: api/TwoFARegistrations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TwoFARegistration>> PostTwoFARegistration(TwoFARegistration twoFARegistration)
        {
            string token = RandomTokenGenerator(32);
            twoFARegistration.Token = token;

            _context.TwoFARegistrations.Add(twoFARegistration);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TwoFARegistrationExists(twoFARegistration.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTwoFARegistration", new { id = twoFARegistration.Id }, twoFARegistration);
        }

        // DELETE: api/TwoFARegistrations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TwoFARegistration>> DeleteTwoFARegistration(string id)
        {
            var twoFARegistration = await _context.TwoFARegistrations.FindAsync(id);
            if (twoFARegistration == null)
            {
                return NotFound();
            }

            _context.TwoFARegistrations.Remove(twoFARegistration);
            await _context.SaveChangesAsync();

            return twoFARegistration;
        }

        private bool TwoFARegistrationExists(string id)
        {
            return _context.TwoFARegistrations.Any(e => e.Id == id);
        }

        //api/TwoFARegistrations
        [HttpPost("UpdateTwoFARegistrationStatus")]
        public ActionResult<TwoFARegistration> UpdateTwoFARegistrationStatus(UpdateTwoFARegistrationStatusRequest updateStatusReguest)
        {
            var token = updateStatusReguest.Token;
            var userAnswer = updateStatusReguest.UserAnswer;

                var twoFAReg = _context.TwoFARegistrations
                .Where(r => r.Token == token)
                .FirstOrDefault();

            // how to use enum here??? // 
            if (twoFAReg.Status == "pending" && userAnswer == "Yes")
            {
                twoFAReg.Status = "approved";
                _context.SaveChangesAsync();
                return Ok();
            }
            else if(twoFAReg.Status == "pending" && userAnswer == "No")
            {
                twoFAReg.Status = "rejected";
                _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("CheckTwoFARegistrationStatus")]
        public ActionResult<string> CheckTwoFARegistrationStatus(UpdateTwoFARegistrationStatusRequest updateStatusReguest)
        {
            var token = updateStatusReguest.Token;

            var twoFAReg = _context.TwoFARegistrations
                .Where(r => r.Token == token)
                .FirstOrDefault();

            if (twoFAReg.Status == "redeemed")
            {
                return NotFound(); 
            }
            else if (twoFAReg.Status == "pending")
            {
                return Ok("pending");
            }
            else if (twoFAReg.Status == "rejected")
            {
                twoFAReg.Status = "redeemed";
                _context.SaveChangesAsync();
                return Ok("rejected");
            }
            else if (twoFAReg.Status == "approved")
            {
                twoFAReg.Status = "redeemed";
                _context.SaveChangesAsync();
                return Ok("success");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
