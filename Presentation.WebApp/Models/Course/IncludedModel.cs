﻿namespace Presentation.WebApp.Models.Course;

public class IncludedModel
{
    public int HoursDemandVideo { get; set; }
    public int Articles {  get; set; }
    public int Resourses { get; set; }
    public bool LifetimeAccess { get; set; } = false;
    public bool Certificate { get; set; } = false;
}