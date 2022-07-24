using Persons.Models.Persons;
using Persons.Models.SurveysOptions;

namespace Persons.Services.Abstracts;

public interface IPersonalityService
{
    Task<Personality> GetPersonalityAsync(Guid id, IReadOnlyCollection<string> options);
    
    Task<Personality> AddPersonalityAsync(Personality personality);

    Task EditPersonalityAsync(Personality personality);

    Task DeletePersonalityAsync(Guid id);
}