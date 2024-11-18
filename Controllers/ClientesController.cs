using Microsoft.AspNetCore.Mvc;
using ClienteRepositoryNamespace;
using TiendaNamespace;
using ViewModel;
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
        return View(new ClienteViewModel());
    }
    [HttpPost]
    public ActionResult Crear(ClienteViewModel cvw)
    {
        if(ModelState.IsValid){
            Cliente c = new Cliente(cvw.IdCliente,cvw.Nombre,cvw.Email,cvw.Telefono);
            clienteRepository.crearCliente(c);
        }
        return RedirectToAction("Index");
    }
    [HttpGet]
    public ActionResult Modificar(int id)
    {
        Cliente c = clienteRepository.obtenerCliente(id);
        ClienteViewModel cvw = new ClienteViewModel(){
            IdCliente=c.IdCliente,
            Nombre = c.Nombre,
            Email = c.Email,
            Telefono = c.Telefono
        };
        return View(cvw);
    }
    [HttpPost]
    public ActionResult Modificar(ClienteViewModel cvw)
    {
        if(ModelState.IsValid){
            Cliente c = new Cliente(cvw.IdCliente,cvw.Nombre,cvw.Email,cvw.Telefono);
            clienteRepository.modificarCliente(c);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Eliminar(int id)
    {
        clienteRepository.eliminarCliente(id);
        return RedirectToAction("Index"); 
    }
}