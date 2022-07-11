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
        CreateMap<PersonalityCreateRequestModel, Personality>();

        CreateMap<PersonalityEditRequestModel, Personality>();
        
        CreateMap<Personality, PersonalityResponseModel>();
    }
}