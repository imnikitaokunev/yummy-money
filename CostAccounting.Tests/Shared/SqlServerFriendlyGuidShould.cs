using System;
using CostAccounting.Shared;
using NUnit.Framework;

namespace CostAccounting.Tests.Shared
{
    [Ignore("Need to be updated")]
    [TestFixture]
    public class SqlServerFriendlyGuidShould
    {
        [Test]
        public void ReturnNotEmptyGuid()
        {
            var emptyGuid = new Guid();
            var notEmptyGuid = SqlServerFriendlyGuid.Generate();

            Assert.AreNotEqual(emptyGuid, notEmptyGuid);
        }
    }
}
