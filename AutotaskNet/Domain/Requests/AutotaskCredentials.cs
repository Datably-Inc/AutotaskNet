using System.ComponentModel.DataAnnotations;
using AutotaskNet.Utilities;

namespace AutotaskNet.Domain.Requests;

public class AutotaskCredentials : IValidation
{
    [Required] public string ApiIntegrationCode { get; set; }
    [Required] public string UserName { get; set; }
    [Required] public string Secret { get; set; }

    public void AssertValidState()
    {
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true))
            throw new AutotaskNetCoreValidationException(
                $"{nameof(AutotaskCredentials)} is invalid.\n{string.Join('\n', validationResults)}");
    }
}