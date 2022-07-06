using Persons.Models.Persons;
using Persons.Models.SurveysOptions;

namespace Persons.Services.Abstracts;

public interface IPersonalityService
{
    Task<Personality> GetPersonalityAsync(Guid id, SurveyOptions requiredProperties);
    
    Task<Guid> AddPersonalityAsync(Personality personality);

    Task EditPersonalityAsync(Personality personality);

    Task DeletePersonality(string id);
}