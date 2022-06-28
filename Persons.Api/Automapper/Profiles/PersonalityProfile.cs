using AutoMapper;
using MongoDB.Bson;
using Persons.Models.Persons;
using SurveyMe.PersonsApi.Models.Request.Personality;
using SurveyMe.PersonsApi.Models.Response.Personality;

namespace Person.Api.Automapper.Profiles;

public class PersonalityProfile : Profile
{
    public PersonalityProfile()
    {
        CreateMap<PersonalityCreateRequestModels, Personality>();
        
        CreateMap<PersonalityEditRequestModels, Personality>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.Parse(src.Id)));
        
        CreateMap<Personality, PersonalityResponseModel>();
    }
}