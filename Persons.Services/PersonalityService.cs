using Persons.Data.Repositories.Abstracts;
using Persons.Models.Persons;
using Persons.Models.SurveysOptions;
using Persons.Services.Abstracts;
using SurveyMe.Common.Exceptions;

namespace Persons.Services;

public sealed class PersonalityService : IPersonalityService
{
    private readonly IPersonalityRepository _repository;
    
    
    public PersonalityService(IPersonalityRepository repository)
    {
        _repository = repository;
    }

    
    public async Task<Personality> GetPersonalityAsync(Guid id, IReadOnlyCollection<string> options)
    {
        var personality = await _repository.GetPersonalityById(id, options);

        if (personality == null)
        {
            throw new BadRequestException("User do not exists");
        }
        
        return personality;
    }

    public async Task<Personality> AddPersonalityAsync(Personality personality)
    {
        personality.PersonalityId = Guid.NewGuid();
        
        await _repository.CreateAsync(personality);

        return personality;
    }

    public async Task EditPersonalityAsync(Personality personality)
    {
        await _repository.UpdateAsync(personality);
    }

    public async Task DeletePersonalityAsync(Guid id)
    {
        await _repository.DeletePersonalityAsync(id);
    }
}