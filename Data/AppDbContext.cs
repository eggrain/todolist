using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using todolist.Models;

namespace todolist.Data;

public class AppDbContext(DbContextOptions opts) : IdentityDbContext<AppUser>(opts)
{
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Todo> Todos { get; set;  }
}