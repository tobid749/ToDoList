namespace ToDoList.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public bool Finalizado { get; set; }
        public int IdUsuarios { get; set; }
    }
}
