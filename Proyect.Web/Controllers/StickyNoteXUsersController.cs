using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Proyect.Data;
using Proyect.Web.Filters;

namespace Proyect.Web.Controllers
{
    [CustomAuthorize]
    public class StickyNoteXUsersController : BaseController
    {
        // ==========================================================
        // GET: StickyNoteXUsers
        // Mostrar SOLO las relaciones del usuario logueado
        // ==========================================================
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            var relations = StickyNoteXUserBusiness
                .GetNotesXUsers(0)
                .Where(x => x.UserID == userId)
                .ToList();

            return View(relations);
        }

        // ==========================================================
        // GET: StickyNoteXUsers/Details/5
        // ==========================================================
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int userId = Convert.ToInt32(Session["UserID"]);

            var relation = StickyNoteXUserBusiness
                .GetNotesXUsers((int)id)
                .FirstOrDefault();

            if (relation == null || relation.UserID != userId)
                return HttpNotFound();

            return View(relation);
        }

        // ==========================================================
        // GET: StickyNoteXUsers/Create
        // No permitimos seleccionar usuario, siempre será el logueado
        // ==========================================================
        public ActionResult Create()
        {
            // Solo mostrar notas disponibles
            ViewBag.StickynoteID = new SelectList(
                StickyNoteBusiness.GetNotes(0), "StickynoteID", "Title"
            );

            return View();
        }

        // ==========================================================
        // POST: StickyNoteXUsers/Create
        // ==========================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StickynoteID")] StickyNoteXUser stickyNoteXUser)
        {
            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(Session["UserID"]);

                stickyNoteXUser.UserID = userId;
                StickyNoteXUserBusiness.SaveOrUpdate(stickyNoteXUser);

                return RedirectToAction("Index");
            }

            ViewBag.StickynoteID = new SelectList(
                StickyNoteBusiness.GetNotes(0), "StickynoteID", "Title"
            );

            return View(stickyNoteXUser);
        }

        // ==========================================================
        // GET: StickyNoteXUsers/Edit/5
        // ==========================================================
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int userId = Convert.ToInt32(Session["UserID"]);

            var relation = StickyNoteXUserBusiness
                .GetNotesXUsers((int)id)
                .FirstOrDefault();

            if (relation == null || relation.UserID != userId)
                return HttpNotFound();

            ViewBag.StickynoteID = new SelectList(
                StickyNoteBusiness.GetNotes(0),
                "StickynoteID",
                "Title",
                relation.StickynoteID
            );

            return View(relation);
        }

        // ==========================================================
        // POST: StickyNoteXUsers/Edit/5
        // ==========================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StickyUserID,StickynoteID")] StickyNoteXUser stickyNoteXUser)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            var original = StickyNoteXUserBusiness
                .GetNotesXUsers(stickyNoteXUser.StickyUserID)
                .FirstOrDefault();

            // Validar que existe y pertenece al usuario
            if (original == null || original.UserID != userId)
                return new HttpStatusCodeResult(401);

            if (ModelState.IsValid)
            {
                stickyNoteXUser.UserID = userId; // mantener propietario
                StickyNoteXUserBusiness.SaveOrUpdate(stickyNoteXUser);
                return RedirectToAction("Index");
            }

            ViewBag.StickynoteID = new SelectList(
                StickyNoteBusiness.GetNotes(0),
                "StickynoteID",
                "Title",
                stickyNoteXUser.StickynoteID
            );

            return View(stickyNoteXUser);
        }

        // ==========================================================
        // GET: StickyNoteXUsers/Delete/5
        // ==========================================================
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            int userId = Convert.ToInt32(Session["UserID"]);

            var relation = StickyNoteXUserBusiness
                .GetNotesXUsers((int)id)
                .FirstOrDefault();

            if (relation == null || relation.UserID != userId)
                return HttpNotFound();

            return View(relation);
        }

        // ==========================================================
        // POST: StickyNoteXUsers/Delete/5
        // ==========================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            var relation = StickyNoteXUserBusiness
                .GetNotesXUsers(id)
                .FirstOrDefault();

            if (relation == null || relation.UserID != userId)
                return new HttpStatusCodeResult(401);

            StickyNoteXUserBusiness.Delete(id);

            return RedirectToAction("Index");
        }
    }
}