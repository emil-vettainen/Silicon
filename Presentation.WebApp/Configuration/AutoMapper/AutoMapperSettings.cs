﻿using AutoMapper;
using Business.Dtos.Course;
using Presentation.WebApp.Models.Course;
using Presentation.WebApp.Models.Courses;

namespace Presentation.WebApp.Configuration.AutoMapper
{
    public class AutoMapperSettings : Profile
    {
        public AutoMapperSettings()
        {
            CreateMap<CourseDto, CourseModel>();
            CreateMap<RatingDto, RatingModel>();
            CreateMap<PriceDto, PriceModel>();
            CreateMap<IncludedDto, IncludedModel>();
            CreateMap<AuthorDto, AuthorModel>();
            CreateMap<SocialMediaDto, SocialMediaModel>();
            CreateMap<HighlightsDto, HighlightsModel>();
            CreateMap<ProgramDetailsDto, ProgramDetailsModel>();


            CreateMap<CourseResultDto, CourseResultModel>();
        }
    }
}