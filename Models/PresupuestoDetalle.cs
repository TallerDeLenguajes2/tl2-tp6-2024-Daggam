namespace TiendaNamespace;

public class PresupuestoDetalle
{
    Producto producto;
    int cantidad;

    public Producto Producto { get => producto; set => producto = value;}
    public int Cantidad { get => cantidad; set => cantidad = value; }
    public PresupuestoDetalle(){
        producto = new Producto();
        cantidad = 0;
    }
    public PresupuestoDetalle(Producto producto,int cantidad){
        this.producto = producto;
        this.cantidad = cantidad;
    }
}