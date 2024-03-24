using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Course
{
    public class AuthorDto
    {
        public string FullName { get; set; } = null!;


        public string Biography { get; set; } = null!;


        public string? ProfileImageUrl { get; set; }


        public string YoutubeUrl { get; set; } = null!;


        public string Subscribers { get; set; } = null!;


        public string FacebookUrl { get; set; } = null!;


        public string Followers { get; set; } = null!;
    }
}
