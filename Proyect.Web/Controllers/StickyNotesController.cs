using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Proyect.Data;
using Proyect.Web.Filters;

namespace Proyect.Web.Controllers
{
    [CustomAuthorize]
    public class StickyNotesController : BaseController
    {
        // GET: StickyNotes
        public ActionResult Index(string status, string search)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            var notes = StickyNoteXUserBusiness
                .GetNotesXUsers(0)
                .Where(x => x.UserID == userId)
                .Select(x => x.StickyNote)
                .AsQueryable();

            // 🔹 Filtro por estado
            if (!string.IsNullOrEmpty(status))
            {
                notes = notes.Where(n => n.Status == status);
            }

            // 🔍 Búsqueda por título o descripción
            if (!string.IsNullOrEmpty(search))
            {
                notes = notes.Where(n =>
                    n.Title.Contains(search) ||
                    n.Description.Contains(search)
                );
            }

            // Para mantener valores en la vista
            ViewBag.CurrentStatus = status;
            ViewBag.Search = search;

            return View(notes.ToList());
        }

        // GET: StickyNotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int userId = Convert.ToInt32(Session["UserID"]);

            var note = StickyNoteXUserBusiness
                .GetNotesXUsers(0)
                .Where(x => x.UserID == userId && x.StickynoteID == id)
                .Select(x => x.StickyNote)
                .FirstOrDefault();

            if (note == null)
                return HttpNotFound();

            return View(note);
        }

        // GET: StickyNotes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StickyNotes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StickynoteID,Title,Description,Status,DueDate")] StickyNote stickyNote)
        {
            if (ModelState.IsValid)
            {
                // Crear nota
                StickyNoteBusiness.SaveOrUpdate(stickyNote);

                // Asociar nota al usuario logueado
                int userId = Convert.ToInt32(Session["UserID"]);
                StickyNoteXUserBusiness.SaveOrUpdate(new StickyNoteXUser
                {
                    UserID = userId,
                    StickynoteID = stickyNote.StickynoteID
                });

                return RedirectToAction("Index");
            }

            return View(stickyNote);
        }

        // GET: StickyNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int userId = Convert.ToInt32(Session["UserID"]);

            var note = StickyNoteXUserBusiness
                .GetNotesXUsers(0)
                .Where(x => x.UserID == userId && x.StickynoteID == id)
                .Select(x => x.StickyNote)
                .FirstOrDefault();

            if (note == null)
                return HttpNotFound();

            return View(note);
        }

        // POST: StickyNotes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StickynoteID,Title,Description,Status,DueDate")] StickyNote stickyNote)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            // Validar que la nota pertenece al usuario
            var relation = StickyNoteXUserBusiness
                .GetNotesXUsers(0)
                .FirstOrDefault(x => x.UserID == userId && x.StickynoteID == stickyNote.StickynoteID);

            if (relation == null)
                return new HttpStatusCodeResult(401);

            if (ModelState.IsValid)
            {
                StickyNoteBusiness.SaveOrUpdate(stickyNote);
                return RedirectToAction("Index");
            }

            return View(stickyNote);
        }

        // GET: StickyNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int userId = Convert.ToInt32(Session["UserID"]);

            var note = StickyNoteXUserBusiness
                .GetNotesXUsers(0)
                .Where(x => x.UserID == userId && x.StickynoteID == id)
                .Select(x => x.StickyNote)
                .FirstOrDefault();

            if (note == null)
                return HttpNotFound();

            return View(note);
        }

        // POST: StickyNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            var relation = StickyNoteXUserBusiness
                .GetNotesXUsers(0)
                .FirstOrDefault(x => x.UserID == userId && x.StickynoteID == id);

            if (relation == null)
                return new HttpStatusCodeResult(401);

            StickyNoteXUserBusiness.Delete(relation.StickyUserID);

            StickyNoteBusiness.Delete(id);

            return RedirectToAction("Index");
        }

    }
}