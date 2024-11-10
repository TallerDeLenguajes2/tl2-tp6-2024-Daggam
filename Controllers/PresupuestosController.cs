using Microsoft.AspNetCore.Mvc;
using PresupuestoRepositoryNamespace;
using TiendaNamespace;

namespace tl2_tp6_2024_Daggam.Controllers;

public class PresupuestosController : Controller
{
    PresupuestoRepository presupuestoRespository;
    public PresupuestosController()
    {
        presupuestoRespository = new SQLitePresupuestoRepository();
    }    
    [HttpGet]
    public ActionResult Index(){
        return View();
    }
}