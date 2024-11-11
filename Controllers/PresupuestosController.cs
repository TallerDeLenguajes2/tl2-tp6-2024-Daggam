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
        var presupuestos = presupuestoRespository.ObtenerPresupuestos();
        return View(presupuestos);
    }
    [HttpGet]
    public ActionResult Crear(){
        return View(new Presupuesto());
    }
    [HttpPost]
    public ActionResult Crear(Presupuesto p){
        presupuestoRespository.CrearPresupuesto(p.NombreDestinatario);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public ActionResult Agregar(int id){
        Presupuesto p = new Presupuesto();
        p.IdPresupuesto = id;
        p.Detalle.Add(new PresupuestoDetalle());
        return View(p);
    }
    [HttpPost]
    public ActionResult Agregar(Presupuesto p){
        presupuestoRespository.AgregarDetallePresupuesto(p.IdPresupuesto,p.Detalle[0].Producto.IdProducto,p.Detalle[0].Cantidad);
        return RedirectToAction("Index");
    }
}