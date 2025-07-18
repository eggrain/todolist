namespace todolist.Pages.Todos;

public class EditModel(AppDbContext db, UserManager<AppUser> users)
                : AppPageModel(db, users)
{
    [BindProperty] public Todo Todo { get; set; } = null!;
    [BindProperty] public string? ReturnUrl { get; set; } = null;
    [BindProperty] public string Id { get; set; } = null!;

    private async Task<Todo?> GetTodo()
    {
        string userId = UserId();
        return await _db.Todos
                .Where(t => t.Id == Id && t.UserId == userId).FirstOrDefaultAsync();
    }

    private LocalRedirectResult RedirectToReturnUrlOrTodo()
    {
        if (ReturnUrl != null)
            return LocalRedirect(ReturnUrl);

        return LocalRedirect($"/Todos/Show/{Id}");
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Todo? todo = await GetTodo();
        if (todo == null) return NotFound();

        Todo = todo;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        Todo? todo = await GetTodo();
        if (todo == null) return NotFound();

        todo.Description = Todo.Description;
        todo.OnDate = Todo.OnDate;
        todo.StartTime = Todo.StartTime;
        todo.Completed = Todo.Completed;
        todo.EndTime = Todo.EndTime;
        todo.GoalId = Todo.GoalId;
        todo.ProjectId = Todo.ProjectId;

        await _db.SaveChangesAsync();

        return RedirectToReturnUrlOrTodo();
    }

    public async Task<IActionResult> OnPostCompleteTodoAsync()
    {
        Todo? todo = await GetTodo();
        if (todo == null) return NotFound();

        todo.Completed = true;
        await _db.SaveChangesAsync();
        return RedirectToReturnUrlOrTodo();
    }

    public async Task<IActionResult> OnPostUncompleteTodoAsync()
    {
        Todo? todo = await GetTodo();
        if (todo == null) return NotFound();

        todo.Completed = false;
        await _db.SaveChangesAsync();
        return RedirectToReturnUrlOrTodo();
    }

    public async Task<IActionResult> OnPostFailTodoAsync()
    {
        Todo? todo = await GetTodo();
        if (todo == null || !todo.OnDate.HasValue)
            return RedirectToReturnUrlOrTodo();

        DateOnly onDate = todo.OnDate.Value;
        DateOnly today  = DateOnly.FromDateTime(DateTime.Now);

        if (onDate <= today)
            todo.OnDate = today.AddDays(1);

        await _db.SaveChangesAsync();
        return RedirectToReturnUrlOrTodo();
    }
}
