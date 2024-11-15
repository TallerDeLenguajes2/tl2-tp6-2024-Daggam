namespace PresupuestoRepositoryNamespace;

using Microsoft.Data.Sqlite;
using TiendaNamespace;

interface PresupuestoRepository
{
    bool CrearPresupuesto(int id_cliente);
    List<Presupuesto>? ObtenerPresupuestos();
    Presupuesto? ObtenerPresupuesto(int id);
    bool AgregarDetallePresupuesto(int idPresupuesto, int idProducto, int cantidad);
    bool EliminarPresupuesto(int id);
}

class SQLitePresupuestoRepository : PresupuestoRepository
{
    string connectionString = @"Data Source=db\Tienda.db;Cache=Shared";
    public bool CrearPresupuesto(int id_cliente)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"INSERT INTO Presupuestos (id_cliente, FechaCreacion) VALUES (@id_cliente, @FechaCreacion);";
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@id_cliente", id_cliente);
                command.Parameters.AddWithValue("@FechaCreacion",DateTime.Now.ToString("yyyy-MM-dd"));
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
    
    public List<Presupuesto>? ObtenerPresupuestos()
    {
        try{
            List<Presupuesto> presupuestos = new List<Presupuesto>();
            using(var connection = new SqliteConnection(connectionString)){
                connection.Open();
                //Creo las consultas que hagan lo siguiente
                /*
                    1. Me regresan todos los presupuestos.
                    2. De esos presupuestos, regresar sus DetallePresupuestos
                    3. De esos detallesPresupuestos, sus productos.
                    4. De esos productos, sus datos.
                */
                string queryPresupuesto = "SELECT * FROM Presupuestos;";
                var command = new SqliteCommand(queryPresupuesto,connection);
                using(var reader = command.ExecuteReader()){
                    while(reader.Read()){
                        int idPresupuesto = reader.GetInt32(0);
                        Cliente cliente=null;
                        // int idCliente = reader.GetInt32(1);
                        List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();
                        //Todo lo necesario para obtener los productos de un PresupuestoDetalle dado un idPresupuesto
                        string queryDetalles = @"SELECT idProducto,Descripcion,Precio,Cantidad FROM PresupuestosDetalle
                        INNER JOIN Productos USING(idProducto)
                        WHERE idPresupuesto=@idPresupuesto";
                        var commandDetalles = new SqliteCommand(queryDetalles,connection);
                        commandDetalles.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);
                        using(var reader2 = commandDetalles.ExecuteReader()){
                            while(reader2.Read()){
                                Producto p = new Producto(reader2.GetInt32(0),reader2.GetString(1),reader2.GetInt32(2));
                                detalles.Add(new PresupuestoDetalle(p,reader2.GetInt32(3)));        
                            }
                        }
                        //Query para obtener al cliente
                        string querycliente = @"SELECT id_cliente, nombre, email, telefono FROM clientes
                        INNER JOIN Presupuestos USING(id_cliente)
                        WHERE idPresupuesto=@idPresupuesto;";
                        var commandCliente = new SqliteCommand(querycliente,connection);
                        commandCliente.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);
                        using(var reader2 = commandCliente.ExecuteReader()){
                            while(reader2.Read()){
                                cliente = new Cliente(reader2.GetInt32(0),reader2.GetString(1),reader2.GetString(2),reader2.GetString(3));
                            }
                        }
                        presupuestos.Add(new Presupuesto(idPresupuesto,cliente, detalles));
                    }
                }
                connection.Close();
            }
            return presupuestos;
        }catch(SqliteException e){
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public Presupuesto? ObtenerPresupuesto(int id){
        Presupuesto? presupuesto=null;
        try{
            using(var connection = new SqliteConnection(connectionString)){
                connection.Open();
                string queryPresupuesto = "SELECT idPresupuesto,NombreDestinatario FROM Presupuestos WHERE idPresupuesto=@idPresupuesto";
                var commandPresupuesto = new SqliteCommand(queryPresupuesto,connection);
                commandPresupuesto.Parameters.AddWithValue("@idPresupuesto",id);
                using(var reader = commandPresupuesto.ExecuteReader()){
                    while(reader.Read()){
                        int idPresupuesto = reader.GetInt32(0);
                        Cliente cliente=null;
                        List<PresupuestoDetalle> detalles = new List<PresupuestoDetalle>();
                        //Todo lo necesario para obtener los productos de un PresupuestoDetalle dado un idPresupuesto
                        string queryDetalles = @"SELECT idProducto,Descripcion,Precio,Cantidad FROM PresupuestosDetalle
                        INNER JOIN Productos USING(idProducto)
                        WHERE idPresupuesto=@idPresupuesto";
                        var commandDetalles = new SqliteCommand(queryDetalles,connection);
                        commandDetalles.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);
                        using(var reader2 = commandDetalles.ExecuteReader()){
                            while(reader2.Read()){
                                Producto p = new Producto(reader2.GetInt32(0),reader2.GetString(1),reader2.GetInt32(2));
                                detalles.Add(new PresupuestoDetalle(p,reader2.GetInt32(3)));        
                            }
                        }
                        //Query para obtener al cliente
                        string querycliente = @"SELECT id_cliente, nombre, email, telefono FROM clientes
                        INNER JOIN Presupuestos USING(id_cliente)
                        WHERE idPresupuesto=@idPresupuesto;";
                        var commandCliente = new SqliteCommand(querycliente,connection);
                        commandCliente.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);
                        using(var reader2 = commandCliente.ExecuteReader()){
                            while(reader2.Read()){
                                cliente = new Cliente(reader2.GetInt32(0),reader2.GetString(1),reader2.GetString(2),reader2.GetString(3));
                            }
                        }
                        presupuesto = new Presupuesto(idPresupuesto,cliente, detalles);
                    }
                }                
                connection.Close();
            }
        }catch(SqliteException e){
            Console.WriteLine(e.Message);
        }
        return presupuesto;
    }
    public bool AgregarDetallePresupuesto(int idPresupuesto,int idProducto,int cantidad)
    {
        try{
            using(var connection = new SqliteConnection(connectionString)){
                connection.Open();
                string querySelect = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, cantidad) VALUES (@idPresupuesto, @idProducto, @cantidad);";
                var command = new SqliteCommand(querySelect,connection);
                command.Parameters.AddWithValue("@idPresupuesto",idPresupuesto);
                command.Parameters.AddWithValue("@idProducto",idProducto);
                command.Parameters.AddWithValue("@cantidad",cantidad);
                int filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }catch(SqliteException e){
            Console.WriteLine(e.Message);
        }
        return false;
    }

    public bool EliminarPresupuesto(int id)
    {
        try
        {
            using(var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = "DELETE FROM Presupuestos WHERE idPresupuesto = @idPresupuesto;";
                var command = new SqliteCommand(queryString,connection);
                command.Parameters.AddWithValue("@idPresupuesto",id);
                int filasAfectadas = command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }catch(SqliteException e){
            Console.WriteLine(e.Message);
        }
        return false;
    }        
}