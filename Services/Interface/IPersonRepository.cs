using HNGBACKENDTrack.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HNGBACKENDTrack.Services.Interface
{
    public interface IPersonRepository
    {
        //CreatePerson
        Task<BaseResponseDto> CreatePerson( string name);

        //ReadPersonById
        Task<BaseResponseDto> GetPerson(int userId);
        //UpdatePerson
        Task<BaseResponseDto> UpdatePerson(int user_id,  PersonNameRequestDto model);

        //DeletePerson
        Task<BaseResponseDto> DeletePerson(int userId);

    }
}
