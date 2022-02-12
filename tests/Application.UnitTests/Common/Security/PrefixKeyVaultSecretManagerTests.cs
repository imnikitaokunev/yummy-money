using Application.Common.Security;
using FluentAssertions;
using Microsoft.Azure.KeyVault.Models;
using NUnit.Framework;

namespace Application.UnitTests.Common.Security;

public class PrefixKeyVaultSecretManagerTests
{
    private PrefixKeyVaultSecretManager _secretManager;

    [SetUp]
    public void Setup()
    {
        _secretManager = new PrefixKeyVaultSecretManager();
    }

    [Test]
    public void GetKeyReturnsStringColonDelimitered()
    {
        var secret = new SecretBundle("Application-ConnectionString-DefaultConnection", "https://keyvaultmock.com/secrets/Application-ConnectionString-DefaultConnection");

        var result = _secretManager.GetKey(secret);

        result.Should().BeEquivalentTo("Application:ConnectionString:DefaultConnection");
    }
}
