﻿using Persons.Data.Core.Abstracts;
using Persons.Models.Persons;

namespace Persons.Data.Repositories.Abstracts;

public interface IPersonalityRepository : IRepository<Personality>
{
    Task<Personality> GetPersonalityByUserId(Guid userId);
}