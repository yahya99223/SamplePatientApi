using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using AutoMapper;
using DataAccess.Contracts;
using DataAccess.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Service.Contracts;
using Service.Implementation;
using Moq;
using Shared.Models.Settings;

namespace UnitTests.Service.Implementation.PatientServiceTests
{
    [TestFixture]
    public class WhenAttachImage
    {
        private PatientsService _patientService;
        private Mock<IStorageHandler> _storageHandlerMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IOptions<Settings>> _mockSettings;
        private PatientsDbContext _memoryDbContext;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PatientsDbContext>()
                .UseInMemoryDatabase(databaseName: "PatientsDatabase")
                .Options;
            _memoryDbContext = new PatientsDbContext(options);
            _memoryDbContext.Patients.Add(new PatientRecord()
            {
                Id = Guid.NewGuid()
            });
            _memoryDbContext.SaveChanges();
            _storageHandlerMock = new Mock<IStorageHandler>();
            _mockSettings = new Mock<IOptions<Settings>>();
            _mapperMock= new Mock<IMapper>();
            _mockSettings.Setup(x => x.Value).Returns(new Settings()
            {
                AppSettings = new AppSettings()
            });
            _storageHandlerMock.Setup(x => x.StoreFile(It.IsAny<Stream>())).Returns("path");
            _patientService = new PatientsService(_storageHandlerMock.Object, _mapperMock.Object, _mockSettings.Object, _memoryDbContext);
        }
        [Test]
        public void Then_Result_Is_True_And_StorageHandler_Is_Invoked_If_PatientId_Is_Correct()
        {
            var result = _patientService.AttachImage(_memoryDbContext.Patients.First().Id, null);
            Assert.IsTrue(result);
            _storageHandlerMock.Verify(x => x.StoreFile(It.IsAny<Stream>()), Times.Once);
        }
        [Test]
        public void Then_Result_Is_False_And_StorageHandler_Is_Not_Invoked_If_PatientId_Is_Wrong()
        {
            var result = _patientService.AttachImage(Guid.NewGuid(), null);
            Assert.IsFalse(result);
            _storageHandlerMock.Verify(x => x.StoreFile(It.IsAny<Stream>()), Times.Never);
        }
    }
}
