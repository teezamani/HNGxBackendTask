using HNGBACKENDTrack.Dto;
using HNGBACKENDTrack.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Numerics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HNGBACKENDTrack.Controllers
{
    [Route("api")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        public AppSettings AppSettings { get; }
        public IPersonRepository IPersonRepository { get; }

        public PersonsController(IOptions<AppSettings> appSettings ,IPersonRepository  personRepository)
        {
            AppSettings = appSettings.Value;
            IPersonRepository = personRepository;
        }
      
        // GET api/5
        [HttpGet("{user_id}")]
        public async Task<IActionResult> Get([FromRoute]int user_id)
        {
            try
            {
                //Call GetPersonby Id
                var activity = await IPersonRepository.GetPerson(user_id);
                if (activity.Status_code == 404) { return NotFound(activity); }

                return Ok(activity);
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return StatusCode(500, new BaseResponseDto(false, 500, ex.Message)); }

                return StatusCode(500, new BaseResponseDto(false, 500, AppSettings.FailedAttempt));
            }
        }

        // POST /api
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonNameRequestDto model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name)) { return BadRequest(new BaseResponseDto(false ,400, "Name is required")); }
                var callCreateMethod = await IPersonRepository.CreatePerson(model.Name);
                return CreatedAtAction(null,callCreateMethod);
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return StatusCode(500, new BaseResponseDto(false, 500, ex.Message)); }

                return StatusCode(500, new BaseResponseDto(false, 500, AppSettings.FailedAttempt));
            }
        }

        // PUT api/{user_id}
        [HttpPut("{user_id}")]
        public async Task<IActionResult> Put([FromRoute] int user_id ,  [FromBody] PersonNameRequestDto model)
        {
            try
            {   //Check if name is not an empty string              
                if (string.IsNullOrWhiteSpace(model.Name)) { return BadRequest(new BaseResponseDto(false, 400, "Name is required")); }

                //Call Update method
                var activity = await IPersonRepository.UpdatePerson(user_id , model);
                if (activity.Status_code == 404) { return NotFound(activity); }
                    
                return Ok(activity);
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return StatusCode(500, new BaseResponseDto(false, 500, ex.Message)); }
                return StatusCode(500, new BaseResponseDto(false, 500, AppSettings.FailedAttempt));
            }
        }

        // DELETE api/{user_id}
        [HttpDelete("{user_id}")]
        public async Task<IActionResult> Delete([FromRoute]int user_id)
        {
            try
            {         
                //Call Delete method
                var activity = await IPersonRepository.DeletePerson(user_id);
                if (activity.Status_code == 404) { return NotFound(activity); }

                return Ok(activity);
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return StatusCode(500, new BaseResponseDto(false, 500, ex.Message)); }
                return StatusCode(500, new BaseResponseDto(false, 500, AppSettings.FailedAttempt));
            }
        }
    }
}
