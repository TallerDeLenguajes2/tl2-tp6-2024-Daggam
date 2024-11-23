using Microsoft.AspNetCore.Mvc;
using ProductoRepositoryNamespace;
using TiendaNamespace;

namespace tl2_tp6_2024_Daggam.Controllers;

public class ProductosController : Controller
{
    readonly IProductoRepository _productoRepository;

    public ProductosController(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }    
    [HttpGet]
    public ActionResult Index()
    {
        if(HttpContext.Session.GetString("Rol")==null) return RedirectToAction("Index","Login");
        List<Producto>? productos = _productoRepository.obtenerProductos();
        return View(productos);
    }
    [HttpGet]
    public ActionResult Crear()
    {
       
        if(HttpContext.Session.GetString("Rol")==null) return RedirectToAction("Index","Login");
        return View(new Producto());
    }
    [HttpPost]
    public ActionResult Crear(Producto p)
    {
        if(HttpContext.Session.GetString("Rol")==null) return RedirectToAction("Index","Login");
        _productoRepository.crearProducto(p.Descripcion,p.Precio);
        return RedirectToAction("Index");
    }
    [HttpGet()]
    public ActionResult Modificar(int id)
    {
        if(HttpContext.Session.GetString("Rol")==null) return RedirectToAction("Index","Login");
        Producto p = _productoRepository.obtenerProducto(id);
        return View(p);
    }

    [HttpPost()]
    public ActionResult Modificar(Producto p)
    {
        if(HttpContext.Session.GetString("Rol")==null) return RedirectToAction("Index","Login");
        _productoRepository.modificarProducto(p.IdProducto,p.Descripcion,p.Precio);
        return RedirectToAction("Index");
    }
    [HttpPost()]
    public ActionResult Eliminar(int id)
    {
        if(HttpContext.Session.GetString("Rol")==null) return RedirectToAction("Index","Login");
        _productoRepository.eliminarProducto(id);   
        return RedirectToAction("Index");
    }
}