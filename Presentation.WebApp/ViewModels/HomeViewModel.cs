using Presentation.WebApp.Models;

namespace Presentation.WebApp.ViewModels;

public class HomeViewModel
{
    public List<ToolBoxModel> ToolBoxes { get; set; } = [

        new() { ImageUrl = "/images/tools/chat.svg", ImageAlt = "Comments on Tasks", Title = "Comments on Tasks", Description = "Id mollis consectetur congue egestas egestas suspendisse blandit justo.",},
        new() { ImageUrl = "/images/tools/presentation.svg", ImageAlt = "Tasks Analytics", Title = "Tasks Analytics", Description = "Non imperdiet facilisis nulla tellus Morbi scelerisque eget adipiscing vulputate.",},
        new() { ImageUrl = "/images/tools/add-group.svg", ImageAlt = "Multiple Assignees", Title = "Multiple Assignees", Description = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris.",},
        new() { ImageUrl = "/images/tools/bell.svg", ImageAlt = "Notifications", Title = "Notifications", Description = "Diam, suspendisse velit cras ac. Lobortis diam volutpat, eget pellentesque viverra.",},
        new() { ImageUrl = "/images/tools/subtasks.svg", ImageAlt = "Sections & Subtasks", Title = "Sections & Subtasks", Description = "Mi feugiat hac id in. Sit elit placerat lacus nibh lorem ridiculus lectus.",},
        new() { ImageUrl = "/images/tools/shield.svg", ImageAlt = "Data Security", Title = "Data Security", Description = "Aliquam malesuada neque eget elit nulla vestibulum nunc cras.",},

    ];

    public List<UnsortedListModel> UnsortedList { get; set; } = [

        new() {Icon = "fa-circle-check", Description = "Powerful project management"},
        new() {Icon = "fa-circle-check", Description = "Transparent work management"},
        new() {Icon = "fa-circle-check", Description = "Manage work & focus on the most important tasks"},
        new() {Icon = "fa-circle-check", Description = "Track your progress with interactive charts"},
        new() {Icon = "fa-circle-check", Description = "Easiest way to track time spent on tasks"}

    ];
}
