using Proyect.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyect.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly StickyNoteBusiness StickyNoteBusiness;
        protected readonly UserBusiness UserBusiness;
        protected readonly StickyNoteXUserBusiness StickyNoteXUserBusiness;

        public BaseController()
        {
            StickyNoteBusiness = new StickyNoteBusiness();
            UserBusiness = new UserBusiness();
            StickyNoteXUserBusiness = new StickyNoteXUserBusiness();
        }

        public void SetSelectLists()
        {
            ViewBag.StickyNotes = new SelectList(StickyNoteBusiness.GetNotes(0), "StickynoteID", "Title");
            ViewBag.Users = new SelectList(UserBusiness.GetUsers(0), "UserID", "Username");
            ViewBag.StickyNoteXUsers = new SelectList(StickyNoteXUserBusiness.GetNotesXUsers(0), "StickyUserID", "StickyUserID");
        }
    }
}