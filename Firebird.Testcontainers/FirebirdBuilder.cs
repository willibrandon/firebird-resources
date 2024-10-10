namespace Firebird.Testcontainers;

/// <inheritdoc cref="ContainerBuilder{TBuilderEntity, TContainerEntity, TConfigurationEntity}" />
[PublicAPI]
public sealed class FirebirdBuilder : ContainerBuilder<FirebirdBuilder, FirebirdContainer, FirebirdConfiguration>
{
    /// <inheritdoc />
    protected override FirebirdConfiguration DockerResourceConfiguration { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdBuilder" /> class.
    /// </summary>
    public FirebirdBuilder()
        : this(new FirebirdConfiguration())
    {
        // 1) To change the ContainerBuilder default configuration override the DockerResourceConfiguration property and the "FirebirdBuilder Init()" method.
        //    Append the module configuration to base.Init() e.g. base.Init().WithImage("alpine:3.17") to set the modules' default image.

        // 2) To customize the ContainerBuilder validation override the "void Validate()" method.
        //    Use Testcontainers' Guard.Argument<TType>(TType, string) or your own guard implementation to validate the module configuration.

        // 3) Add custom builder methods to extend the ContainerBuilder capabilities such as "FirebirdBuilder WithFirebirdConfig(object)".
        //    Merge the current module configuration with a new instance of the immutable FirebirdConfiguration type to update the module configuration.

        // DockerResourceConfiguration = Init().DockerResourceConfiguration;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FirebirdBuilder" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    private FirebirdBuilder(FirebirdConfiguration resourceConfiguration)
        : base(resourceConfiguration)
    {
        DockerResourceConfiguration = resourceConfiguration;
    }

    /// <summary>
    /// Sets the Firebird config.
    /// </summary>
    /// <param name="config">The Firebird config.</param>
    /// <returns>A configured instance of <see cref="FirebirdBuilder" />.</returns>
    public FirebirdBuilder WithFirebirdConfig(object config)
    {
        // Extends the ContainerBuilder capabilities and holds a custom configuration in FirebirdConfiguration.
        // In case of a module requires other properties to represent itself, extend ContainerConfiguration.
        return Merge(DockerResourceConfiguration, new FirebirdConfiguration(config: config));
    }

    /// <inheritdoc />
    public override FirebirdContainer Build()
    {
        Validate();
        return new FirebirdContainer(DockerResourceConfiguration);
    }

    // /// <inheritdoc />
    // protected override FirebirdBuilder Init()
    // {
    //     return base.Init();
    // }

    // /// <inheritdoc />
    // protected override void Validate()
    // {
    //     base.Validate();
    // }

    /// <inheritdoc />
    protected override FirebirdBuilder Clone(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new FirebirdConfiguration(resourceConfiguration));
    }

    /// <inheritdoc />
    protected override FirebirdBuilder Clone(IContainerConfiguration resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new FirebirdConfiguration(resourceConfiguration));
    }

    /// <inheritdoc />
    protected override FirebirdBuilder Merge(FirebirdConfiguration oldValue, FirebirdConfiguration newValue)
    {
        return new FirebirdBuilder(new FirebirdConfiguration(oldValue, newValue));
    }
}