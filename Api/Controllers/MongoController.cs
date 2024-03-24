using AutoMapper;
using Business.Dtos.Course;
using Infrastructure.Entities.MongoDb;
using Infrastructure.Repositories.MongoRepositories;
using Infrastructure.Repositories.SqlRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoController : ControllerBase
    {
        private readonly MongoRepository _courseRepository;
        private readonly IMapper _mapper;

        public MongoController(MongoRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            //var courses = await _testRepo.GetAllAsync();
            //return Ok(courses);



            var result = await _courseRepository.GetAllAsync();
            if (!result.Any())
            {
                return NotFound();
            }

            var courseDto = _mapper.Map <IEnumerable<GetCoursesDto>>(result);
            return Ok(courseDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(string id)
        {
            var result = await _courseRepository.GetOneAsync(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOne(CourseEntity entity)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseRepository.CreateAsync(entity);
                return Ok(result);
            }
            
            return BadRequest();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOne(string id, TestCreateCoruseDto dto)
        {

            var existingCourse = await _courseRepository.GetOneAsync(id);
            if (existingCourse == null)
            {
                return BadRequest();
            }

            dto.CourseId = existingCourse.CourseId;


            var result = await _courseRepository.UpdateAsync(_mapper.Map<CourseEntity>(dto));
            if (result != null)
            {
                return Ok(_mapper.Map<GetCoursesDto>(result));
            }
            return BadRequest();


            //var existing = await _courseRepository.GetOneAsync(id);

            //if (existing is null)
            //{
            //    return BadRequest();
            //}
            //entity.CourseId = existing.CourseId;
            //await _courseRepository.UpdateAsync(entity);
            //return Ok();



        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(string id)
        {
            var result = await _courseRepository.DeleteAsync(id);
            return result ? Ok() : BadRequest();
        }



      
    }
}