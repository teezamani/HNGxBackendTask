using HNGBACKENDTrack.Dto;
using Microsoft.Extensions.Options;

namespace HNGBACKENDTrack.Services.Interface
{
    public class ExplorerRepository : IExplorerRepository
    {
        public AppSettings AppSettings { get; }

        public ExplorerRepository(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
        }

        public BaseResponseDto CheckIfIsNumber(string UserId)
        {
            try
            { 
                //Check if USERiD is a number
                int id;
                bool success = int.TryParse(UserId, out id);

                if (!success) { return new BaseResponseDto(false, 400, "UserId must be a number"); }
                return new BaseResponseDto(true, 200, AppSettings.SuccessfullAttempt ,id );
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return new BaseResponseDto(false, 500, ex.Message); }
                return new BaseResponseDto(false, 500, AppSettings.FailedAttempt);
            }
        }
    }
}
