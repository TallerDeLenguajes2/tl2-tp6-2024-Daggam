using Microsoft.AspNetCore.Mvc;
using TiendaNamespace;
using UsuarioRepositoryNamespace;
using ViewModel;
namespace tl2_tp6_2024_Daggam.Controllers;

public class LoginController:Controller{
    readonly IUsuarioRepository _usuarioRepository;
    public LoginController(IUsuarioRepository usuarioRepository){
        _usuarioRepository = usuarioRepository;
    }
    [HttpGet]
    public ActionResult Index(){
        return View(new LoginViewModel());
    }

    [HttpPost]
    public ActionResult Autenticar(LoginViewModel loginVM){
        Usuario? usuario = _usuarioRepository.obtenerUsuario(loginVM.Usuario,loginVM.Password);
        if(usuario!=null){
            HttpContext.Session.SetString("Rol",usuario.Rol);
            return RedirectToAction("Index","Home");
            
        }
        return RedirectToAction("Index");
    }
}