using SurveyMe.SurveyPersonApi.Models.Common;

namespace Persons.Models.PersonalitiesOptions;

public class PersonalityOption
{
    public string PropertyName { get; set; }

    public bool IsRequired { get; set; }

    public OptionType Type { get; set; }
}