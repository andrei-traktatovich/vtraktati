using System;
using System.CodeDom;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VTraktate.Core.Infrastructure;
using VTraktate.Core.Interfaces;
using VTraktate.Core.Repository.Interfaces;
using VTraktate.Domain;
using Nito.AsyncEx.UnitTests;
using VTraktate.BL.Providers;

namespace VTraktate.BL.Tests
{
     
    
    [TestClass]
    public class ProviderManagerShould
    {
        private ProviderManager sut;
        const int FAKEID = 42;
        private readonly Mock<ICerberos> mockCerberos = new Mock<ICerberos>();
        private readonly Mock<IRepo<Provider>> mockProviderRepo = new Mock<IRepo<Provider>>();

        [TestInitialize]
        public void Setup()
        {
            sut = new ProviderManager(mockCerberos.Object, mockProviderRepo.Object);
        }

      
        [TestMethod]
        public async Task WhenDeletingProviderFindsProviderById()
        {
            
            mockCerberos.Setup(x => x.CanDeleteProvidersAsync()).ReturnsAsync(true);
            mockProviderRepo.Setup(x => x.FindByIdAsync(It.Is<int>(id => id == FAKEID))).ReturnsAsync(new Provider() {Id = FAKEID});
            
            var result = await sut.FindAndDeleteAsync(FAKEID);

            mockProviderRepo.Verify(x => x.FindByIdAsync(FAKEID));
            Assert.AreEqual(FAKEID, result.Data.Id);
        }

        [TestMethod]
        public async Task WhenDeletingProviderReturnsOkAndProviderDeleted()
        {
            mockCerberos.Setup(x => x.CanDeleteProvidersAsync()).ReturnsAsync(true);
            mockProviderRepo.Setup(x => x.FindByIdAsync(It.Is<int>(id => id == FAKEID))).ReturnsAsync(new Provider() { Id = FAKEID });
            var result = await sut.FindAndDeleteAsync(42);
            Assert.IsTrue(result.Success);
        }
        
        [TestMethod]
        public async Task WhenDeletingProviderSavesChanges()
        {
            mockCerberos.Setup(x => x.CanDeleteProvidersAsync()).ReturnsAsync(true);
            mockProviderRepo.Setup(x => x.FindByIdAsync(It.Is<int>(id => id == FAKEID))).ReturnsAsync(new Provider() { Id = FAKEID });
            mockProviderRepo.Setup(x => x.SaveAsUserAsync(It.IsAny<int>())).ReturnsAsync(1);
            var result = await sut.FindAndDeleteAsync(42);
            mockProviderRepo.Verify(x => x.SaveAsUserAsync(It.IsAny<int>()));
        }

        [TestMethod]
        public async Task WhenDeletingProvider_IfNoProviderIsFoundReturnsEntity_NotFound_Error()
        {
            const int FAKEID = 42;
            mockCerberos.Setup(x => x.CanDeleteProvidersAsync()).ReturnsAsync(true);
            mockProviderRepo.Setup(x => x.FindByIdAsync(It.Is<int>(id => id == FAKEID))).ReturnsAsync(null);

            var result = await sut.FindAndDeleteAsync(FAKEID);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(OperationResult.ENTITY_NOT_FOUND, result.StatusCode);
        }

        [TestMethod]
        public async Task WhenDeletingProviderReturnsUnauthorizedErrorIfUserHasNoRightTo()
        {
            mockCerberos.Setup(x => x.CanDeleteProvidersAsync()).ReturnsAsync(false);
            var result = await sut.FindAndDeleteAsync(42);
            Assert.IsFalse(result.Success);
            Assert.AreEqual(OperationResult.UNAUTHORIZED, result.StatusCode);
        }
    }
}
