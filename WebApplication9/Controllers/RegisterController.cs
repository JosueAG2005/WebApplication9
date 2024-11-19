using Microsoft.AspNetCore.Mvc;
using WebApplication9.Models;
using System.Linq;
using System;

namespace WebApplication9.Controllers
{
    public class RegisterController : Controller
    {
        private readonly SistemaDeConcursosContext _context;

        public RegisterController(SistemaDeConcursosContext context)
        {
            _context = context;
        }

        // Mostrar la vista de registro
        public IActionResult Index()
        {
            return View();
        }

        // Acción para registrar un nuevo usuario
        [HttpPost]
        public IActionResult Index(Usuario usuario, string confirmarContraseña)
        {
            if (ModelState.IsValid)
            {
                // Verifica si el correo electrónico ya está registrado
                if (_context.Usuarios.Any(u => u.Email == usuario.Email))
                {
                    ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
                    return View(usuario);
                }

                // Verifica si la contraseña y la confirmación coinciden
                if (usuario.Contraseña != confirmarContraseña)
                {
                    ModelState.AddModelError("ConfirmarContraseña", "Las contraseñas no coinciden.");
                    return View(usuario);
                }

                // Guardar el nuevo usuario en la base de datos
                usuario.FechaRegistro = DateTime.Now;
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                // Redirigir al login después de un registro exitoso
                return RedirectToAction("Index", "Login");
            }

            return View(usuario);
        }
    }
}
