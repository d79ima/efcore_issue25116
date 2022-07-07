// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
///     Provides a simple API for configuring a <see cref="IMutableStoredProcedure" /> that an entity type is mapped to.
/// </summary>
/// <typeparam name="TEntity">The entity type being configured.</typeparam>
public class StoredProcedureBuilder<TEntity> : StoredProcedureBuilder, IInfrastructure<EntityTypeBuilder<TEntity>>
    where TEntity : class
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [EntityFrameworkInternal]
    public StoredProcedureBuilder(IMutableStoredProcedure sproc, EntityTypeBuilder<TEntity> entityTypeBuilder)
        : base(sproc, entityTypeBuilder)
    {
    }

    private EntityTypeBuilder<TEntity> EntityTypeBuilder
        => (EntityTypeBuilder<TEntity>)((IInfrastructure<EntityTypeBuilder>)this).Instance;

    /// <summary>
    ///     Configures a new parameter if no parameter mapped to the given property exists.
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual StoredProcedureBuilder<TEntity> HasParameter(string propertyName)
        => (StoredProcedureBuilder<TEntity>)base.HasParameter(propertyName);

    /// <summary>
    ///     Configures a new parameter if no parameter mapped to the given property exists.
    /// </summary>
    /// <param name="propertyName">The parameter name.</param>
    /// <param name="buildAction">An action that performs configuration of the parameter.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual StoredProcedureBuilder<TEntity> HasParameter(
        string propertyName, Action<StoredProcedureParameterBuilder> buildAction)
        => (StoredProcedureBuilder<TEntity>)base.HasParameter(propertyName, buildAction);

    /// <summary>
    ///     Configures a new column of the result for this stored procedure. This is used for database generated columns.
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual StoredProcedureBuilder<TEntity> HasResultColumn(string propertyName)
        => (StoredProcedureBuilder<TEntity>)base.HasResultColumn(propertyName);

    /// <summary>
    ///     Configures a new column of the result for this stored procedure. This is used for database generated columns.
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <param name="buildAction">An action that performs configuration of the column.</param>
    /// <returns>The same builder instance so that multiple configuration calls can be chained.</returns>
    public new virtual StoredProcedureBuilder<TEntity> HasResultColumn(
        string propertyName, Action<StoredProcedureResultColumnBuilder> buildAction)
        => (StoredProcedureBuilder<TEntity>)base.HasResultColumn(propertyName, buildAction);

    EntityTypeBuilder<TEntity> IInfrastructure<EntityTypeBuilder<TEntity>>.Instance => EntityTypeBuilder;
}
