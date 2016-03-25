using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nito.AsyncEx.UnitTests;
using VTraktate.BL.Cerberos;
using VTraktate.Core.Interfaces;
using VTraktate.Domain;

namespace VTraktate.BL.Tests
{
    [TestClass]
    public class CerberosShould
    {
        const int FAKEUSERID = 42;
        private ICerberos sut;
        private Mock<ITraktatContext> mockContext = new Mock<ITraktatContext>();

        [TestInitialize]
        public void Setup()
        {
            sut = new CerberosMum().MakeCerberos(mockContext.Object, () => FAKEUSERID);
        }

        [TestMethod]
        public async Task Allow_Deleting_Providers_If_User_Is_In_Role_That_Allows_That()
        {
            var roles = AspNetRole.RolesToDeleteProvider.Select(x => new AspNetRole() {Id = x});
            
            var user = new AspNetUser();
            user.AspNetRoles.Add(roles.First());

            mockContext.Setup(x => x.GetByIdAsync<AspNetUser>(FAKEUSERID)).ReturnsAsync(user);
            
            var result = await sut.CanDeleteProvidersAsync();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DisAllow_Deleting_Providers_If_User_Is_Not_In_Role_That_Allows_That()
        {
            var user = new AspNetUser();
            user.AspNetRoles.Add(new AspNetRole() { Id = 666, Name = "Fake role" });

            mockContext.Setup(x => x.GetByIdAsync<AspNetUser>(FAKEUSERID)).ReturnsAsync(user);

            var result = await sut.CanDeleteProvidersAsync();

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task ThrowIfUserIsNotFound()
        {
            var user = new AspNetUser();
            user.AspNetRoles.Add(new AspNetRole() { Id = 666, Name = "Fake role" });

            mockContext.Setup(x => x.GetByIdAsync<AspNetUser>(FAKEUSERID)).ReturnsAsync(null);

            await sut.CanDeleteProvidersAsync();
            
        }
    }
}
