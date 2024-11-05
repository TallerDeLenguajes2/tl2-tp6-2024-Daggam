namespace PresupuestoRepositoryNamespace;

using System.Data;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using TiendaNamespace;

interface PresupuestoRepository
{
    bool CrearPresupuesto(string nombreDestinatario);
    List<Presupuesto>? ObtenerPresupuestos();
    Presupuesto? ObtenerPresupuesto(int id);
    bool AgregarDetallePresupuesto(int idPresupuesto, int idProducto, int cantidad);
    // bool modificarProducto(int idProducto, string descripcion, int precio);
    // List<Producto>? obtenerProductos();
    // Producto? obtenerProducto(int id);

    // bool eliminarProducto(int id);
}

class SQLitePresupuestoRepository : PresupuestoRepository
{
    string connectionString = @"Data Source=db\Tienda.db;Cache=Shared";
    public bool CrearPresupuesto(string nombreDestinatario)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string queryString = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@NombreDestinatario, @FechaCreacion);";
                var command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@NombreDestinatario", nombreDestinatario);
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
                        string nombreDestinatario = reader.GetString(1);
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
                        presupuestos.Add(new Presupuesto(idPresupuesto,nombreDestinatario, detalles));
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
                        string nombreDestinatario = reader.GetString(1);
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
                        presupuesto = new Presupuesto(idPresupuesto,nombreDestinatario, detalles);
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
}