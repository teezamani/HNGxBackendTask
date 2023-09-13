using HNGBACKENDTrack.Dto;

namespace HNGBACKENDTrack.Services.Interface
{
    public interface IPersonRepository
    {
        //CreatePerson
        Task<BaseResponseDto> CreatePerson( string name);

        //ReadPerson
        Task<BaseResponseDto> GetPerson(int userId);

        //UpdatePerson
        Task<BaseResponseDto> UpdatePerson(int userId);

        //DeletePerson
        Task<BaseResponseDto> DeletePerson(int userId);

    }
}
