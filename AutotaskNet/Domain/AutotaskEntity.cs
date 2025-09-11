using System.ComponentModel.DataAnnotations;
using AutotaskNet.Utilities;

namespace AutotaskNet.Domain;

public abstract class AutotaskEntity : IEntityValidation
{
    public long Id { get; set; }

    public abstract string Endpoint { get; }
    protected abstract bool CanCreate { get; }
    protected abstract bool CanUpdate { get; }
    protected abstract bool CanDelete { get; }
    protected abstract bool CanQuery { get; }
    protected abstract bool CanGetEntityInformation { get; }

    public virtual void AssertValidState()
    {
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(this, new ValidationContext(this), validationResults, true))
            throw new AutotaskNetCoreValidationException(
                $"Entity {GetType()} is invalid:\n{string.Join('\n', validationResults)}");
    }

    public virtual void AssertValidOperation(AutotaskOperation operation)
    {
        var operationPermissions = new Dictionary<AutotaskOperation, (bool CanPerform, string ErrorMessage)>
        {
            { AutotaskOperation.Create, (CanCreate, "is not able to be created") },
            { AutotaskOperation.Update, (CanUpdate, "is not able to be updated") },
            { AutotaskOperation.Delete, (CanDelete, "is not able to be deleted") },
            { AutotaskOperation.Query, (CanQuery, "is not able to be queried") },
            {
                AutotaskOperation.GetEntityInformation,
                (CanGetEntityInformation, "does not have entity information endpoints registered")
            }
        };

        if (operationPermissions.TryGetValue(operation, out var permission) && !permission.CanPerform)
            throw new AutotaskNetCoreValidationException($"Entity {GetType()} {permission.ErrorMessage}.");
    }

    public void AssertValidStateAndOperation(AutotaskOperation operation)
    {
        AssertValidState();
        AssertValidOperation(operation);
    }
}

public enum AutotaskOperation
{
    Create,
    Update,
    Delete,
    Query,
    GetEntityInformation
}