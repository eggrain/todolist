using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace todolist.Data;

public class AppDbContext(DbContextOptions opts) : IdentityDbContext<IdentityUser>(opts)
{
    
}