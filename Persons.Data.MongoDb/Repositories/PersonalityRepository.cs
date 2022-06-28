using MongoDB.Driver;
using Persons.Data.Core;
using Persons.Data.Repositories.Abstracts;
using Persons.Models.Persons;

namespace Persons.Data.Repositories;

public sealed class PersonalityRepository : Repository<Personality>, IPersonalityRepository
{
    public PersonalityRepository(PersonsDbContext dbContext) : base(dbContext) { }

    
    public async Task<Personality> GetPersonalityById(Guid id)
    {
        var filter = await Collection.FindAsync(obj => obj.PersonalityId == id);
        var personality = await filter.SingleOrDefaultAsync();

        return personality;
    }
}