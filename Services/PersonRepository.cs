using HNGBACKENDTrack.Dto;
using HNGBACKENDTrack.Services.Interface;

namespace HNGBACKENDTrack.Services
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository()
        {
        }

        public Task<BaseResponseDto> CreatePerson(string name)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto> DeletePerson(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto> GetPerson(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDto> UpdatePerson(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
