using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SmartHomeAPI.Models;

namespace SmartHomeAPI.Controllers
{
    public class TemperaturesAPIController : ApiController
    {
        private TemperatureEntities db = new TemperatureEntities();

        // GET: api/TemperaturesAPI
        public IQueryable<Temperatures> GetTemperatures()
        {
            return db.Temperatures;
        }

        // GET: api/TemperaturesAPI/5
        [ResponseType(typeof(Temperatures))]
        public async Task<IHttpActionResult> GetTemperatures(int id)
        {
            Temperatures temperatures = await db.Temperatures.FindAsync(id);
            if (temperatures == null)
            {
                return NotFound();
            }

            return Ok(temperatures);
        }

        // PUT: api/TemperaturesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTemperatures(int id, Temperatures temperatures)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temperatures.Id)
            {
                return BadRequest();
            }

            db.Entry(temperatures).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemperaturesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TemperaturesAPI
        [ResponseType(typeof(Temperatures))]
        public async Task<IHttpActionResult> PostTemperatures(Temperatures temperatures)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Temperatures.Add(temperatures);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = temperatures.Id }, temperatures);
        }

        // DELETE: api/TemperaturesAPI/5
        [ResponseType(typeof(Temperatures))]
        public async Task<IHttpActionResult> DeleteTemperatures(int id)
        {
            Temperatures temperatures = await db.Temperatures.FindAsync(id);
            if (temperatures == null)
            {
                return NotFound();
            }

            db.Temperatures.Remove(temperatures);
            await db.SaveChangesAsync();

            return Ok(temperatures);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TemperaturesExists(int id)
        {
            return db.Temperatures.Count(e => e.Id == id) > 0;
        }
    }
}