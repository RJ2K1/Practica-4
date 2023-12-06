using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using APIPractica4;

namespace APIPractica4.Controllers
{
    public class AbonosController : ApiController
    {
        private DBPractica4Entities5 db = new DBPractica4Entities5();

        // GET: api/Abonos
        public IQueryable<Abonos> GetAbonos()
        {
            return db.Abonos;
        }

        // GET: api/Abonos/5
        [ResponseType(typeof(Abonos))]
        public IHttpActionResult GetAbonos(int id)
        {
            Abonos abonos = db.Abonos.Find(id);
            if (abonos == null)
            {
                return NotFound();
            }

            return Ok(abonos);
        }

        // PUT: api/Abonos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAbonos(int id, Abonos abonos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != abonos.id)
            {
                return BadRequest();
            }

            db.Entry(abonos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonosExists(id))
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

        // POST: api/Abonos
        [ResponseType(typeof(Abonos))]
        public IHttpActionResult PostAbonos(Abonos abonos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Abonos.Add(abonos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = abonos.id }, abonos);
        }

        // DELETE: api/Abonos/5
        [ResponseType(typeof(Abonos))]
        public IHttpActionResult DeleteAbonos(int id)
        {
            Abonos abonos = db.Abonos.Find(id);
            if (abonos == null)
            {
                return NotFound();
            }

            db.Abonos.Remove(abonos);
            db.SaveChanges();

            return Ok(abonos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AbonosExists(int id)
        {
            return db.Abonos.Count(e => e.id == id) > 0;
        }
    }
}