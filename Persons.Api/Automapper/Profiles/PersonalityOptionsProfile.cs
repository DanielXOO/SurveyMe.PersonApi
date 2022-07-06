using AutoMapper;
using Persons.Models.SurveysOptions;
using SurveyMe.SurveyPersonApi.Models.Request.Options.Survey;

namespace Person.Api.Automapper.Profiles;

public class PersonalityOptionsProfile : Profile
{
    public PersonalityOptionsProfile()
    {
        CreateMap<SurveyOptionsGetRequestModel, SurveyOptions>();
    }
}