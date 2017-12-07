using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace FileProcessor
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base("TodoContext")
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

    }
}
