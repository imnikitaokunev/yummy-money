using System;
using System.Text;
using CostAccounting.Shared.Helpers;
using NUnit.Framework;

namespace CostAccounting.Tests.Shared.Helpers
{
    [TestFixture]
    public class PasswordHelperShould
    {
        [Test]
        public void GenerateSaltThrowsArgumentExceptionIfPassedLengthIsLessOrEqualToZero()
        {
            Assert.Throws<ArgumentException>(() => PasswordHelper.GenerateSalt(0));
        }

        [Test]
        public void GenerateReturnsRandomStringPassedLength()
        {
            var length = 32;

            var randomString = PasswordHelper.Generate(length);
            var emptyGuidString = Convert.ToBase64String(Guid.Empty.ToByteArray()).Trim();

            Assert.AreNotEqual(emptyGuidString, randomString);
        }

        [Test]
        public void ComputeHashThrowsArgumentNullExceptionIfPassedPasswordIsNullOrEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => PasswordHelper.ComputeHash(null, "test"));
        }

        [Test]
        public void ComputeHashThrowsArgumentNullExceptionIfPassedSaltIsNullOrEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => PasswordHelper.ComputeHash("test", null));
        }

        [Test]
        public void ComputeHashForPassedStringAndSalt()
        {
            var salt = "test";
            var qwerty123Hash = "왒瘮鯃ଆᐹ둽𢡄嘹彰�曜兀礆얝裋姍琷섍뫗襌ꀸ㚚뮵퓶棝髰㪿䂭莫뿋";

            var hash = PasswordHelper.ComputeHash("qwerty123", salt);

            Assert.AreEqual(qwerty123Hash, hash);
        }

        [Test]
        public void GenerateThrowsArgumentExceptionIfPassedLengthIsLessOrEqualToZero()
        {
            Assert.Throws<ArgumentException>(() => PasswordHelper.Generate(0));
        }

        [Test]
        public void GenerateSaltReturnsSaltPassedLength()
        {
            var length = 16;

            var emptySaltBytes = new byte[length];
            var emptySalt = Encoding.Default.GetString(emptySaltBytes);

            var notEmptySalt = PasswordHelper.GenerateSalt(length);

            Assert.AreNotEqual(emptySalt, notEmptySalt);
        }
    }
}