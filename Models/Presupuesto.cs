//using System.Text.Json.Serialization;

namespace TiendaNamespace;

public class Presupuesto
{
    int idPresupuesto;
    string nombreDestinatario;
    List<PresupuestoDetalle> detalle;

    public int IdPresupuesto { get => idPresupuesto; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public List<PresupuestoDetalle> Detalle {get => detalle;}

    //[JsonConstructor]
    public Presupuesto(){
        detalle = new List<PresupuestoDetalle>();
    }
    public Presupuesto(int idPresupuesto, string nombreDestinatario, List<PresupuestoDetalle> detalle){
        this.idPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
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
