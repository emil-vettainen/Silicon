using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Course
{
    public class TestDto
    {
        public string Title { get; set; } = null!;


        public string Description { get; set; } = null!;


        public int Duration { get; set; }

        public bool IsBestSeller { get; set; } = false;


        public string ModuleTitle { get; set; } = null!;


        public string ModuleDescription { get; set; } = null!;

        public int HoursOfVideo { get; set; }


        public int Articles { get; set; }


        public int Resourses { get; set; }

        public bool LifetimeAccess { get; set; } = false;


        public bool Certificate { get; set; } = false;

        public string FullName { get; set; } = null!;


        public string Biography { get; set; } = null!;


        public string? ProfileImageUrl { get; set; }


        public string YoutubeUrl { get; set; } = null!;


        public string Subscribers { get; set; } = null!;


        public string FacebookUrl { get; set; } = null!;


        public string Followers { get; set; } = null!;


        public string OriginalPrice { get; set; } = null!;


        public string? DiscountPrice { get; set; }

        public string? InNumbers { get; set; }


        public string? InProcent { get; set; }
    }
}
