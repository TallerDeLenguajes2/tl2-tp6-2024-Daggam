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
    public IActionResult Index(){
        return View();
    }
    public IActionResult Listar()
    {
        List<Producto>? productos = productoRepository.obtenerProductos();
        return View(productos);
    }
    public IActionResult Crear()
    {
        return View();
    }
    public IActionResult Modificar()
    {
        return View();
    }
    public IActionResult Eliminar()
    {
        return View();
    }
}