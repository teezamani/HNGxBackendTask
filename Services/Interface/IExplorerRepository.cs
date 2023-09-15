using HNGBACKENDTrack.Dto;

namespace HNGBACKENDTrack.Services.Interface
{
    public interface IExplorerRepository
    {
        BaseResponseDto CheckIfIsNumber(string UserId);
    }
}
