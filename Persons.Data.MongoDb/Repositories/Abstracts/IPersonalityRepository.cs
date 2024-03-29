﻿using Persons.Data.Core.Abstracts;
using Persons.Models.Persons;

namespace Persons.Data.Repositories.Abstracts;

public interface IPersonalityRepository : IRepository<Personality>
{
    Task<Personality> GetPersonalityById(Guid id, IEnumerable<string> properties);

    Task DeletePersonalityAsync(Guid id);
}