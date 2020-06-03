using CostAccounting.Shared.Cryptography;
using NUnit.Framework;

namespace CostAccounting.Tests.Shared.Cryptography
{
    [TestFixture]
    public class Sha512HashShould
    {
        [Test]
        public void CalculateHashForPassedString()
        {
            var algorithm = new Sha512Hash();

            var qwerty123Hash = "ʸ鸻㢎헤躞ᓔ百緪鶤斋�츳앸⋅ꮧ붉�ᳺ煙鰐㘯驽୼鳋原◉捀恞";
            var hash = algorithm.CalculateHash("qwerty123");

            Assert.AreEqual(qwerty123Hash, hash);
        }
    }
}
