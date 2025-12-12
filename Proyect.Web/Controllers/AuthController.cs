using Proyect.Core;
using Proyect.Data;
using System;
using System.Web;
using System.Web.Mvc;

namespace Proyect.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserBusiness _userBusiness;

        public AuthController()
        {
            _userBusiness = new UserBusiness();
        }

        // GET: /Auth/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            // si ya hay sesión, redirige
            if (Session["UserID"] != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Email y contraseña son requeridos.";
                return View();
            }

            var user = _userBusiness.Login(email.Trim(), password);
            if (user != null)
            {
                // Guarda datos en sesión
                Session["UserID"] = user.UserID;
                Session["Username"] = user.Username;
                Session["UserEmail"] = user.Email;

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Credenciales incorrectas.";
            return View();
        }

        // GET: /Auth/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (Session["UserID"] != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register([Bind(Include = "Username,Email,Password")] User user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Por favor complete todos los campos.";
                return View(user);
            }

            // Verificar si el email ya existe
            if (_userBusiness.ExistsEmail(user.Email))
            {
                ViewBag.Error = "El correo ya está registrado.";
                return View(user);
            }

            // Crear nuevo usuario (UserID será asignado por EF)
            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password // texto plano, como pediste
            };

            _userBusiness.SaveOrUpdate(newUser);

            TempData["Message"] = "Registro exitoso. Ya puedes iniciar sesión.";
            return RedirectToAction("Login");
        }

        // GET: /Auth/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            ModelState.Clear();
            return RedirectToAction("Login");
        }
    }
}
