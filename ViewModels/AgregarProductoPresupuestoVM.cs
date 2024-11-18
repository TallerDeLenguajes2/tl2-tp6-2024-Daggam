using ProductoRepositoryNamespace;
using TiendaNamespace;

namespace ViewModel;

class AgregarProductoPresupuestoViewModel{
    public int IdPresupuesto {get;set;}
    public List<Producto> Productos{get;set;}
    public AgregarProductoPresupuestoViewModel(int idPresupuesto){

        ProductoRepository pr = new SQLiteProductoRepository();
        this.Productos = pr.obtenerProductos();
        this.IdPresupuesto = idPresupuesto;
    }
}