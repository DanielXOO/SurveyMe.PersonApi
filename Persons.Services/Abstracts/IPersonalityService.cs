using Persons.Models.Persons;

namespace Persons.Services.Abstracts;

public interface IPersonalityService
{
    Task<Personality> GetPersonalityAsync(Guid id);
    
    Task<Guid> AddPersonalityAsync(Personality personality);

    Task EditPersonalityAsync(Personality personality);

    Task DeletePersonality(string id);
}