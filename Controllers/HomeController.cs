using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07.Models;

namespace TP07.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Login", "Account");
    }

    public IActionResult CargarTareas(int idUsuario)
    {
        List<Tarea> tareas = BD.TraerTareas(idUsuario);
        return View("VerTareas", tareas);
    }

    public IActionResult CrearTarea(string Titulo, string Descripcion, DateTime fecha, bool Finalizada)
    {
        return View("CrearTarea");
    }

    public IActionResult FinalizarTarea(bool Finalizada)
    {
        // nc si falta algo
        return View("VerTareas");
    }

    public IActionResult EliminarTarea(bool Finalizada)
    { 
        // faltan cosas
        return View("VerTareas");
    }

    public IActionResult EditarTarea(string Titulo, string Descripcion, DateTime fecha, bool Finalizada)
    {
        return View("ModificarTarea");
    }

}
