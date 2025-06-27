using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace todolist.Data;

public class AppDbContext(DbContextOptions opts) : IdentityDbContext<AppUser>(opts)
{
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<ProgressNote> ProgressNotes { get; set;  }
}