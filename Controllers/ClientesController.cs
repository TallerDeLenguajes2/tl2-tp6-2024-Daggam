using Microsoft.AspNetCore.Mvc;
using ClienteRepositoryNamespace;
using TiendaNamespace;
namespace tl2_tp6_2024_Daggam.Controllers;

public class ClientesController : Controller
{
    ClienteRepository clienteRepository;

    public ClientesController()
    {
        clienteRepository = new SQLiteClienteRepository();
    }

    [HttpGet]
    public ActionResult Index()
    {
        List<Cliente>? clientes = clienteRepository.obtenerClientes();
        return View(clientes);
    }
    [HttpGet]
    public ActionResult Crear()
    {
        return View(new Cliente());
    }
    [HttpPost]
    public ActionResult Crear(Cliente p)
    {
        clienteRepository.crearCliente(p);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public ActionResult Modificar(int id)
    {
        Cliente c = clienteRepository.obtenerCliente(id);
        return View(c);
    }
    [HttpPost]
    public ActionResult Modificar(Cliente c)
    {
        clienteRepository.modificarCliente(c);
        return RedirectToAction("Index");
    }
    
}