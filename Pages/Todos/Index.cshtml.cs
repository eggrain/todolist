namespace todolist.Pages.Todos;

public class IndexModel(AppDbContext db, UserManager<AppUser> users)
                                        : AppPageModel(db, users)
{
    public List<Todo> Todos { get; set; } = [];

    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public string Search { get; set; } = "";

    [BindProperty(SupportsGet = true)] public bool ShowCompleted { get; set; }

    public async Task<IActionResult> OnGetAsync(int pageNum = 1, int pageSize = 30, string search = "")
    {
        PageNum = Math.Max(1, pageNum);
        PageSize = Math.Clamp(pageSize, 1, 50);
        Search = search;

        string userId = UserId();

        var baseQuery = _db.Todos
                    .Where(t => t.UserId == userId)
                    .AsNoTracking();

        if (ShowCompleted == false)
            baseQuery = baseQuery.Where(t => t.Completed == false);

        if (!string.IsNullOrWhiteSpace(Search))
            baseQuery = baseQuery.Where(t => t.Description.ToLower().Contains(Search.ToLower()));

        TotalCount = await baseQuery.CountAsync(HttpContext.RequestAborted);
        TotalPages = Math.Max((int)Math.Ceiling(TotalCount / (double)PageSize), 1);

        if (PageNum > TotalPages) PageNum = TotalPages;

        Todos = await baseQuery
            .Include(t => t.Project)
            .Include(t => t.Goal)
            .OrderBy(t => t.OnDate == null)
            .ThenBy(t => t.OnDate)
            .Skip((PageNum - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync(HttpContext.RequestAborted);

        return Page();
    }                                    
}