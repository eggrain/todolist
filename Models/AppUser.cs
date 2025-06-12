using Microsoft.AspNetCore.Identity;

namespace todolist.Models;

public class AppUser : IdentityUser
{
    public List<Todo> Todos { get; set; } = [];
    public List<Project> Projects { get; set; } = [];
    public List<Goal> Goals { get; set; } = [];
}