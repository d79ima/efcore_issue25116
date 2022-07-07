// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
///     Provides a simple API for configuring a <see cref="IMutableStoredProcedure" /> that an entity type is mapped to.
/// </summary>
/// <typeparam name="TOwnerEntity">The entity type owning the relationship.</typeparam>
/// <typeparam name="TDependentEntity">The dependent entity type of the relationship.</typeparam>
public class OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity> :
    OwnedNavigationNamingStoredProcedureBuilder, IInfrastructure<OwnedNavigationBuilder<TOwnerEntity, TDependentEntity>>
    where TOwnerEntity : class
    where TDependentEntity : class
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
        OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> ownedNavigationBuilder)
        : base(sproc, ownedNavigationBuilder)
    {
    }

    private OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> OwnedNavigationBuilder
        => (OwnedNavigationBuilder<TOwnerEntity, TDependentEntity>)((IInfrastructure<OwnedNavigationBuilder>)this)
            .GetInfrastructure();

    /// <summary>
    ///     Sets the name of the stored procedure.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="name">The name of the function in the database.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity> HasName(string name)
        => (OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity>)base.HasName(name);

    /// <summary>
    ///     Sets the schema of the stored procedure.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="schema">The schema of the function in the database.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity> HasSchema(string? schema)
        => (OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity>)base.HasSchema(schema);

    /// <summary>
    ///     Removes the currently configured parameters.
    /// </summary>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity> WithNewParameterOrder()
        => (OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity>)base.WithNewParameterOrder();

    /// <summary>
    ///     Removes the currently configured result columns.
    /// </summary>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity> WithNewResultColumnOrder()
        => (OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity>)base.WithNewResultColumnOrder();

    OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> IInfrastructure<OwnedNavigationBuilder<TOwnerEntity, TDependentEntity>>.Instance
        => OwnedNavigationBuilder;
}
