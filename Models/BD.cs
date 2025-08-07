using Microsoft.Data.SqlClient;
using Dapper;

namespace ToDoList.Models
{
    public static class BD
    {
        private static string connectionString = "Server=.;Database=ToDoList;Trusted_Connection=True;";

        public static bool Registrarse(Usuario u)
        {
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                string chequeo = "SELECT COUNT(*) FROM Usuarios WHERE Username = @Username";
                int existe = db.ExecuteScalar<int>(chequeo, new { u.Username });

                if (existe > 0)
                    return false;

                string insert = @"INSERT INTO Usuarios (Nombre, Apellido, Foto, Username, UltimoLogin, Pass) 
                                  VALUES (@Nombre, @Apellido, @Foto, @Username, @UltimoLogin, @Pass)";

               var parametros = new
                {
                 Nombre = u.Nombre,
                Apellido = u.Apellido,
                Foto = u.Foto,
                Username = u.Username,
                Pass = u.Pass,
        UltimoLogin = DateTime.Now
};

db.Execute(insert, parametros);


                return true;
            }
        }

        public static Usuario Login(string username, string pass)
        {
         using (SqlConnection db = new SqlConnection(connectionString))
            {
             string sql = "SELECT * FROM Usuarios WHERE Username = @Username AND Pass = @Pass";
                return db.QueryFirstOrDefault<Usuario>(sql, new { Username = username, Pass = pass });
            }
        }

        public static List<Tarea> DevolverTareas(int idUsuarios)
        {
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Tareas WHERE IdUsuarios = @Id AND Finalizado = 0";
            return db.Query<Tarea>(sql, new { Id = idUsuarios }).ToList();
            }
        }

        public static Tarea DevolverTarea(int idTarea)
        {
            using (SqlConnection db = new SqlConnection(connectionString))
        {
                string sql = "SELECT * FROM Tareas WHERE Id = @Id";
                return db.QueryFirstOrDefault<Tarea>(sql, new { Id = idTarea });
            }
        }

        public static void CrearTarea(Tarea t)
        {
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Tareas (Titulo, Descripcion, Fecha, Finalizado, IdUsuarios) 
                               VALUES (@Titulo, @Descripcion, @Fecha, 0, @IdUsuarios)";
            db.Execute(sql, t);
            }
        }

        public static void ModificarTarea(Tarea t)
        {
            using (SqlConnection db = new SqlConnection(connectionString))
            {
             string sql = @"UPDATE Tareas 
                               SET Titulo = @Titulo, Descripcion = @Descripcion, Fecha = @Fecha 
                               WHERE Id = @Id";
                db.Execute(sql, t);
            }
        }

        public static void EliminarTarea(int idTarea)
        {
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Tareas WHERE Id = @Id";
             db.Execute(sql, new { Id = idTarea });
            }
        }

        public static void FinalizarTarea(int idTarea)
        {
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Tareas SET Finalizado = 1 WHERE Id = @Id";
                db.Execute(sql, new { Id = idTarea });
            }
        }

        public static void ActualizarLogin(int idUsuarios)
        {
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Usuarios SET UltimoLogin = @Fecha WHERE Id = @Id";
                db.Execute(sql, new { Fecha = DateTime.Now, Id = idUsuarios });
            }
        }
    }
}
