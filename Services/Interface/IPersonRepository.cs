using HNGBACKENDTrack.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HNGBACKENDTrack.Services.Interface
{
    public interface IPersonRepository
    {
        //CreatePerson
        Task<BaseResponseDto> CreatePerson( string name);

        //ReadPersonById
        Task<BaseResponseDto> GetPerson(int Id);
        //UpdatePerson
        Task<BaseResponseDto> UpdatePerson(int Id,  PersonNameRequestDto model);

        //DeletePerson
        Task<BaseResponseDto> DeletePerson(int Id);
    }
}
