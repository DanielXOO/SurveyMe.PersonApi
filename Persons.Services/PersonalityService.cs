using MongoDB.Bson;
using Persons.Data.Repositories.Abstracts;
using Persons.Models.Persons;
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

    
    public async Task<Personality> GetPersonalityAsync(string id)
    {
        var objectId = ObjectId.Parse(id);
        var personality = await _repository.GetByIdAsync(objectId);

        if (personality == null)
        {
            throw new BadRequestException("User do not exists");
        }
        
        return personality;
    }

    public async Task AddPersonalityAsync(Personality personality)
    {
        await _repository.CreateAsync(personality);
    }

    public async Task EditPersonalityAsync(Personality personality)
    {
        await _repository.UpdateAsync(personality);
    }

    public async Task DeletePersonality(string id)
    {
        var objectId = ObjectId.Parse(id);
        
        await _repository.DeleteAsync(objectId);
    }

    public async Task<Personality> GetPersonalityByUserIdAsync(string userId)
    {
        var id = Guid.Parse(userId);
        var personality = await _repository.GetPersonalityByUserId(id);

        return personality;
    }
}