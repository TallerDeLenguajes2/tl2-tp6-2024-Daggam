using ClienteRepositoryNamespace;
using TiendaNamespace;

namespace ViewModel;

public class CrearPresupuestoViewModel{
    public List<Cliente> Clientes {get;set;}

    public CrearPresupuestoViewModel(){
        ClienteRepository rp = new SQLiteClienteRepository();
        Clientes =  rp.obtenerClientes();
    }
}