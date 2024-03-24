using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Course
{
    public class PriceDto
    {
        public string OriginalPrice { get; set; } = null!;


        public string? DiscountPrice { get; set; }
    }

}

