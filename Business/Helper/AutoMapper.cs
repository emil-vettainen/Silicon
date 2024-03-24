using AutoMapper;
using Business.Dtos.Course;
using Business.Dtos.Subscribe;
using Business.Dtos.Subscriber;
using Infrastructure.Entities.MongoDb;
using Infrastructure.Entities.SubscribeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //Entity => Dto

            CreateMap<CourseEntity, TestCreateCoruseDto>();
            CreateMap<CourseEntity, GetCoursesDto>();


            CreateMap<RatingEntity, RatingDto>();
            CreateMap<PriceEntity, PriceDto>();
            CreateMap<IncludedEntity, IncludedDto>();
            CreateMap<AuthorEntity, AuthorDto>();
            CreateMap<ContentEntity, ContentDto>();

            CreateMap<SubscribeEntity, GetSubscriberDto>();


            // Dto => Entity

            CreateMap<TestCreateCoruseDto, CourseEntity>();
           


            CreateMap<RatingDto, RatingEntity>();
            CreateMap<PriceDto, PriceEntity>();
            CreateMap<IncludedDto, IncludedEntity>();
            CreateMap<AuthorDto, AuthorEntity>();
            CreateMap<ContentDto, ContentEntity>();

            CreateMap<CreateSubsriberDto, SubscribeEntity>();





        }


    }
}
