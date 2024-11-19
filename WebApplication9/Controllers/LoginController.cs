using Microsoft.AspNetCore.Mvc;
using WebApplication9.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace WebApplication9.Controllers
{
    public class LoginController : Controller
    {
        private readonly SistemaDeConcursosContext _context;

        public LoginController(SistemaDeConcursosContext context)
        {
            _context = context;
        }

        // Mostrar la vista de login
        public IActionResult Index()
        {
            return View();
        }

        // Acción para manejar el inicio de sesión
        [HttpPost]
        public IActionResult Index(string email, string contraseña)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == email && u.Contraseña == contraseña);

            if (usuario != null)
            {
                // Guardar el ID del usuario y el rol en la sesión
                HttpContext.Session.SetInt32("UsuarioId", usuario.IdUsuario);
                HttpContext.Session.SetString("UserRole", usuario.TipoUsuario); // Guardar el tipo de usuario

                // Redirigir al dashboard después del inicio de sesión exitoso
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Correo electrónico o contraseña incorrectos.");
                return View();
            }
        }

        // Acción para cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UsuarioId");
            HttpContext.Session.Remove("UserRole"); // Eliminar el rol de usuario de la sesión
            return RedirectToAction("Index", "Login");
        }
    }
}
