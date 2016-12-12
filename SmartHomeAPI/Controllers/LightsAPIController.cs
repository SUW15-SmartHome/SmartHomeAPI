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
    public class LightsAPIController : ApiController
    {
        private SmartHomeDatabaseLightsEntities db = new SmartHomeDatabaseLightsEntities();

        // GET: api/LightsAPI
        public IQueryable<Lights> GetLights()
        {
            return db.Lights;
        }

        // GET: api/LightsAPI/5
        [ResponseType(typeof(Lights))]
        public async Task<IHttpActionResult> GetLights(int id)
        {
            Lights lights = await db.Lights.FindAsync(id);
            if (lights == null)
            {
                return NotFound();
            }

            return Ok(lights);
        }

        // PUT: api/LightsAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLights(int id, Lights lights)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lights.Id)
            {
                return BadRequest();
            }

            db.Entry(lights).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LightsExists(id))
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

        // POST: api/LightsAPI
        [ResponseType(typeof(Lights))]
        public async Task<IHttpActionResult> PostLights(Lights lights)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lights.Add(lights);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lights.Id }, lights);
        }

        // DELETE: api/LightsAPI/5
        [ResponseType(typeof(Lights))]
        public async Task<IHttpActionResult> DeleteLights(int id)
        {
            Lights lights = await db.Lights.FindAsync(id);
            if (lights == null)
            {
                return NotFound();
            }

            db.Lights.Remove(lights);
            await db.SaveChangesAsync();

            return Ok(lights);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LightsExists(int id)
        {
            return db.Lights.Count(e => e.Id == id) > 0;
        }
    }
}