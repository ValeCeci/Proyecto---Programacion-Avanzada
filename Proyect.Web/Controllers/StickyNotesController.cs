using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyect.Data;

namespace Proyect.Web.Controllers
{
    public class StickyNotesController : BaseController
    {
        // GET: StickyNotes
        public ActionResult Index()
        {
            var notes = StickyNoteBusiness.GetNotes(0);
            return View(notes.ToList());
        }

        // GET: StickyNotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stickyNote = StickyNoteBusiness.GetNotes((int)id).FirstOrDefault();
            if (stickyNote == null)
            {
                return HttpNotFound();
            }
            return View(stickyNote);
        }

        // GET: StickyNotes/Create
        public ActionResult Create()
        {
            SetSelectLists();
            return View();
        }

        // POST: StickyNotes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StickynoteID,Title,Description,Status,DueDate")] StickyNote stickyNote)
        {
            if (ModelState.IsValid)
            {
                StickyNoteBusiness.SaveOrUpdate(stickyNote);
                return RedirectToAction("Index");
            }
            SetSelectLists();
            return View(stickyNote);
        }

        // GET: StickyNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stickyNote = StickyNoteBusiness.GetNotes((int)id).FirstOrDefault();
            if (stickyNote == null)
            {
                return HttpNotFound();
            }
            SetSelectLists();
            return View(stickyNote);
        }

        // POST: StickyNotes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StickynoteID,Title,Description,Status,DueDate")] StickyNote stickyNote)
        {
            if (ModelState.IsValid)
            {
                StickyNoteBusiness.SaveOrUpdate(stickyNote);
                return RedirectToAction("Index");
            }
            SetSelectLists();
            return View(stickyNote);
        }

        // GET: StickyNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var stickyNote = StickyNoteBusiness.GetNotes((int)id).FirstOrDefault();
            if (stickyNote == null)
            {
                return HttpNotFound();
            }
            return View(stickyNote);
        }

        // POST: StickyNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StickyNoteBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
