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
                //check if name already exist
                var nameExist = await NameExists(name);
                if (!nameExist.Status) { return new BaseResponseDto(false,400, "Name already exist"); }

                //Person object to create
                var Person = new Person { Name = name.Trim()};
              
                //use ORM to talk to Db and create the new person
                var create = await HNGxDBContext.Persons.AddAsync(Person);
                var SaveCreation = await HNGxDBContext.SaveChangesAsync();

                if (SaveCreation > 0 ) { return new BaseResponseDto(true, 201, AppSettings.SuccessfullAttempt , new PersonDto { Id = create.Entity.Id , Name = Person.Name });}

                //get the new record and return as data
                return new BaseResponseDto(false,500, AppSettings.FailedAttempt);
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
                //Check If name exists
                var getname = await HNGxDBContext.Persons.Where(x => x.Name == name.Trim()).FirstOrDefaultAsync();
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
                //Check if userId exists 
                var GetUserId = await HNGxDBContext.Persons.Where(d => d.Id == userId).FirstOrDefaultAsync();
                if (GetUserId == null) { return new BaseResponseDto(false, 404, "UserId Not  Found"); }

                //Delete the UserId
                var deleteUserId = HNGxDBContext.Persons.Remove(GetUserId);
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
                //Get the Person by  userId
                var GetUserId = await HNGxDBContext.Persons.Where(d => d.Id == userId).FirstOrDefaultAsync();
                if(GetUserId == null) { return new BaseResponseDto(false, 404,"Not Found");}

                return new BaseResponseDto(true, 200, AppSettings.SuccessfullAttempt , new PersonDto { Id = GetUserId.Id , Name = GetUserId.Name}); 
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return new BaseResponseDto(false, 500, ex.Message); }
                return new BaseResponseDto(false, 500, AppSettings.FailedAttempt);
            }
        }

        public async Task<BaseResponseDto> UpdatePerson(int user_id, PersonNameRequestDto model)
        {
            try
            {
                // get the person record 
                var GetUserId = await HNGxDBContext.Persons.Where(d => d.Id == user_id).FirstOrDefaultAsync();
                if (GetUserId != null)
                {
                    //check if name already exist
                    var nameExist = await NameExists(model.Name.Trim());
                    if (!nameExist.Status) { return new BaseResponseDto(false, 400, "Name already exist"); }

                    //Update
                    GetUserId.Name = model.Name.Trim();
                    var SaveActivity = await HNGxDBContext.SaveChangesAsync();

                    if (SaveActivity > 0) { return new BaseResponseDto(true, 200, AppSettings.SuccessfullAttempt , new PersonDto { Id = GetUserId.Id , Name = GetUserId.Name} ); }
                }

                return new BaseResponseDto(false, 404, "UserId Not Found");
            }
            catch (Exception ex)
            {
                if (AppSettings.Environment == "DEV") { return new BaseResponseDto(false, 500, ex.Message); }
                return new BaseResponseDto(false, 500, AppSettings.FailedAttempt);
            }
        }    
    }
}
