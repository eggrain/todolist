namespace todolist.Pages.Weeks;

public class CurrentWeekModel(AppDbContext db, UserManager<AppUser> users)
                                             : AppPageModel(db, users)
{
    public List<DateTime> DaysOfWeek { get; private set; } = [];
    public string MonthYear { get; private set; } = null!;

    public void OnGet()
    {
        DateTime today = DateTime.Today;
        int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
        DateTime monday = today.AddDays(-diff);

        DaysOfWeek = [.. Enumerable.Range(0, 7).Select(i => monday.AddDays(i))];
        MonthYear = today.ToString("MMMM yyyy").ToUpper();
    }
}