using ToDoList.Models;
namespace ToDoList.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Foto { get; set; }
        public string Username { get; set; }
        public DateTime UltimoLogin { get; set; }
        public string Pass { get; set; }
    }
}
