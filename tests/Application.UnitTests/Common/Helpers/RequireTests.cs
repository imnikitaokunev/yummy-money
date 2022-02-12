using Application.Common.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Application.UnitTests.Common.Helpers;

public class RequireTests
{
    [Test]
    public void NotNullThrowsAgrumentNullExceptionIfArgumentIsNull()
    {
        Action act = () => Require.NotNull((object)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void NotNullDontThrowArgumentNullExceptionIfArgumentIsNotNull()
    {
        Action act = () => Require.NotNull(new object());
        act.Should().NotThrow<ArgumentNullException>();
    }
}
