using NUnit.Framework;
using PadService.Models;
using PadService.Models.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace PadService.UnitTests
{
    [TestFixture]
    public class UserDTOTests
    {
        private List<UserDTO> _fakeUserList;

        [SetUp]
        public void SetUp()
        {
            _fakeUserList = new List<UserDTO>
            {
                new UserDTO{ firstName = "Vadim", lastName = "Pupkin"},
                new UserDTO{ firstName = "Mamkin", lastName = "Ion", address = "Valea Morilor"},
                new UserDTO{ firstName = "Bun", lastName = "Alexandru cel", phoneNumber = "060606060"},
            };
        }

        [Test]
        public void XmlSerialization_IsNotEmpty()
        {
            var result = _fakeUserList.ToXml();

            Assert.IsNotEmpty(result);
        }

        [Test]
        public void UserDTO_FirstItem_FirstNameIsVadim()
        {
            var result = _fakeUserList;

            Assert.AreEqual(result.First().firstName, "Vadim");
        }

        [Test]
        public void UserDTO_SecondItem_LastNameIsIon()
        {
            var result = _fakeUserList;

            Assert.AreEqual(result.Skip(1).Take(1).First().lastName, "Ion");
        }

        [Test]
        public void UserDTO_ThirdItem_FullnameIsAlexandruCelBun()
        {
            var thirdUser = _fakeUserList.Skip(2).Take(1).First();
            var result = thirdUser.lastName + " " + thirdUser.firstName;

            Assert.AreEqual(result, "Alexandru cel Bun");
        }
    }
}
