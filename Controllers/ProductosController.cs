using Microsoft.AspNetCore.Mvc;
using ProductoRepositoryNamespace;
using TiendaNamespace;

namespace tl2_tp6_2024_Daggam.Controllers;

public class ProductosController : Controller
{
    ProductoRepository productoRepository;

    public ProductosController()
    {
        productoRepository = new SQLiteProductoRepository();
    }    
    [HttpGet]
    public ActionResult Index()
    {
        List<Producto>? productos = productoRepository.obtenerProductos();
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
        productoRepository.crearProducto(p.Descripcion,p.Precio);
        return RedirectToAction("Index");
    }
    [HttpGet()]
    public ActionResult Modificar(int id)
    {
        Producto p = productoRepository.obtenerProducto(id);
        return View(p);
    }

    [HttpPost()]
    public ActionResult Modificar(Producto p)
    {
        productoRepository.modificarProducto(p.IdProducto,p.Descripcion,p.Precio);
        return RedirectToAction("Index");
    }
    [HttpPost()]
    public ActionResult Eliminar(int id)
    {
        productoRepository.eliminarProducto(id);   
        return RedirectToAction("Index");
    }
}