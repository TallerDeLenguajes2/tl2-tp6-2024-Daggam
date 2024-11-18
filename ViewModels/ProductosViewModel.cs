using System.ComponentModel.DataAnnotations;
namespace ViewModel;

public class ProductosViewModel{
    public int IdProducto{get;set;}
    [StringLength(250,ErrorMessage = "La descripcion del producto no debe superar los 250 caracteres")]
    public string? Descripcion{get;set;}
    [Required]
    [Range(1,int.MaxValue,ErrorMessage ="El valor ingresado debe ser positivo")]
    public int Precio{get;set;}

    public ProductosViewModel(){}
}