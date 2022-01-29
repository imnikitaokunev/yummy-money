using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace Application.Common.Security;

public class PrefixKeyVaultSecretManager : IKeyVaultSecretManager
{
    public bool Load(SecretItem secret) => true;

    public string GetKey(SecretBundle secret) =>
        secret.SecretIdentifier.Name.Replace("-", ConfigurationPath.KeyDelimiter);
}
