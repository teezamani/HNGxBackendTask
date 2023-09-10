using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HNGBACKENDTrack.Controllers
{
    [Route("api")]
    [ApiController]
    public class TaskOneController : ControllerBase
    {
        public AppSettings AppSettings { get; }

     
        public record ToAsk(string Slack_name, string Track);     
        public TaskOneController(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
        }
        
        [HttpGet(Name = "")]
        public IActionResult  GetResult([FromQueryAttribute] ToAsk request) 
        {           
            if (request.Track.ToLower() != "backend")
                return BadRequest(new { Message = "Specified Track is not accepted" });

            return Ok ( new 
                { 
                    slack_name  = "HNGx" , 
                    current_day  = DateTime.Today.DayOfWeek.ToString(), 
                    utc_time  = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"), 
                    track  = "backend", 
                    github_file_url  = AppSettings.Github_file_url , 
                    github_repo_url = AppSettings.Github_repo_url , 
                    status_code  = 200
                });
        }  
    }
}
