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
        List<Producto>? productos = _productoRepository.obtenerProductos();
        return View(productos);
    }
    [HttpGet]
    public ActionResult Crear()
    {
        return View(new Producto());
    }
    [HttpPost]
    public ActionResult Crear(Producto p)
    {
        _productoRepository.crearProducto(p.Descripcion,p.Precio);
        return RedirectToAction("Index");
    }
    [HttpGet()]
    public ActionResult Modificar(int id)
    {
        Producto p = _productoRepository.obtenerProducto(id);
        return View(p);
    }

    [HttpPost()]
    public ActionResult Modificar(Producto p)
    {
        _productoRepository.modificarProducto(p.IdProducto,p.Descripcion,p.Precio);
        return RedirectToAction("Index");
    }
    [HttpPost()]
    public ActionResult Eliminar(int id)
    {
        _productoRepository.eliminarProducto(id);   
        return RedirectToAction("Index");
    }
}