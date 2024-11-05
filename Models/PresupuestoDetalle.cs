namespace TiendaNamespace;

public class PresupuestoDetalle
{
    Producto producto;
    int cantidad;

    public Producto Producto { get => producto; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
    public PresupuestoDetalle(Producto producto,int cantidad){
        this.producto = producto;
        this.cantidad = cantidad;
    }
}