namespace NetAdvancedShop.Tests.Common.Extensions;

public static class GenericCollectionAssertionsExtensions
{
    /// <summary>
    ///     Asserts that a collection of objects contains at least one object equivalent to <see cref="expectation" />
    ///     which has the SAME TYPE as <see cref="expectation" />.
    /// </summary>
    public static AndConstraint<GenericCollectionAssertions<T>> ContainSameTo<T>(
        this GenericCollectionAssertions<T> assertions,
        T expectation) =>
        assertions.ContainSameTo(expectation, options => options);

    /// <summary>
    ///     Asserts that a collection of objects contains at least one object equivalent to <see cref="expectation" />
    ///     which has the SAME TYPE as <see cref="expectation" />.
    /// </summary>
    public static AndConstraint<GenericCollectionAssertions<T>> ContainSameTo<T>(
        this GenericCollectionAssertions<T> assertions,
        T expectation,
        Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config)
    {
        return assertions.ContainEquivalentOf(
            expectation,
            options => config(options)
                .Using(new SameTypeEquivalencyStep())
                .RespectingRuntimeTypes());
    }

    /// <summary>
    ///     Asserts that a collection of objects does not contain at least one object equivalent to
    ///     <see cref="unexpected" /> with the SAME TYPE.
    /// </summary>
    public static AndConstraint<GenericCollectionAssertions<T>> NotContainSameTo<T>(
        this GenericCollectionAssertions<T> assertions,
        T unexpected) =>
        assertions.NotContainSameTo(unexpected, options => options);

    /// <summary>
    ///     Asserts that a collection of objects does not contain at least one object equivalent to
    ///     <see cref="unexpected" /> with the SAME TYPE.
    /// </summary>
    public static AndConstraint<GenericCollectionAssertions<T>> NotContainSameTo<T>(
        this GenericCollectionAssertions<T> assertions,
        T unexpected,
        Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> config)
    {
        bool condition;

        using (var assertionScope = new AssertionScope())
        {
            assertions.ContainSameTo(unexpected, config);
            condition = assertionScope.Discard().Length > 0;
        }

        Execute.Assertion.ForCondition(condition)
            .FailWith(
                "Expected {0:collection} not to contain same to {1}, but it does.",
                assertions.Subject,
                unexpected);

        return new AndConstraint<GenericCollectionAssertions<T>>(assertions);
    }
}

public class SameTypeEquivalencyStep : IEquivalencyStep
{
    public EquivalencyResult Handle(
        Comparands comparands,
        IEquivalencyValidationContext context,
        IEquivalencyValidator parent)
    {
        var runtimeType = comparands.RuntimeType;
        if (comparands.Subject == null // null values are processed in the other Equivalency Step
            // skip now as a collection will be iterated in the other Equivalency Step
            || typeof(IEnumerable).IsAssignableFrom(runtimeType)
            || runtimeType.IsPrimitive) // handle only complex types
        {
            return EquivalencyResult.ContinueWithNext;
        }

        var actualType = comparands.Subject.GetType();

        if (actualType == runtimeType) return EquivalencyResult.ContinueWithNext;

        Execute.Assertion
            .BecauseOf(context.Reason.FormattedMessage)
            .FailWith(
                "Expected {context:object} to be type of {0}{reason}, but found type of {1}. \n\nExpected type: {2}\nActual type:   {3}",
                runtimeType.Name,
                actualType.Name,
                runtimeType.FullName,
                actualType.FullName);

        return EquivalencyResult.AssertionCompleted;
    }
}