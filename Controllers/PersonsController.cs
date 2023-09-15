using HNGBACKENDTrack.Dto;
using HNGBACKENDTrack.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HNGBACKENDTrack.Controllers
{
    [Route("api")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        public AppSettings AppSettings { get; }
        public IPersonRepository IPersonRepository { get; }
        public IExplorerRepository IExplorerRepository { get; }

        public PersonsController(IOptions<AppSettings> appSettings ,IPersonRepository  personRepository , IExplorerRepository explorerRepository)
        {
            AppSettings = appSettings.Value;
            IPersonRepository = personRepository;
            IExplorerRepository = explorerRepository;
        }
      
        // GET api/5
        [HttpGet("{user_id}")]
        public async Task<IActionResult> Get([FromRoute]string user_id)
        {
            try
            {
                //Check if name is not an empty string              
                if (string.IsNullOrWhiteSpace(user_id)) { return BadRequest(new BaseResponseDto(false, 400, "UserId is required")); }

                user_id = user_id.Trim();

                //Check if USERiD is a number
                var iSNumber = IExplorerRepository.CheckIfIsNumber(user_id);
                if (!iSNumber.Status) { return BadRequest(iSNumber); }

                //Call GetPersonby Id
                var activity = await IPersonRepository.GetPerson((int)iSNumber.Data);
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
                model.Name = model.Name.Trim();
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
        public async Task<IActionResult> Put([FromRoute] string user_id ,  [FromBody] PersonNameRequestDto model)
        {
            try
            {   //Check if name is not an empty string              
                if (string.IsNullOrWhiteSpace(model.Name)) { return BadRequest(new BaseResponseDto(false, 400, "Name is required")); }

                model.Name = model.Name.Trim();
                user_id = user_id.Trim();

                //Check if USERiD is a number
                var iSNumber = IExplorerRepository.CheckIfIsNumber(user_id);
                if (!iSNumber.Status) { return BadRequest(iSNumber); }

                //Call Update method
                var activity = await IPersonRepository.UpdatePerson((int)iSNumber.Data , model);
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
        public async Task<IActionResult> Delete([FromRoute]string user_id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user_id)) { return BadRequest(new BaseResponseDto(false, 400, "UserId is required")); }

                user_id = user_id.Trim();

                //Check if USERiD is a number
                var iSNumber = IExplorerRepository.CheckIfIsNumber(user_id);
                if (!iSNumber.Status) { return BadRequest(iSNumber); }

                //Call Delete method
                var activity = await IPersonRepository.DeletePerson((int)iSNumber.Data);
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
