namespace AutotaskNet.Domain;

internal interface IValidation
{
    /// <summary>
    /// Validates the object.
    /// </summary>
    /// <exception cref="T:AutotaskNet.Utilities.AutotaskNetCoreValidationException">
    /// The object is not valid.
    /// </exception>
    void AssertValidState();
}

internal interface IEntityValidation : IValidation
{
    /// <summary>
    /// Validates the operation to be performed on the entity.
    /// </summary>
    /// <exception cref="T:AutotaskNet.Utilities.AutotaskNetCoreValidationException">
    /// The operation is not valid for this entity.
    /// </exception>
    void AssertValidOperation(AutotaskOperation operation);

    /// <summary>
    /// Validates entity's state and the operation to be performed on the entity.
    /// </summary>
    /// <exception cref="T:AutotaskNet.Utilities.AutotaskNetCoreValidationException">
    /// The entity is not in a valid state or the operation is not valid for this entity.
    /// </exception>
    void AssertValidStateAndOperation(AutotaskOperation operation);
}