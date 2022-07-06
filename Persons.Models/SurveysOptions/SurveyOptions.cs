using Persons.Models.PersonalitiesOptions;

namespace Persons.Models.SurveysOptions;

public class SurveyOptions
{
    public Guid SurveyId { get; set; }

    public IReadOnlyCollection<string> Options { get; set; }
}