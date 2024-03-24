using Business.Dtos.Subscribe;
using Business.Services.SubscriberServices;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses.Enums;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController(SubscribeService subscribeService) : ControllerBase
    {
        private readonly SubscribeService _subscribeService = subscribeService;

        #region CREATE
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubsriberDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Email))
            {
                if (dto.DailyNewsletter || dto.AdvertisingUpdates || dto.WeenInReview || dto.EventUpdates || dto.StartupsWeekly || dto.Podcasts)
                {
                    var result = await _subscribeService.CreateAsync(dto);
                    return result.StatusCode switch
                    {
                        ResultStatus.OK => Created("", null),
                        ResultStatus.EXISTS => Conflict("Your email address is already subscribed."),
                        _ => Problem("An unexpected error occurred. Please try again!"),
                    };
                }
            }
            return BadRequest();    
        }
        #endregion

        #region READ

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _subscribeService.GetAsync();
            if (result.Any())
            {
                return Ok(result);  
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _subscribeService.GetAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateSubsriberDto dto)
        {
            if (dto.DailyNewsletter || dto.AdvertisingUpdates || dto.WeenInReview || dto.EventUpdates || dto.StartupsWeekly || dto.Podcasts)
            {
                var result = await _subscribeService.UpdateAsync(id, dto);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            return BadRequest();
        }
        #endregion

        #region DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _subscribeService.DeleteAsync(id);   
            return result ? Ok() : NotFound();
        }
        #endregion
    }
} => 