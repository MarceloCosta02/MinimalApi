using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Data
{
    public class Context : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
}
