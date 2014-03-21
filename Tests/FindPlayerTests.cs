using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PhoneGameService.Models;
using PhoneGameService.Repositories;
using PhoneGameService.Services;

namespace PhoneGameService.Tests
{
    [TestFixture]
    public class FindPlayerTests
    {
        [TestFixtureSetUp]
        public void SetupTests()
        {
        }

        [TestFixtureTearDown]
        public void TeardownTests()
        {
        }

        [Test]
        public void Player_FindByString()
        {
            using (var repository = new TelephoneGameRepository())
            {
                Player p = GameService.FindPlayer("15022967010", repository);
                Assert.That(null != p, "Player not found");
            }
        }

        [Test]
        public void Player_FindByPhoneNumber()
        {
            using (var repository = new TelephoneGameRepository())
            {
                PhoneNumber phone = new PhoneNumber() { Number = "15022967010" };
                Player p = GameService.FindPlayer(phone, repository);
                Assert.That(null != p, "Player not found");
            }
        }
    }
}
