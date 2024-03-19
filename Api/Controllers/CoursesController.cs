using Business.Dtos.Course;
using Business.Services.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAll();
            if (courses != null)
            {
                return Ok(courses);
            }
            return BadRequest();
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var course = await _courseService.GetOne(id);
            if (course != null)
            {
                return Ok(course);
            }
            return NotFound();  
        }




        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            if (ModelState.IsValid)
            {
                if (!await _courseService.Exists(dto))
                {
                    var result = await _courseService.CreateAsync(dto);
                    if (result != null)
                    {
                        return Created("", result);
                    }
                }
                else
                {
                    return Conflict("A course with the same title already exists");
                }

            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(CreateCourseDto dto, int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseService.UpdateCourseAsync(dto, id);
                if (result != null)
                {
                    return Ok(result);
                }

                return Conflict();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _courseService.DeleteAsync(id);
            return result ? Ok() : NotFound();

        }
            


    }
}
