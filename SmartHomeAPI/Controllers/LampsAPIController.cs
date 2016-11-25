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
    public class LampsAPIController : ApiController
    {
        private SmartHomeLampEntities db = new SmartHomeLampEntities();

        // GET: api/LampsAPI
        public IQueryable<Lamp> GetLamp()
        {
            return db.Lamp;
        }

        // GET: api/LampsAPI/5
        [ResponseType(typeof(Lamp))]
        public async Task<IHttpActionResult> GetLamp(int id)
        {
            Lamp lamp = await db.Lamp.FindAsync(id);
            if (lamp == null)
            {
                return NotFound();
            }

            return Ok(lamp);
        }

        // PUT: api/LampsAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLamp(int id, Lamp lamp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lamp.Id)
            {
                return BadRequest();
            }

            db.Entry(lamp).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LampExists(id))
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

        // POST: api/LampsAPI
        [ResponseType(typeof(Lamp))]
        public async Task<IHttpActionResult> PostLamp(Lamp lamp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lamp.Add(lamp);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lamp.Id }, lamp);
        }

        // DELETE: api/LampsAPI/5
        [ResponseType(typeof(Lamp))]
        public async Task<IHttpActionResult> DeleteLamp(int id)
        {
            Lamp lamp = await db.Lamp.FindAsync(id);
            if (lamp == null)
            {
                return NotFound();
            }

            db.Lamp.Remove(lamp);
            await db.SaveChangesAsync();

            return Ok(lamp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LampExists(int id)
        {
            return db.Lamp.Count(e => e.Id == id) > 0;
        }
    }
}