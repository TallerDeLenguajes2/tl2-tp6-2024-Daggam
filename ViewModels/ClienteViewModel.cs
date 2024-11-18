using System.ComponentModel.DataAnnotations;

namespace ViewModel;

public class ClienteViewModel{
    public int IdCliente{set;get;}
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre {set;get;}
    [EmailAddress(ErrorMessage = "El correo no es válido.")]
    public string? Email {set;get;}
    [Phone(ErrorMessage = "Ingrese un numero de telefono válido.")]
    public string? Telefono{set;get;}

    public ClienteViewModel(){}
}