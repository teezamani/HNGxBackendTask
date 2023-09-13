using HNGBACKENDTrack.Dto;
using HNGBACKENDTrack.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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

       /* // GET: api/<PersonsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PersonsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
*/
        // POST api/<PersonsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name)) { return BadRequest(new BaseResponseDto(false ,400, "Name is required")); }
                var callCreateMethod = await IPersonRepository.CreatePerson(name);
                return Created("Created" , callCreateMethod);
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return StatusCode(500, new BaseResponseDto(false, 500, ex.Message)); }

                return StatusCode(500, new BaseResponseDto(false, 500, AppSettings.FailedAttempt));
            }
        }

      /*  // PUT api/<PersonsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
