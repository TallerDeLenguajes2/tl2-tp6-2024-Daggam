using Microsoft.AspNetCore.Mvc;
using ProductoRepositoryNamespace;
using TiendaNamespace;
using ViewModel;

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
        return View(new ProductosViewModel());
    }
    [HttpPost]
    public ActionResult Crear(ProductosViewModel p)
    {
        if(ModelState.IsValid) productoRepository.crearProducto(p.Descripcion,p.Precio);
        return RedirectToAction("Index");
    }
    [HttpGet()]
    public ActionResult Modificar(int id)
    {
        Producto p = productoRepository.obtenerProducto(id);
        ProductosViewModel pvm = new ProductosViewModel(){
            IdProducto = p.IdProducto,
            Descripcion = p.Descripcion,
            Precio = p.Precio
        };
        return View(pvm);
    }

    [HttpPost()]
    public ActionResult Modificar(ProductosViewModel p)
    {
        if(ModelState.IsValid) productoRepository.modificarProducto(p.IdProducto,p.Descripcion,p.Precio);
        return RedirectToAction("Index");
    }
    [HttpPost()]
    public ActionResult Eliminar(int id)
    {
        productoRepository.eliminarProducto(id);   
        return RedirectToAction("Index");
    }
}