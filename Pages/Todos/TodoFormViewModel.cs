using Microsoft.AspNetCore.Mvc.Rendering;

namespace todolist.Pages.Todos;

public class TodoFormViewModel
{
    public string Description { get; set; } = null!;

    public List<string> SelectedGoalIds { get; set; } = [];

    public List<SelectListItem> AvailableGoals { get; set; } = [];

    public DateOnly OnDate { get; set;  }
}