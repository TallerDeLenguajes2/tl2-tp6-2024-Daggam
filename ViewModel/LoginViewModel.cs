using System.ComponentModel.DataAnnotations;

namespace ViewModel;
public class LoginViewModel{
    [Required(ErrorMessage = "El campo usuario es requerido")]
    public string Usuario{get;set;}
    [Required(ErrorMessage = "El campo contraseña es requerido")]
    public string Password{get;set;}
}