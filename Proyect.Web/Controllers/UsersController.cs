using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Proyect.Data;
using Proyect.Web.Filters;

namespace Proyect.Web.Controllers
{
    [CustomAuthorize] // Protege todas las acciones: requiere sesión
    public class UsersController : BaseController
    {
        // GET: Users
        // Muestra SOLO el usuario logueado (puedes usar esto como "perfil" o lista de 1)
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            var users = UserBusiness.GetUsers(0)
                .Where(u => u.UserID == userId)
                .ToList();

            return View(users);
        }

        // GET: Users/Details
        // Muestra el perfil del usuario logueado
        public ActionResult Details()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            var user = UserBusiness.GetUsers(0)
                .FirstOrDefault(u => u.UserID == userId);

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // GET: Users/Create
        // Si quieres permitir registro desde aquí (opcional), mantenlo protegido o no según prefieras.
        // Normalmente el registro lo maneja AuthController, así que se puede deshabilitar.
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                UserBusiness.SaveOrUpdate(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit
        // Solo permite editar el propio perfil
        public ActionResult Edit()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            var user = UserBusiness.GetUsers(0)
                .FirstOrDefault(u => u.UserID == userId);

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // POST: Users/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,Email,Password")] User user)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            // Validación: el usuario sólo puede modificar su propio registro
            if (user.UserID != userId)
                return new HttpStatusCodeResult(401);

            if (ModelState.IsValid)
            {
                // Guarda en texto plano (como tienes en la BD)
                UserBusiness.SaveOrUpdate(user);
                return RedirectToAction("Details");
            }

            return View(user);
        }

        // GET: Users/Delete
        // Confirmación para eliminar su cuenta
        public ActionResult Delete()
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            var user = UserBusiness.GetUsers(0)
                .FirstOrDefault(u => u.UserID == userId);

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        // POST: Users/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int userId = Convert.ToInt32(Session["UserID"]);

            if (id != userId)
                return new HttpStatusCodeResult(401);

            UserBusiness.Delete(id);

            // Cerrar sesión luego de borrar la cuenta
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Auth");
        }

        public ActionResult Profile()
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            //var user = UserBusiness.GetById(userId);
            var user = UserBusiness.GetUsers(0)
                .FirstOrDefault(u => u.UserID == userId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(User model)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            var user = UserBusiness.GetUsers(0).FirstOrDefault(u => u.UserID==userId);
            if (user == null) return HttpNotFound();    

            user.Username = model.Username;
            UserBusiness.SaveOrUpdate(user);

            Session["Username"] = user.Username;

            ViewBag.Success = "Perfil actualizado correctamente";
            return View(model);
        }

    }
}