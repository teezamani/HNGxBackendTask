using HNGBACKENDTrack.Dto;

namespace HNGBACKENDTrack.Services.Interface
{
    public interface IPersonRepository
    {
        //CreatePerson
        Task<BaseResponseDto> CreatePerson( PersonDto personDto);

        //ReadPerson
        Task<BaseResponseDto> GetPerson(long userId);

        //UpdatePerson
        Task<BaseResponseDto> UpdatePerson(long userId);

        //DeletePerson
        Task<BaseResponseDto> DeletePerson(long userId);

    }
}
