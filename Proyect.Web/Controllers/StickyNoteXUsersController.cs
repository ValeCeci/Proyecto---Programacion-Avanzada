using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyect.Core;
using Proyect.Data;

namespace Proyect.Web.Controllers
{
    public class StickyNoteXUsersController : BaseController
    {
        // ==============================================
        // Muestra todas las relaciones Usuario-Nota
        // ==============================================
        public ActionResult Index()
        {
            // Obtiene todas las relaciones desde la capa de negocio
            var stickyNoteXUser = StickyNoteXUserBusiness.GetNotesXUsers(0);
            return View(stickyNoteXUser.ToList());
        }

        // ==============================================
        // Muestra el detalle de una relación específica
        // ==============================================
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Busca la relación específica por ID
            var stickyNoteXUser = StickyNoteXUserBusiness.GetNotesXUsers((int)id).FirstOrDefault();
            if (stickyNoteXUser == null)
            {
                return HttpNotFound();
            }

            return View(stickyNoteXUser);
        }

        // ==============================================
        // Vista para crear una nueva relación
        // ==============================================
        public ActionResult Create()
        {
            // Llama al método que llena los desplegables (usuarios y notas)
            SetSelectLists();
            return View();
        }

        // ==============================================
        // Acción POST para guardar una nueva relación
        // ==============================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StickyUserID,UserID,StickynoteID")] StickyNoteXUser stickyNoteXUser)
        {
            // Si el modelo es válido, se guarda mediante la capa de negocio
            if (ModelState.IsValid)
            {
                StickyNoteXUserBusiness.SaveOrUpdate(stickyNoteXUser);
                return RedirectToAction("Index");
            }

            // Si hay error de validación, se vuelven a llenar los combos
            SetSelectLists();
            return View(stickyNoteXUser);
        }

        // ==============================================
        // Vista para editar una relación existente
        // ==============================================
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var stickyNoteXUser = StickyNoteXUserBusiness.GetNotesXUsers((int)id).FirstOrDefault();
            if (stickyNoteXUser == null)
            {
                return HttpNotFound();
            }

            SetSelectLists();
            return View(stickyNoteXUser);
        }

        // ==============================================
        // Acción POST para actualizar una relación existente
        // ==============================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StickyUserID,UserID,StickynoteID")] StickyNoteXUser stickyNoteXUser)
        {
            if (ModelState.IsValid)
            {
                StickyNoteXUserBusiness.SaveOrUpdate(stickyNoteXUser);
                return RedirectToAction("Index");
            }

            SetSelectLists();
            return View(stickyNoteXUser);
        }

        // ==============================================
        // Vista para confirmar eliminación
        // ==============================================
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var stickyNoteXUser = StickyNoteXUserBusiness.GetNotesXUsers((int)id).FirstOrDefault();
            if (stickyNoteXUser == null)
            {
                return HttpNotFound();
            }

            return View(stickyNoteXUser);
        }

        // ==============================================
        // Acción POST que elimina la relación
        // ==============================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StickyNoteXUserBusiness.Delete(id);
            return RedirectToAction("Index");
        }

        // ==============================================
        // Método privado para llenar los combos de Usuario y Nota
        // ==============================================
        // ==========================================================
        // Carga los combos de Usuario y Nota en los formularios Create/Edit
        // ==========================================================
        private new void SetSelectLists()
        {
            using (var db = new ProyectoEntities())
            {
                // Lista de usuarios (usa "User" porque la tabla es singular)
                ViewBag.UserID = new SelectList(db.User.ToList(), "UserID", "Username");

                // Lista de notas (usa "StickyNote" porque la tabla es singular)
                ViewBag.StickynoteID = new SelectList(db.StickyNote.ToList(), "StickynoteID", "Title");
            }
        }

    }
}
