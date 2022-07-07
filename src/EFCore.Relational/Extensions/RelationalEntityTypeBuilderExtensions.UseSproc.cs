// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Microsoft.EntityFrameworkCore;

/// <summary>
///     Relational database specific extension methods for <see cref="EntityTypeBuilder" />.
/// </summary>
/// <remarks>
///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and examples.
/// </remarks>
public static partial class RelationalEntityTypeBuilderExtensions
{
    /// <summary>
    ///     Configures the stored procedure that the entity type would use for updates when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static EntityTypeBuilder UpdateUsingStoredProcedure(
        this EntityTypeBuilder entityTypeBuilder,
        Action<NamingStoredProcedureBuilder> buildAction)
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            entityTypeBuilder.Metadata, StoreObjectType.UpdateStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, entityTypeBuilder));

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for updates when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <typeparam name="TEntity">The entity type being configured.</typeparam>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static EntityTypeBuilder<TEntity> UpdateUsingStoredProcedure<TEntity>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder,
        Action<NamingStoredProcedureBuilder<TEntity>> buildAction)
        where TEntity : class
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            entityTypeBuilder.Metadata, StoreObjectType.UpdateStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, entityTypeBuilder));

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for updates when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="ownedNavigationBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static OwnedNavigationBuilder UpdateUsingStoredProcedure(
        this OwnedNavigationBuilder ownedNavigationBuilder,
        Action<OwnedNavigationNamingStoredProcedureBuilder> buildAction)
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            ownedNavigationBuilder.OwnedEntityType, StoreObjectType.UpdateStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, ownedNavigationBuilder));

        return ownedNavigationBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for updates when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <typeparam name="TOwnerEntity">The entity type owning the relationship.</typeparam>
    /// <typeparam name="TDependentEntity">The dependent entity type of the relationship.</typeparam>
    /// <param name="ownedNavigationBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> UpdateUsingStoredProcedure<TOwnerEntity, TDependentEntity>(
        this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> ownedNavigationBuilder,
        Action<OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity>> buildAction)
        where TOwnerEntity : class
        where TDependentEntity : class
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            ownedNavigationBuilder.OwnedEntityType, StoreObjectType.UpdateStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, ownedNavigationBuilder));

        return ownedNavigationBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for updates when targeting a relational database.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The builder instance if the configuration was applied, <see langword="null" /> otherwise.
    /// </returns>
    public static IConventionStoredProcedureBuilder? UpdateUsingStoredProcedure(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        bool fromDataAnnotation = false)
        => InternalStoredProcedureBuilder.HasStoredProcedure(
            entityTypeBuilder.Metadata, StoreObjectType.UpdateStoredProcedure, fromDataAnnotation);

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for deletes when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static EntityTypeBuilder DeleteUsingStoredProcedure(
        this EntityTypeBuilder entityTypeBuilder,
        Action<NamingStoredProcedureBuilder> buildAction)
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            entityTypeBuilder.Metadata, StoreObjectType.DeleteStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, entityTypeBuilder));

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for deletes when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <typeparam name="TEntity">The entity type being configured.</typeparam>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static EntityTypeBuilder<TEntity> DeleteUsingStoredProcedure<TEntity>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder,
        Action<NamingStoredProcedureBuilder<TEntity>> buildAction)
        where TEntity : class
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            entityTypeBuilder.Metadata, StoreObjectType.DeleteStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, entityTypeBuilder));

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for deletes when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="ownedNavigationBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static OwnedNavigationBuilder DeleteUsingStoredProcedure(
        this OwnedNavigationBuilder ownedNavigationBuilder,
        Action<OwnedNavigationNamingStoredProcedureBuilder> buildAction)
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            ownedNavigationBuilder.OwnedEntityType, StoreObjectType.DeleteStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, ownedNavigationBuilder));

        return ownedNavigationBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for deletes when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <typeparam name="TOwnerEntity">The entity type owning the relationship.</typeparam>
    /// <typeparam name="TDependentEntity">The dependent entity type of the relationship.</typeparam>
    /// <param name="ownedNavigationBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> DeleteUsingStoredProcedure<TOwnerEntity, TDependentEntity>(
        this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> ownedNavigationBuilder,
        Action<OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity>> buildAction)
        where TOwnerEntity : class
        where TDependentEntity : class
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            ownedNavigationBuilder.OwnedEntityType, StoreObjectType.DeleteStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, ownedNavigationBuilder));

        return ownedNavigationBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for deletes when targeting a relational database.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The builder instance if the configuration was applied, <see langword="null" /> otherwise.
    /// </returns>
    public static IConventionStoredProcedureBuilder? DeleteUsingStoredProcedure(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        bool fromDataAnnotation = false)
        => InternalStoredProcedureBuilder.HasStoredProcedure(
            entityTypeBuilder.Metadata, StoreObjectType.DeleteStoredProcedure, fromDataAnnotation);
    
    /// <summary>
    ///     Configures the stored procedure that the entity type would use for inserts when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static EntityTypeBuilder InsertUsingStoredProcedure(
        this EntityTypeBuilder entityTypeBuilder,
        Action<NamingStoredProcedureBuilder> buildAction)
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            entityTypeBuilder.Metadata, StoreObjectType.InsertStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, entityTypeBuilder));

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for inserts when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <typeparam name="TEntity">The entity type being configured.</typeparam>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static EntityTypeBuilder<TEntity> InsertUsingStoredProcedure<TEntity>(
        this EntityTypeBuilder<TEntity> entityTypeBuilder,
        Action<NamingStoredProcedureBuilder<TEntity>> buildAction)
        where TEntity : class
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            entityTypeBuilder.Metadata, StoreObjectType.InsertStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, entityTypeBuilder));

        return entityTypeBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for inserts when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <param name="ownedNavigationBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static OwnedNavigationBuilder InsertUsingStoredProcedure(
        this OwnedNavigationBuilder ownedNavigationBuilder,
        Action<OwnedNavigationNamingStoredProcedureBuilder> buildAction)
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            ownedNavigationBuilder.OwnedEntityType, StoreObjectType.InsertStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, ownedNavigationBuilder));

        return ownedNavigationBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for inserts when targeting a relational database.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> and
    ///     <see href="https://aka.ms/efcore-docs-saving-data">Saving data with EF Core</see> for more information and examples.
    /// </remarks>
    /// <typeparam name="TOwnerEntity">The entity type owning the relationship.</typeparam>
    /// <typeparam name="TDependentEntity">The dependent entity type of the relationship.</typeparam>
    /// <param name="ownedNavigationBuilder">The builder for the entity type being configured.</param>
    /// <param name="buildAction">An action that performs configuration of the stored procedure.</param>
    /// <returns>The same builder instance so that multiple calls can be chained.</returns>
    public static OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> InsertUsingStoredProcedure<TOwnerEntity, TDependentEntity>(
        this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> ownedNavigationBuilder,
        Action<OwnedNavigationNamingStoredProcedureBuilder<TOwnerEntity, TDependentEntity>> buildAction)
        where TOwnerEntity : class
        where TDependentEntity : class
    {
        Check.NotNull(buildAction, nameof(buildAction));

        var sprocBuilder = InternalStoredProcedureBuilder.HasStoredProcedure(
            ownedNavigationBuilder.OwnedEntityType, StoreObjectType.InsertStoredProcedure);
        buildAction(new(sprocBuilder.Metadata, ownedNavigationBuilder));

        return ownedNavigationBuilder;
    }

    /// <summary>
    ///     Configures the stored procedure that the entity type would use for inserts when targeting a relational database.
    /// </summary>
    /// <param name="entityTypeBuilder">The builder for the entity type being configured.</param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The builder instance if the configuration was applied, <see langword="null" /> otherwise.
    /// </returns>
    public static IConventionStoredProcedureBuilder? InsertUsingStoredProcedure(
        this IConventionEntityTypeBuilder entityTypeBuilder,
        bool fromDataAnnotation = false)
        => InternalStoredProcedureBuilder.HasStoredProcedure(
            entityTypeBuilder.Metadata, StoreObjectType.InsertStoredProcedure, fromDataAnnotation);
}
