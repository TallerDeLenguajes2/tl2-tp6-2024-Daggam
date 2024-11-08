using Microsoft.AspNetCore.Mvc;
using ProductoRepositoryNamespace;
using TiendaNamespace;
using tl2_tp6_2024_Daggam.Models;

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
        return View();
    }
    // [HttpPost]
    // public ActionResult Crear()
    // {
    //     return View();
    // }
    [HttpGet()]
    public ActionResult Modificar()
    {
        int id=1;
        Producto p = productoRepository.obtenerProducto(id);
        return View(p);
    }
    public ActionResult Eliminar()
    {
        return View();
    }
}