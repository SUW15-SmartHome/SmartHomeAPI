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
    public class AlarmsAPIController : ApiController
    {
        private AlarmEntities db = new AlarmEntities();

        // GET: api/AlarmsAPI
        public IQueryable<Alarms> GetAlarms()
        {
            return db.Alarms;
        }

        // GET: api/AlarmsAPI/5
        [ResponseType(typeof(Alarms))]
        public async Task<IHttpActionResult> GetAlarms(int id)
        {
            Alarms alarms = await db.Alarms.FindAsync(id);
            if (alarms == null)
            {
                return NotFound();
            }

            return Ok(alarms);
        }

        // PUT: api/AlarmsAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAlarms(int id, Alarms alarms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alarms.Id)
            {
                return BadRequest();
            }

            db.Entry(alarms).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlarmsExists(id))
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

        // POST: api/AlarmsAPI
        [ResponseType(typeof(Alarms))]
        public async Task<IHttpActionResult> PostAlarms(Alarms alarms)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alarms.Add(alarms);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = alarms.Id }, alarms);
        }

        // DELETE: api/AlarmsAPI/5
        [ResponseType(typeof(Alarms))]
        public async Task<IHttpActionResult> DeleteAlarms(int id)
        {
            Alarms alarms = await db.Alarms.FindAsync(id);
            if (alarms == null)
            {
                return NotFound();
            }

            db.Alarms.Remove(alarms);
            await db.SaveChangesAsync();

            return Ok(alarms);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlarmsExists(int id)
        {
            return db.Alarms.Count(e => e.Id == id) > 0;
        }
    }
}