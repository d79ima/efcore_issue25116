// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
///     Provides a simple API for configuring a <see cref="IMutableStoredProcedure" /> that an entity type is mapped to.
/// </summary>
public class OwnedNavigationNamingStoredProcedureBuilder : OwnedNavigationStoredProcedureBuilder
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [EntityFrameworkInternal]
    public OwnedNavigationNamingStoredProcedureBuilder(
        IMutableStoredProcedure sproc,
        OwnedNavigationBuilder ownedNavigationBuilder)
        : base(sproc, ownedNavigationBuilder)
    {
    }

    /// <summary>
    ///     Sets the name of the stored procedure.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="name">The name of the stored procedure in the database.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public virtual OwnedNavigationNamingStoredProcedureBuilder HasName(string name)
    {
        Check.NullButNotEmpty(name, nameof(name));

        Builder.HasName(name, ConfigurationSource.Explicit);

        return this;
    }

    /// <summary>
    ///     Sets the schema of the stored procedure.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="schema">The schema of the stored procedure in the database.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public virtual OwnedNavigationNamingStoredProcedureBuilder HasSchema(string? schema)
    {
        Builder.HasSchema(schema, ConfigurationSource.Explicit);

        return this;
    }

    /// <summary>
    ///     Removes the currently configured parameters.
    /// </summary>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public virtual OwnedNavigationNamingStoredProcedureBuilder WithNewParameterOrder()
    {
        Builder.WithNewParameterOrder(ConfigurationSource.Explicit);
        
        return this;
    }

    /// <summary>
    ///     Removes the currently configured result columns.
    /// </summary>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public virtual OwnedNavigationNamingStoredProcedureBuilder WithNewResultColumnOrder()
    {
        Builder.WithNewResultColumnOrder(ConfigurationSource.Explicit);

        return this;
    }
}
