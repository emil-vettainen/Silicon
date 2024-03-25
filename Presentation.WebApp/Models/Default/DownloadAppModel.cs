namespace Presentation.WebApp.Models.Default;

public class DownloadAppModel
{
    public string Title { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal StarRating { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string ImageAlt { get; set; } = null!;
    public string? DownloadUrl { get; set; }
}