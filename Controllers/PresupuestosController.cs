using Microsoft.AspNetCore.Mvc;
using PresupuestoRepositoryNamespace;
using TiendaNamespace;
using ViewModel;

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
        var presupuestos = presupuestoRespository.ObtenerPresupuestos();
        return View(presupuestos);
    }
    [HttpGet]
    public ActionResult Crear(){
        CrearPresupuestoViewModel vm = new CrearPresupuestoViewModel();
        return View(vm);
    }
    [HttpPost]
    public ActionResult Crear(int id){
        presupuestoRespository.CrearPresupuesto(id);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public ActionResult Agregar(int id){
        AgregarProductoPresupuestoViewModel p = new AgregarProductoPresupuestoViewModel(id);
        return View(p);
    }
    [HttpPost]
    public ActionResult Agregar(DetallePresupuestoViewModel dp){
        presupuestoRespository.AgregarDetallePresupuesto(dp.IdPresupuesto,dp.IdProducto,dp.Cantidad);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public ActionResult AgregarCantidad(int idProducto,int idPresupuesto){
        DetallePresupuestoViewModel dp = new DetallePresupuestoViewModel(idPresupuesto,idProducto,0);
        return View(dp);
    }

    [HttpPost]
    public ActionResult Eliminar(int id){
        presupuestoRespository.EliminarPresupuesto(id);
        return RedirectToAction("Index");
    }
}