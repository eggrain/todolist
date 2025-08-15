namespace todolist.Pages.Goals;

public class DeleteModel(AppDbContext db, UserManager<AppUser> users)
                                        : AppPageModel(db, users)
{
    public async Task<IActionResult> OnPostAsync(string id)
    {
        string userId = UserId();

        Goal? goal = _db.Goals.Include(g => g.Todos).
                            FirstOrDefault(g => g.Id == id && g.UserId == userId);
        if (goal == null) return NotFound();

        foreach (Todo todo in goal.Todos) todo.GoalId = null;
        _db.Goals.Remove(goal);
        await _db.SaveChangesAsync();

        return RedirectToPage("/Goals/Index");
    }
}