﻿using MongoDB.Bson;
using MongoDB.Driver;
using Persons.Data.Core.Abstracts;
using Persons.Models.Common;

namespace Persons.Data.Core;

public class Repository<T> : IRepository<T> where T : BaseObject
{
    protected readonly IMongoCollection<T> Collection;


    public Repository(PersonsDbContext dbContext)
    {
        Collection = dbContext.GetCollection<T>(typeof(T).Name);
    }
    
    
    public async Task CreateAsync(T data)
    {
        await Collection.InsertOneAsync(data);
    }

    public async Task UpdateAsync(T data)
    {
        await Collection
            .ReplaceOneAsync(obj => obj.Id == data.Id, data);
    }

    public async Task<T> GetByIdAsync(ObjectId id)
    {
        
        var document = await Collection.FindAsync(obj => obj.Id == id);
        var data = await document.FirstOrDefaultAsync();

        return data;
    }

    public async Task<bool> IsRecordExistAsync(ObjectId id)
    {
        var isExist = await Collection
            .Find(personality => personality.Id == id)
            .AnyAsync();

        return isExist;
    }

    public async Task DeleteAsync(ObjectId id)
    {
        await Collection.DeleteOneAsync(odj => odj.Id == id);
    }
}