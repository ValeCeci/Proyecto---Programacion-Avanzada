using Proyect.Core;
using Proyect.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect.Web.Controllers
{
    public class HomeController : Controller
    {

        StickyNoteXUserBusiness stickyUserBusiness = new StickyNoteXUserBusiness();

        public ActionResult Index()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Auth");

            int userId = (int)Session["UserID"];

            var notesXUser = stickyUserBusiness
                .GetNotesXUsers(0)
                .Where(x => x.UserID == userId)
                .ToList();

            var notes = notesXUser
                .Select(x => x.StickyNote)
                .ToList();

            ViewBag.Pendientes = notes.Count(n => n.Status == "Pendiente");
            ViewBag.EnProceso = notes.Count(n => n.Status == "En Proceso");
            ViewBag.Completadas = notes.Count(n => n.Status == "Completado");

            return View();
        }
    }

}