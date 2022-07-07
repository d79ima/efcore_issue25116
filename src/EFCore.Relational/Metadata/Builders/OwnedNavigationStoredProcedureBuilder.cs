// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
///     Provides a simple API for configuring a <see cref="IMutableStoredProcedure" /> that an entity type is mapped to.
/// </summary>
public class OwnedNavigationStoredProcedureBuilder :
    IInfrastructure<OwnedNavigationBuilder>, IInfrastructure<IConventionStoredProcedureBuilder>
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [EntityFrameworkInternal]
    public OwnedNavigationStoredProcedureBuilder(
        IMutableStoredProcedure sproc,
        OwnedNavigationBuilder ownedNavigationBuilder)
    {
        Builder = ((StoredProcedure)sproc).Builder;
        OwnedNavigationBuilder = ownedNavigationBuilder;
    }

    private OwnedNavigationBuilder OwnedNavigationBuilder { get; }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [EntityFrameworkInternal]
    protected virtual InternalStoredProcedureBuilder Builder { [DebuggerStepThrough] get; }

    /// <inheritdoc />
    IConventionStoredProcedureBuilder IInfrastructure<IConventionStoredProcedureBuilder>.Instance
    {
        [DebuggerStepThrough]
        get => Builder;
    }

    /// <summary>
    ///     The stored procedure being configured.
    /// </summary>
    public virtual IMutableStoredProcedure Metadata
        => Builder.Metadata;

    /// <summary>
    ///     Configures a new parameter if no parameter mapped to the given property exists.
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public virtual OwnedNavigationStoredProcedureBuilder HasParameter(string propertyName)
    {
        Builder.HasParameter(propertyName, ConfigurationSource.Explicit);
        return this;
    }

    /// <summary>
    ///     Configures a new parameter if no parameter mapped to the given property exists.
    /// </summary>
    /// <param name="propertyName">The parameter name.</param>
    /// <param name="buildAction">An action that performs configuration of the parameter.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public virtual OwnedNavigationStoredProcedureBuilder HasParameter(string propertyName, Action<StoredProcedureParameterBuilder> buildAction)
    {
        Builder.HasParameter(propertyName, ConfigurationSource.Explicit);
        buildAction(new(((StoredProcedure)Metadata).CreateIdentifier(), CreatePropertyBuilder(propertyName)));
        return this;
    }

    private PropertyBuilder CreatePropertyBuilder(string propertyName)
    {
        var entityType = OwnedNavigationBuilder.OwnedEntityType;

        var property = entityType.FindProperty(propertyName);
        if (property == null)
        {
            property = entityType.GetDerivedTypes().SelectMany(et => et.GetDeclaredProperties())
                .FirstOrDefault(p => p.Name == propertyName);
        }
        
        if (property == null)
        {
            throw new InvalidOperationException("Property not found: " + propertyName);
        }

#pragma warning disable EF1001 // Internal EF Core API usage.
        return new ModelBuilder(entityType.Model)
#pragma warning restore EF1001 // Internal EF Core API usage.
            .Entity(entityType.Name)
            .Property(property.ClrType, propertyName);
    }

    /// <summary>
    ///     Configures a new column of the result for this stored procedure. This is used for database generated columns.
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public virtual OwnedNavigationStoredProcedureBuilder HasResultColumn(string propertyName)
    {
        Builder.HasParameter(propertyName, ConfigurationSource.Explicit);
        return this;
    }

    /// <summary>
    ///     Configures a new column of the result for this stored procedure. This is used for database generated columns.
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <param name="buildAction">An action that performs configuration of the column.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public virtual OwnedNavigationStoredProcedureBuilder HasResultColumn(
        string propertyName, Action<StoredProcedureResultColumnBuilder> buildAction)
    {
        Builder.HasParameter(propertyName, ConfigurationSource.Explicit);
        buildAction(new(((StoredProcedure)Metadata).CreateIdentifier(), CreatePropertyBuilder(propertyName)));
        return this;
    }

    OwnedNavigationBuilder IInfrastructure<OwnedNavigationBuilder>.Instance => OwnedNavigationBuilder;
}
