namespace ViewModel;

public class DetallePresupuestoViewModel{
    public int IdPresupuesto {get;set;}
    public int IdProducto {get;set;}
    public int Cantidad {get;set;}

    public DetallePresupuestoViewModel(){}
    public DetallePresupuestoViewModel(int IdPresupuesto,int idProducto, int cantidad){
        this.IdProducto = idProducto;
        this.Cantidad = cantidad;
    }

}