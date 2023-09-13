using HNGBACKENDTrack.Dto;
using HNGBACKENDTrack.Model;
using HNGBACKENDTrack.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HNGBACKENDTrack.Services
{
    public class PersonRepository : IPersonRepository
    {
        public AppSettings AppSettings { get; }
        public HNGxDBContext HNGxDBContext { get; }

        public PersonRepository(IOptions<AppSettings> appSettings , HNGxDBContext hNGxDBContext)
        {
            AppSettings = appSettings.Value;
            HNGxDBContext = hNGxDBContext;
        }


        public async Task<BaseResponseDto> CreatePerson(string name)
        {
            try
            {
                var nameExist = await NameExists(name);
                if (!nameExist.Status) { return new BaseResponseDto(false,400, "Name already exist"); }

                var Person = new Person
                {
                    Name = name    
                };
               var create = await  HNGxDBContext.Persons.AddAsync(Person);
               var SaveCreation = await HNGxDBContext.SaveChangesAsync();
               if (SaveCreation > 0 ) { return new BaseResponseDto(true, 201, AppSettings.SuccessfullAttempt); }

               //get the new ecord and return as data
                return new BaseResponseDto(false,500, AppSettings.FailedAttempt , SaveCreation);
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return new BaseResponseDto(false ,500, ex.Message ); }

                return new BaseResponseDto(false, 500, AppSettings.FailedAttempt);
            }
        }

        private async Task<BaseResponseDto> NameExists(string name)
        {
            try
            {
                var getname = await HNGxDBContext.Persons.Where(x => x.Name == name).FirstOrDefaultAsync();
                if (getname == null) { return new BaseResponseDto(true); }
                return new BaseResponseDto(false);
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return new BaseResponseDto(false,500, ex.Message); }

                return new BaseResponseDto(false, 500, AppSettings.FailedAttempt);
            }
        }

        public async Task<BaseResponseDto> DeletePerson(int userId)
        {
            try
            {
                var GetUserId = await HNGxDBContext.Persons.Where(d => d.Id == userId).FirstOrDefaultAsync();
                if (GetUserId == null) { return new BaseResponseDto(false, 400, "Record does not exist"); }

                var deleteUserId = HNGxDBContext.Remove(HNGxDBContext.Persons.Where(d => d.Id == userId));
                var SaveActivity = await HNGxDBContext.SaveChangesAsync();
                if (SaveActivity > 0) { return new BaseResponseDto(true, 200, AppSettings.SuccessfullAttempt); }

                return new BaseResponseDto(false, 500, AppSettings.FailedAttempt);
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return new BaseResponseDto(false, 500, ex.Message); }

                return new BaseResponseDto(false, 500, AppSettings.FailedAttempt);
            }
        }

        public async Task<BaseResponseDto> GetPerson(int userId)
        {
            try
            {
                var GetUserId = await HNGxDBContext.Persons.Where(d => d.Id == userId).FirstOrDefaultAsync();
                if(GetUserId == null) { return new BaseResponseDto(false, 400,"Record does not exist");}

                return new BaseResponseDto(true, 200, AppSettings.SuccessfullAttempt , GetUserId); 

            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return new BaseResponseDto(false, 500, ex.Message); }

                return new BaseResponseDto(false, 500, AppSettings.FailedAttempt);
            }
        }

        public async Task<BaseResponseDto> UpdatePerson(int userId ,string Name)
        {
            try
            {
                var GetUserId = await HNGxDBContext.Persons.Where(d => d.Id == userId).FirstOrDefaultAsync();
                if (GetUserId != null) 
                { 
                    GetUserId.Name = Name;
                    var SaveActivity = await HNGxDBContext.SaveChangesAsync();
                    if (SaveActivity > 0) { return new BaseResponseDto(true, 200, AppSettings.SuccessfullAttempt); }

                    return new BaseResponseDto(false, 500, AppSettings.FailedAttempt); 
                }

                return new BaseResponseDto(true, 200, AppSettings.SuccessfullAttempt, GetUserId);
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return new BaseResponseDto(false, 500, ex.Message); }

                return new BaseResponseDto(false, 500, AppSettings.FailedAttempt);
            }
        }
    }
}
