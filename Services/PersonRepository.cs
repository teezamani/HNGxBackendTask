using HNGBACKENDTrack.Dto;
using HNGBACKENDTrack.Services.Interface;

namespace HNGBACKENDTrack.Services
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository()
        {
        }

        public Task<BaseResponseDto> CreatePerson(PersonDto personDto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto> DeletePerson(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto> GetPerson(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto> UpdatePerson(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
