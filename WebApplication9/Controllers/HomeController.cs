using Microsoft.AspNetCore.Mvc;
using WebApplication9.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace WebApplication9.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SistemaDeConcursosContext _context;

        public HomeController(ILogger<HomeController> logger, SistemaDeConcursosContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Obtener estadísticas generales para las tarjetas del dashboard
            int totalUsuarios = _context.Usuarios.Count();
            int totalConcursosActivos = _context.Concursos.Count(c => c.Estado == "activo");
            int totalInscripcionesHoy = _context.Inscripciones.Count(i => i.FechaInscripcion.HasValue && i.FechaInscripcion.Value.Date == DateTime.Today);
            int totalPuntuaciones = _context.PuntuacionesJurados.Count();

            // Pasar estas estadísticas a la vista usando ViewData
            ViewData["TotalUsuarios"] = totalUsuarios;
            ViewData["TotalConcursosActivos"] = totalConcursosActivos;
            ViewData["TotalInscripcionesHoy"] = totalInscripcionesHoy;
            ViewData["TotalPuntuaciones"] = totalPuntuaciones;

            // Pasar los datos de concursos recientes para la tabla de detalles
            var concursosRecientes = _context.Concursos
                .OrderByDescending(c => c.FechaInicio)
                .Take(5)
                .ToList();

            return View(concursosRecientes);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
