//using System.Text.Json.Serialization;

namespace TiendaNamespace;

public class Presupuesto
{
    int idPresupuesto;
    Cliente cliente;
    List<PresupuestoDetalle> detalle;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value;}
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public List<PresupuestoDetalle> Detalle {get => detalle; set => detalle = value;}

    //[JsonConstructor]
    public Presupuesto(){
        detalle = new List<PresupuestoDetalle>();
    }
    public Presupuesto(int idPresupuesto, Cliente cliente, List<PresupuestoDetalle> detalle){
        this.idPresupuesto = idPresupuesto;
        this.cliente = cliente;
        this.detalle = detalle;
    } 
    
    public void MontoPresupuesto()
    {

    }
    public void MontoPresupuestoConIva()
    {

    }
    public int CantidadProductos()
    {
        return 0;
    }
}
