using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Course
{
    public class IncludedDto
    {
        public int HoursOfVideo { get; set; }


        public int Articles { get; set; }


        public int Resourses { get; set; }

        public bool LifetimeAccess { get; set; } = false;


        public bool Certificate { get; set; } = false;
    }
}
