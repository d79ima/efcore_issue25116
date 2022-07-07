// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Metadata.Internal;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
public class StoredProcedure :
    ConventionAnnotatable, IStoredProcedure, IMutableStoredProcedure, IConventionStoredProcedure
{
    private readonly List<string> _parameters = new();
    private readonly HashSet<string> _parametersSet = new();
    private readonly List<string> _resultColumns = new();
    private readonly HashSet<string> _resultColumnsSet = new();
    private string? _schema;
    private string? _name;
    private InternalStoredProcedureBuilder? _builder;

    private ConfigurationSource _configurationSource;
    private ConfigurationSource? _schemaConfigurationSource;
    private ConfigurationSource? _nameConfigurationSource;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public StoredProcedure(
        IMutableEntityType entityType,
        ConfigurationSource configurationSource)
    {
        EntityType = entityType;
        _configurationSource = configurationSource;
        _builder = new(this, ((IConventionModel)entityType).Builder);
    }

    /// <inheritdoc />
    public virtual IMutableEntityType EntityType { get; set; }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual InternalStoredProcedureBuilder Builder
    {
        [DebuggerStepThrough]
        get => _builder ?? throw new InvalidOperationException(CoreStrings.ObjectRemovedFromModel);
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual bool IsInModel
        => _builder is not null;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual void SetRemovedFromModel()
        => _builder = null;

    /// <summary>
    ///     Indicates whether the function is read-only.
    /// </summary>
    public override bool IsReadOnly
        => ((Annotatable)EntityType).IsReadOnly;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static StoredProcedure? GetStoredProcedure(
        IReadOnlyEntityType entityType,
        StoreObjectType sprocType)
        => (StoredProcedure?)entityType[GetAnnotationName(sprocType)]
            ?? (entityType.BaseType != null
                ? GetStoredProcedure(entityType.GetRootType(), sprocType)
                : null);

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static StoredProcedure SetStoredProcedure(
        IMutableEntityType entityType,
        StoreObjectType sprocType)
    {
        var sproc = new StoredProcedure(entityType, ConfigurationSource.Explicit);
        entityType.SetAnnotation(GetAnnotationName(sprocType), sproc);

        return sproc;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static StoredProcedure? SetStoredProcedure(
        IConventionEntityType entityType,
        StoreObjectType sprocType,
        bool fromDataAnnotation)
        => (StoredProcedure?)entityType.SetAnnotation(
            GetAnnotationName(sprocType),
            new StoredProcedure((IMutableEntityType)entityType,
                fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention))?.Value;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static IMutableStoredProcedure? RemoveStoredProcedure(IMutableEntityType entityType, StoreObjectType sprocType)
        => (IMutableStoredProcedure?)entityType.RemoveAnnotation(GetAnnotationName(sprocType))?.Value;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static IConventionStoredProcedure? RemoveStoredProcedure(IConventionEntityType entityType, StoreObjectType sprocType)
        => (IConventionStoredProcedure?)entityType.RemoveAnnotation(GetAnnotationName(sprocType))?.Value;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static ConfigurationSource? GetStoredProcedureConfigurationSource(
        IConventionEntityType entityType, StoreObjectType sprocType)
        => entityType.FindAnnotation(GetAnnotationName(sprocType))
            ?.GetConfigurationSource();

    private static string GetAnnotationName(StoreObjectType sprocType)
        => sprocType switch
        {
            StoreObjectType.InsertStoredProcedure => RelationalAnnotationNames.InsertStoredProcedure,
            StoreObjectType.DeleteStoredProcedure => RelationalAnnotationNames.DeleteStoredProcedure,
            StoreObjectType.UpdateStoredProcedure => RelationalAnnotationNames.UpdateStoredProcedure,
            _ => throw new InvalidOperationException("Unsopported sproc type " + sprocType)
        };

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual StoreObjectIdentifier CreateIdentifier()
    {
        Check.DebugAssert(Name != null, "Name is null");

        if (EntityType.GetInsertStoredProcedure() == this)
        {
            return StoreObjectIdentifier.InsertStoredProcedure(Name, Schema);
        }

        if (EntityType.GetDeleteStoredProcedure() == this)
        {
            return StoreObjectIdentifier.DeleteStoredProcedure(Name, Schema);
        }

        Check.DebugAssert(EntityType.GetUpdateStoredProcedure() == this,
            "Unexpected stored procedure type");

        return StoreObjectIdentifier.UpdateStoredProcedure(Name, Schema);
    }

    /// <inheritdoc />
    [DebuggerStepThrough]
    public virtual ConfigurationSource GetConfigurationSource()
        => _configurationSource;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [DebuggerStepThrough]
    public virtual void UpdateConfigurationSource(ConfigurationSource configurationSource)
        => _configurationSource = configurationSource.Max(_configurationSource);

    /// <inheritdoc />
    public virtual string? Schema
    {
        get => _schema ?? EntityType.GetDefaultSchema();
        set => SetSchema(value, ConfigurationSource.Explicit);
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual string? SetSchema(string? schema, ConfigurationSource configurationSource)
    {
        EnsureMutable();

        _schema = schema;

        _schemaConfigurationSource = configurationSource.Max(_schemaConfigurationSource);

        return schema;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual ConfigurationSource? GetSchemaConfigurationSource()
        => _schemaConfigurationSource;

    /// <inheritdoc />
    public virtual string? Name
    {
        get => _name ?? GetDefaultName();
        set => SetName(value, ConfigurationSource.Explicit);
    }

    private string? GetDefaultName()
    {
        string? suffix;
        if (EntityType.GetInsertStoredProcedure() == this)
        {
            suffix = "_Insert";
        }
        else if (EntityType.GetDeleteStoredProcedure() == this)
        {
            suffix = "_Delete";
        }
        else if (EntityType.GetUpdateStoredProcedure() == this)
        {
            suffix = "_Update";
        }
        else
        {
            return null;
        }

        var tableName = EntityType.GetDefaultTableName();
        if (tableName == null)
        {
            return null;
        }

        return suffix + tableName;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public virtual string? SetName(string? name, ConfigurationSource configurationSource)
    {
        EnsureMutable();

        _name = name;

        _nameConfigurationSource = configurationSource.Max(_nameConfigurationSource);

        return name;
    }

    /// <inheritdoc />
    public virtual ConfigurationSource? GetNameConfigurationSource()
        => _nameConfigurationSource;

    /// <inheritdoc />
    public virtual IReadOnlyList<string> Parameters
    {
        [DebuggerStepThrough]
        get => _parameters;
    }

    /// <inheritdoc />
    public virtual bool ContainsParameter(string propertyName)
        => _parametersSet.Contains(propertyName);

    /// <inheritdoc />
    public virtual bool AddParameter(string propertyName)
    {
        if (!_parametersSet.Contains(propertyName))
        {
            _parameters.Add(propertyName);
            _parametersSet.Add(propertyName);

            return true;
        }

        return false;
    }

    /// <inheritdoc />
    public virtual void ResetParameters()
    {
        _parameters.Clear();
        _parametersSet.Clear();
    }

    /// <inheritdoc />
    public virtual IReadOnlyList<string> ResultColumns
    {
        [DebuggerStepThrough]
        get => _resultColumns;
    }

    /// <inheritdoc />
    public virtual bool ContainsResultColumn(string propertyName)
        => _resultColumnsSet.Contains(propertyName);

    /// <inheritdoc />
    public virtual bool AddResultColumn(string propertyName)
    {
        if (!_resultColumnsSet.Contains(propertyName))
        {
            _resultColumns.Add(propertyName);
            _resultColumnsSet.Add(propertyName);
            
            return true;
        }

        return false;
    }
    
    /// <inheritdoc />
    public virtual void ResetResultColumns()
    {
        _resultColumns.Clear();
        _resultColumnsSet.Clear();
    }

    ///// <summary>
    /////     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    /////     the same compatibility standards as public APIs. It may be changed or removed without notice in
    /////     any release. You should only use it directly in your code with extreme caution and knowing that
    /////     doing so can result in application failures when updating to a new Entity Framework Core release.
    ///// </summary>
    //[DisallowNull]
    //public virtual IStoreFunction? StoreFunction { get; set; }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public override string ToString()
        => ((IDbFunction)this).ToDebugString(MetadataDebugStringOptions.SingleLineDefault);

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    [EntityFrameworkInternal]
    public virtual DebugView DebugView
        => new(
            () => ((IDbFunction)this).ToDebugString(),
            () => ((IDbFunction)this).ToDebugString(MetadataDebugStringOptions.LongDefault));

    /// <inheritdoc />
    IConventionStoredProcedureBuilder IConventionStoredProcedure.Builder
    {
        [DebuggerStepThrough]
        get => Builder;
    }

    /// <inheritdoc />
    string IStoredProcedure.Name
    {
        [DebuggerStepThrough]
        get => Name!;
    }

    /// <inheritdoc />
    IReadOnlyEntityType IReadOnlyStoredProcedure.EntityType
    {
        get => (IConventionEntityType)EntityType;
    }

    /// <inheritdoc />
    IConventionEntityType IConventionStoredProcedure.EntityType
    {
        [DebuggerStepThrough]
        get => (IConventionEntityType)EntityType;
    }

    /// <inheritdoc />
    IEntityType IStoredProcedure.EntityType
    {
        [DebuggerStepThrough]
        get => (IEntityType)EntityType;
    }

    ///// <inheritdoc />
    //IStoreFunction IDbFunction.StoreFunction
    //    => StoreFunction!; // Relational model creation ensures StoreFunction is populated
    
    /// <inheritdoc />
    string? IConventionStoredProcedure.SetName(string? name, bool fromDataAnnotation)
        => SetName(name, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);
    
    /// <inheritdoc />
    string? IConventionStoredProcedure.SetSchema(string? schema, bool fromDataAnnotation)
        => SetSchema(schema, fromDataAnnotation ? ConfigurationSource.DataAnnotation : ConfigurationSource.Convention);

    /// <inheritdoc />
    string? IConventionStoredProcedure.AddParameter(string propertyName, bool fromDataAnnotation)
        => AddParameter(propertyName) ? propertyName : null;

    /// <inheritdoc />
    string? IConventionStoredProcedure.AddResultColumn(string propertyName, bool fromDataAnnotation)
        => AddResultColumn(propertyName) ? propertyName : null;
}
