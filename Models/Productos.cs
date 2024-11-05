namespace TiendaNamespace;

public class Producto
{
    int idProducto;
    string descripcion;
    int precio;

    public int IdProducto { get => idProducto; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public int Precio { get => precio; set => precio = value; }
    public Producto(string descripcion, int precio){
        this.descripcion = descripcion;
        this.precio = precio;
    }
    public Producto(int idProducto,string descripcion,int precio){
        this.idProducto = idProducto;
        this.descripcion = descripcion;
        this.precio = precio;
    }
}
