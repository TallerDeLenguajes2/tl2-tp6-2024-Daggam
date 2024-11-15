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
}