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
      
        // GET: StickyNoteXUsers
        public ActionResult Index()
        {
            var stickyNoteXUser = StickyNoteXUserBusiness.GetNotesXUsers(0);
            return View(stickyNoteXUser.ToList());
        }

        // GET: StickyNoteXUsers/Details/5
        public ActionResult Details(int? id)
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

        // GET: StickyNoteXUsers/Create
        public ActionResult Create()
        {
            SetSelectLists();
            return View();
        }

        // POST: StickyNoteXUsers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StickyUserID,UserID,StickynoteID")] StickyNoteXUser stickyNoteXUser)
        {
            if (ModelState.IsValid)
            {
                StickyNoteXUserBusiness.SaveOrUpdate(stickyNoteXUser);
                return RedirectToAction("Index");
            }
            SetSelectLists();
            return View(stickyNoteXUser);
        }

        // GET: StickyNoteXUsers/Edit/5
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

        // POST: StickyNoteXUsers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: StickyNoteXUsers/Delete/5
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

        // POST: StickyNoteXUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StickyNoteXUserBusiness.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
