using Application.Common.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace Application.UnitTests.Common.Helpers;

public class PropertyNameResolverTests
{
    [Test]
    public void CamelCasePropertyNameResolverReturnsCamelCaseName()
    {
        var info = typeof(TestClass).GetMember(nameof(TestClass.FirstName)).FirstOrDefault();

        var propertyName = PropertyNameResolver.CamelCasePropertyNameResolver(typeof(TestClass), info, null);

        propertyName.Should().Be("firstName");
    }

    [Test]
    public void DefaultPropertyNameResolverReturnsDefaultNameString()
    {
        var info = typeof(TestClass).GetMember(nameof(TestClass.FirstName)).FirstOrDefault();

        var propertyName = PropertyNameResolver.DefaultPropertyNameResolver(typeof(TestClass), info, null);

        propertyName.Should().Be("FirstName");
    }

    private class TestClass
    {
        public string FirstName { get; set; }
    }
}
