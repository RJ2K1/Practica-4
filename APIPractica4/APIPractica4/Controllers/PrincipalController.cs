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
    public class PrincipalController : ApiController
    {
        private DBPractica4Entities5 db = new DBPractica4Entities5();

        // GET: api/Principal
        public IQueryable<Principal> GetPrincipal()
        {
            return db.Principal;
        }

        // GET: api/Principal/5
        [ResponseType(typeof(Principal))]
        public IHttpActionResult GetPrincipal(int id)
        {
            Principal principal = db.Principal.Find(id);
            if (principal == null)
            {
                return NotFound();
            }

            return Ok(principal);
        }

        // PUT: api/Principal/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrincipal(int id, Principal principal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != principal.codCompra)
            {
                return BadRequest();
            }

            db.Entry(principal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrincipalExists(id))
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

        // POST: api/Principal
        [ResponseType(typeof(Principal))]
        public IHttpActionResult PostPrincipal(Principal principal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Principal.Add(principal);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PrincipalExists(principal.codCompra))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = principal.codCompra }, principal);
        }

        // DELETE: api/Principal/5
        [ResponseType(typeof(Principal))]
        public IHttpActionResult DeletePrincipal(int id)
        {
            Principal principal = db.Principal.Find(id);
            if (principal == null)
            {
                return NotFound();
            }

            db.Principal.Remove(principal);
            db.SaveChanges();

            return Ok(principal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrincipalExists(int id)
        {
            return db.Principal.Count(e => e.codCompra == id) > 0;
        }
    }
}