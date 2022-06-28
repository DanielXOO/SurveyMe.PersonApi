using MongoDB.Driver;
using Persons.Data.Core;
using Persons.Data.Repositories.Abstracts;
using Persons.Models.Persons;

namespace Persons.Data.Repositories;

public sealed class PersonalityRepository : Repository<Personality>, IPersonalityRepository
{
    public PersonalityRepository(PersonsDbContext dbContext) : base(dbContext) { }

    public async Task<Personality> GetPersonalityByUserId(Guid userId)
    {
        var document = await Collection
            .FindAsync(personality => personality.UserId == userId);
        var personality = await document.FirstOrDefaultAsync();

        return personality;
    }
}