namespace UsuarioRepositoryNamespace;
using Microsoft.Data.Sqlite;
using TiendaNamespace;

public interface IUsuarioRepository
{
    bool crearUsuario(Usuario user);
    bool modificarUsuario(Usuario user);
    List<Usuario>? obtenerUsuarios();
    Usuario? obtenerUsuario(int id);
    bool eliminarUsuario(int id);
}

public class SQLiteUsuarioRepository : IUsuarioRepository
{
    string connectionString = @"Data Source=db\Tienda.db;Cache=Shared";
    public bool crearUsuario(Usuario user)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"INSERT INTO usuario (nombre, usuario,contraseña,rol) VALUES (@Nombre, @Usuario,@Contraseña,@Rol);";
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@Nombre", user.Nombre);
                command.Parameters.AddWithValue("@Usuario",user.Username);
                command.Parameters.AddWithValue("@Contraseña",user.Password);
                command.Parameters.AddWithValue("@Rol",user.Rol);
                int filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }
    public bool modificarUsuario(Usuario user){    
        try{
            using(SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"UPDATE usuario SET nombre=@Nombre
                                       usuario=@Usuario
                                       contraseña=@Contraseña
                                       rol=@Rol
                                       WHERE id_usuario=@idUsuario;";
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@IdUsuario",user.IdUsuario);
                command.Parameters.AddWithValue("@Nombre", user.Nombre);
                command.Parameters.AddWithValue("@Usuario",user.Username);
                command.Parameters.AddWithValue("@Contraseña",user.Password);
                command.Parameters.AddWithValue("@Rol",user.Rol);
                int filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
        return false;
    }

    public List<Usuario>? obtenerUsuarios()
    {
        try{
            List<Usuario> usuarios = new List<Usuario>();
            using(SqliteConnection connection = new SqliteConnection(connectionString)){
                connection.Open();
                string queryString = "SELECT * FROM usuario";
                var command = new SqliteCommand(queryString,connection);
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        usuarios.Add(new Usuario(){IdUsuario=reader.GetInt32(0),
                                                   Nombre=reader.GetString(1),
                                                   Username=reader.GetString(2),
                                                   Password=reader.GetString(3),
                                                   Rol=reader.GetString(4)});
                    }
                }
                connection.Close();
            } 
            return usuarios;
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public Usuario? obtenerUsuario(int id){
        Usuario? usuario=null;
        try{
            using(var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = "SELECT * FROM usuario WHERE id_usuario=@idUsuario;";
                var command = new SqliteCommand(queryString,connection);
                command.Parameters.AddWithValue("@idUsuario",id);
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

    public bool eliminarUsuario(int id)
    {
        try
        {
            using(var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = "DELETE FROM usuario WHERE id_usuario= @idUsuario;";
                var command = new SqliteCommand(queryString,connection);
                command.Parameters.AddWithValue("@idUsuario",id);
                int filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
        return false;
    }
}