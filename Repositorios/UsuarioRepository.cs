namespace UsuarioRepositoryNamespace;
using Microsoft.Data.Sqlite;
using TiendaNamespace;
//Hice mal esto. Debo crear un repositorio para autenticar un usuario.
public interface IUsuarioRepository
{
    Usuario? obtenerUsuario(string username, string password);
}

public class SQLiteUsuarioRepository : IUsuarioRepository
{
    string connectionString = @"Data Source=db\Tienda.db;Cache=Shared";
    public Usuario? obtenerUsuario(string username,string password){
        Usuario? usuario=null;
        try{
            using(var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = "SELECT * FROM usuario WHERE usuario=@Usuario AND contraseña=@Contraseña;";
                var command = new SqliteCommand(queryString,connection);
                command.Parameters.AddWithValue("@Usuario",username);
                command.Parameters.AddWithValue("@Contraseña",password);
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        usuario = new Usuario(){IdUsuario=reader.GetInt32(0),
                                                   Nombre=reader.GetString(1),
                                                   Username=reader.GetString(2),
                                                   Password=reader.GetString(3),
                                                   Rol=reader.GetString(4)};
                    }
                }
                connection.Close();
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
        return usuario;
    }
}