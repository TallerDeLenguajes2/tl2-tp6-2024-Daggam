namespace ClienteRepositoryNamespace;
using Microsoft.Data.Sqlite;
using TiendaNamespace;

interface ClienteRepository
{
    bool crearCliente(Cliente c);
    bool modificarCliente(Cliente c);
    List<Cliente>? obtenerClientes();
    Cliente? obtenerCliente(int id);

    bool eliminarCliente(int id);
}

class SQLiteClienteRepository : ClienteRepository
{
    string connectionString = @"Data Source=db\Tienda.db;Cache=Shared";
    public bool crearCliente(Cliente c)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"INSERT INTO clientes (nombre, email,telefono) VALUES (@nombre, @email, @telefono);";
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@nombre", c.Nombre);
                command.Parameters.AddWithValue("@email", c.Email);
                command.Parameters.AddWithValue("@telefono", c.Telefono);
                int filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }

    public bool modificarCliente(Cliente c)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = "UPDATE clientes SET nombre=@nombre, email=@email, telefono=@telefono WHERE id_cliente = @id_cliente;";
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@id_cliente", c.IdCliente);
                command.Parameters.AddWithValue("@nombre", c.Nombre);
                command.Parameters.AddWithValue("@email", c.Email);
                command.Parameters.AddWithValue("@telefono", c.Telefono);
                int filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }

    public List<Cliente>? obtenerClientes()
    {
        try
        {
            List<Cliente> clientes = new List<Cliente>();
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = "SELECT * FROM clientes";
                var command = new SqliteCommand(queryString, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    }
                }
                connection.Close();
            }
            return clientes;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public Cliente? obtenerCliente(int id)
    {
        Cliente? cliente = null;
        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = "SELECT * FROM clientes WHERE id_cliente = @id_cliente;";
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@id_cliente", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cliente = new Cliente(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    }
                }
                connection.Close();
            }
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
        return cliente;
    }

    public bool eliminarCliente(int id)
    {
        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = "DELETE FROM clientes WHERE id_cliente=@id_cliente;";
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@id_cliente", id);
                int filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }
        catch (SqliteException e)
        {
            Console.WriteLine(e.Message);
        }
        return false;
    }
}