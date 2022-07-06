using MongoDB.Bson;
using MongoDB.Driver;
using Persons.Data.Core;
using Persons.Data.Repositories.Abstracts;
using Persons.Models.Persons;

namespace Persons.Data.Repositories;

public sealed class PersonalityRepository : Repository<Personality>, IPersonalityRepository
{
    public PersonalityRepository(PersonsDbContext dbContext) : base(dbContext) { }

    
    public async Task<Personality> GetPersonalityById(Guid id, IEnumerable<string> properties)
    {

        var projection = Builders<Personality>.Projection;
        
        foreach (var property in properties)
        {
            projection.Include(property);
        }
        
        var cursor = Collection
            .Find(obj => obj.PersonalityId == id).Project<Personality>(projection.ToBsonDocument());
        var personality = await cursor.SingleOrDefaultAsync();

        return personality;
    }
}