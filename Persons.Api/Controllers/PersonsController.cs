using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persons.Models.Persons;
using Persons.Models.SurveysOptions;
using Persons.Services.Abstracts;
using SurveyMe.Common.Exceptions;
using SurveyMe.Error.Models.Response;
using SurveyMe.PersonsApi.Models.Request.Personality;
using SurveyMe.PersonsApi.Models.Response.Personality;

namespace Person.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class PersonsController : Controller
{
    private readonly IPersonalityService _personalityService;

    private readonly IMapper _mapper;
    
    
    public PersonsController(IPersonalityService personalityService, IMapper mapper)
    {
        _personalityService = personalityService;
        _mapper = mapper;
    }

    
    /// <summary>
    /// Endpoint for adding new personality
    /// </summary>
    /// <param name="personalityRequest">Personality add model</param>
    /// <returns>Created personality model</returns>
    /// <exception cref="BadRequestException">Throws if edit model is invalid</exception>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PersonalityResponseModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestErrorResponse))]
    [HttpPost]
    public async Task<IActionResult> AddPersonality(PersonalityCreateRequestModel personalityRequest)
    {
        if (personalityRequest == null)
        {
            throw new BadRequestException("Empty model");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.ToDictionary(
                error => error.Key,
                error => error.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            
            throw new BadRequestException("Invalid data", errors);
        }
        
        var personality = _mapper.Map<Personality>(personalityRequest);

        personality = await _personalityService.AddPersonalityAsync(personality);

        var personalityResponse = _mapper.Map<PersonalityResponseModel>(personality);
        
        return CreatedAtAction(Url.Action(nameof(GetPersonality)), personalityResponse);
    }

    /// <summary>
    /// Endpoint for getting personality by id with options
    /// </summary>
    /// <param name="id">Personality id</param>
    /// <param name="surveyOptionsRequest">Options for getting required personality</param>
    /// <returns>Personality model</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonalityResponseModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPersonality(Guid id, 
        [FromQuery] PersonalityGetRequestModel surveyOptionsRequest)
    {
        var options = _mapper.Map<SurveyOptions>(surveyOptionsRequest);
        
        var personality = await _personalityService.GetPersonalityAsync(id, options);
        var personalityResponse = _mapper.Map<PersonalityResponseModel>(personality);

        return Ok(personalityResponse);
    }

    /// <summary>
    /// Endpoint for edit personality
    /// </summary>
    /// <param name="personalityEditRequestModels">Personality model</param>
    /// <param name="id">Personality id</param>
    /// <exception cref="BadRequestException">Throws if edit model is invalid</exception>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> EditPersonality(PersonalityEditRequestModel personalityEditRequestModels,
        Guid id)
    {
        if (personalityEditRequestModels == null)
        {
            throw new BadRequestException("Empty model");
        }
        
        if (!ModelState.IsValid)
        {
            var errors = ModelState.ToDictionary(
                error => error.Key,
                error => error.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            
            throw new BadRequestException("Invalid data", errors);
        }

        if (personalityEditRequestModels.PersonalityId == id)
        {
            throw new BadRequestException("Model id and request id do not match");
        }
        
        var personality = _mapper.Map<Personality>(personalityEditRequestModels);
        await _personalityService.EditPersonalityAsync(personality);

        return NoContent();
    }

    /// <summary>
    /// Endpoint for deleting user personality
    /// </summary>
    /// <param name="id">Personality id</param>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersonality(Guid id)
    {
        await _personalityService.DeletePersonalityAsync(id);
        
        return NoContent();
    }
}