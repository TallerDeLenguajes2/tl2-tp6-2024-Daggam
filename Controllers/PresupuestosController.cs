using Microsoft.AspNetCore.Mvc;
using PresupuestoRepositoryNamespace;
using TiendaNamespace;

namespace tl2_tp6_2024_Daggam.Controllers;

public class PresupuestosController : Controller
{
    readonly IPresupuestoRepository _presupuestoRespository;
    public PresupuestosController(IPresupuestoRepository presupuestoRepository)
    {
        _presupuestoRespository = presupuestoRepository;
    }    
    [HttpGet]
    public ActionResult Index(){
        var presupuestos = _presupuestoRespository.ObtenerPresupuestos();
        return View(presupuestos);
    }
    [HttpGet]
    public ActionResult Crear(){
        return View(new Presupuesto());
    }
    [HttpPost]
    public ActionResult Crear(Presupuesto p){
        _presupuestoRespository.CrearPresupuesto(p.NombreDestinatario);
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
        _presupuestoRespository.AgregarDetallePresupuesto(p.IdPresupuesto,p.Detalle[0].Producto.IdProducto,p.Detalle[0].Cantidad);
        return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult Eliminar(int id){
        _presupuestoRespository.EliminarPresupuesto(id);
        return RedirectToAction("Index");
    }
}