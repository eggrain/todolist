using Microsoft.EntityFrameworkCore;

namespace todolist.Data;

public class AppDbContext(DbContextOptions opts) : DbContext(opts)
{
    
}