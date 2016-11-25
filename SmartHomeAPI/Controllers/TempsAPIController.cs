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
    public class TempsAPIController : ApiController
    {
        private SmartHomeTempEntities db = new SmartHomeTempEntities();

        // GET: api/TempsAPI
        public IQueryable<Temp> GetTemp()
        {
            return db.Temp;
        }

        // GET: api/TempsAPI/5
        [ResponseType(typeof(Temp))]
        public async Task<IHttpActionResult> GetTemp(int id)
        {
            Temp temp = await db.Temp.FindAsync(id);
            if (temp == null)
            {
                return NotFound();
            }

            return Ok(temp);
        }

        // PUT: api/TempsAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTemp(int id, Temp temp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != temp.Id)
            {
                return BadRequest();
            }

            db.Entry(temp).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempExists(id))
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

        // POST: api/TempsAPI
        [ResponseType(typeof(Temp))]
        public async Task<IHttpActionResult> PostTemp(Temp temp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Temp.Add(temp);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = temp.Id }, temp);
        }

        // DELETE: api/TempsAPI/5
        [ResponseType(typeof(Temp))]
        public async Task<IHttpActionResult> DeleteTemp(int id)
        {
            Temp temp = await db.Temp.FindAsync(id);
            if (temp == null)
            {
                return NotFound();
            }

            db.Temp.Remove(temp);
            await db.SaveChangesAsync();

            return Ok(temp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TempExists(int id)
        {
            return db.Temp.Count(e => e.Id == id) > 0;
        }
    }
}