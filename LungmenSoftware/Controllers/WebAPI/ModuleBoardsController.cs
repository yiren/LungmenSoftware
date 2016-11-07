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
using LungmenSoftware.Models.V2;

namespace LungmenSoftware.Controllers.WebAPI
{
    public class ModuleBoardsController : ApiController
    {
        private NumacDbContext db = new NumacDbContext();

        // GET: api/ModuleBoards
        public IQueryable<ModuleBoard> GetModuleBoards()
        {
            return db.ModuleBoards;
        }

        // GET: api/ModuleBoards/5
        [ResponseType(typeof(ModuleBoard))]
        public IHttpActionResult GetModuleBoard(Guid id)
        {
            ModuleBoard moduleBoard = db.ModuleBoards.Find(id);
            if (moduleBoard == null)
            {
                return NotFound();
            }

            return Ok(moduleBoard);
        }

        // PUT: api/ModuleBoards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutModuleBoard(Guid id, ModuleBoard moduleBoard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != moduleBoard.ModuleBoardId)
            {
                return BadRequest();
            }

            db.Entry(moduleBoard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleBoardExists(id))
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

        // POST: api/ModuleBoards
        [ResponseType(typeof(ModuleBoard))]
        public IHttpActionResult PostModuleBoard(ModuleBoard moduleBoard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ModuleBoards.Add(moduleBoard);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ModuleBoardExists(moduleBoard.ModuleBoardId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = moduleBoard.ModuleBoardId }, moduleBoard);
        }

        // DELETE: api/ModuleBoards/5
        [ResponseType(typeof(ModuleBoard))]
        public IHttpActionResult DeleteModuleBoard(Guid id)
        {
            ModuleBoard moduleBoard = db.ModuleBoards.Find(id);
            if (moduleBoard == null)
            {
                return NotFound();
            }

            db.ModuleBoards.Remove(moduleBoard);
            db.SaveChanges();

            return Ok(moduleBoard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModuleBoardExists(Guid id)
        {
            return db.ModuleBoards.Count(e => e.ModuleBoardId == id) > 0;
        }
    }
}