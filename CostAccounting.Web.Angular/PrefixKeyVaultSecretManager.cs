using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace CostAccounting.Web.Angular
{
    public class PrefixKeyVaultSecretManager : IKeyVaultSecretManager
    {
        public bool Load(SecretItem secret)
        {
            return true;
        }

        public string GetKey(SecretBundle secret)
        {
            return secret.SecretIdentifier.Name.Replace("-", ConfigurationPath.KeyDelimiter);
        }
    }
}
