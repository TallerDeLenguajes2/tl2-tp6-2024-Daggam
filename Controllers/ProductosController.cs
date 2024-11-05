using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_Daggam.Models;

namespace tl2_tp6_2024_Daggam.Controllers;

public class ProductosController : Controller
{
    public IActionResult Index(){
        return View();
    }
    public IActionResult Listar()
    {
        return View();
    }
}