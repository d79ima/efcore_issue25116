// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
///     Provides a simple API for configuring a <see cref="IMutableStoredProcedure" /> that an entity type is mapped to.
/// </summary>
/// <typeparam name="TOwnerEntity">The entity type owning the relationship.</typeparam>
/// <typeparam name="TDependentEntity">The dependent entity type of the relationship.</typeparam>
public class OwnedNavigationStoredProcedureBuilder<TOwnerEntity, TDependentEntity> :
    OwnedNavigationStoredProcedureBuilder, IInfrastructure<OwnedNavigationBuilder<TOwnerEntity, TDependentEntity>>
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
    public OwnedNavigationStoredProcedureBuilder(
        IMutableStoredProcedure sproc,
        OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> ownedNavigationBuilder)
        : base(sproc, ownedNavigationBuilder)
    {
    }

    private OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> OwnedNavigationBuilder
        => (OwnedNavigationBuilder<TOwnerEntity, TDependentEntity>)((IInfrastructure<OwnedNavigationBuilder>)this)
            .GetInfrastructure();

    /// <summary>
    ///     Configures a new parameter if no parameter mapped to the given property exists.
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual OwnedNavigationStoredProcedureBuilder<TOwnerEntity, TDependentEntity> HasParameter(string propertyName)
        => (OwnedNavigationStoredProcedureBuilder<TOwnerEntity, TDependentEntity>)base.HasParameter(propertyName);

    /// <summary>
    ///     Configures a new parameter if no parameter mapped to the given property exists.
    /// </summary>
    /// <param name="propertyName">The parameter name.</param>
    /// <param name="buildAction">An action that performs configuration of the parameter.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual OwnedNavigationStoredProcedureBuilder<TOwnerEntity, TDependentEntity> HasParameter(
        string propertyName, Action<StoredProcedureParameterBuilder> buildAction)
        => (OwnedNavigationStoredProcedureBuilder<TOwnerEntity, TDependentEntity>)base.HasParameter(propertyName, buildAction);

    /// <summary>
    ///     Configures a new column of the result for this stored procedure. This is used for database generated columns.
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual OwnedNavigationStoredProcedureBuilder<TOwnerEntity, TDependentEntity> HasResultColumn(string propertyName)
        => (OwnedNavigationStoredProcedureBuilder<TOwnerEntity, TDependentEntity>)base.HasResultColumn(propertyName);

    /// <summary>
    ///     Configures a new column of the result for this stored procedure. This is used for database generated columns.
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <param name="buildAction">An action that performs configuration of the column.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual OwnedNavigationStoredProcedureBuilder<TOwnerEntity, TDependentEntity> HasResultColumn(
        string propertyName, Action<StoredProcedureResultColumnBuilder> buildAction)
        => (OwnedNavigationStoredProcedureBuilder<TOwnerEntity, TDependentEntity>)base.HasResultColumn(propertyName, buildAction);

    OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> IInfrastructure<OwnedNavigationBuilder<TOwnerEntity, TDependentEntity>>.Instance
        => OwnedNavigationBuilder;
}
