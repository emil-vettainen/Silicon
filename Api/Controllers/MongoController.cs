using Infrastructure.Entities.MongoDb;
using Infrastructure.Repositories.MongoDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoController : ControllerBase
    {
        private readonly MongoRepository _courseRepository;

        public MongoController(MongoRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _courseRepository.GetAllAsync();
            return Ok(result);
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
    }
}
