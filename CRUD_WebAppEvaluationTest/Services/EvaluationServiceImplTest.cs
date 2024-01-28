using AutoFixture;
using CRUD_WebAppEvaluation.DbApplicationContext;
using CRUD_WebAppEvaluation.MVC.Repository;
using CRUD_WebAppEvaluation.MVC.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Data;

namespace CRUD_WebAppEvaluationTest.Services
{
    [TestClass]
    public class EvaluationServiceImplTest
    {
        private Mock<EvaluationRepository> mockEvaluationRepository;
        private Mock<ILogger<EvaluationService>> mockLogger;
        private EvaluationServiceImpl mockEvaluationService;
        private Fixture fixture;
        private Evaluation evaluation;
        private List<Evaluation> evaluationList;

        [TestInitialize]
        public void Initialize()
        {
            mockEvaluationRepository = new Mock<EvaluationRepository>();
            mockLogger = new Mock<ILogger<EvaluationService>>();

            mockEvaluationService = new EvaluationServiceImpl(mockEvaluationRepository.Object, mockLogger.Object);

            // Random objects that can be used for the following test methods
            fixture = new Fixture(); // Create an instance of Fixture in the setup method or constructor
            evaluation = fixture.Create<Evaluation>();
            evaluationList = fixture.CreateMany<Evaluation>(5).ToList();
        }

        [TestMethod]
        public void FindEvaluationByIdWithException_ShouldReturnEvaluation()
        {
            // Arrange
            int id = 1; // random number
            mockEvaluationRepository.Setup(repo => repo.FindById(id)).Returns(evaluation);
            // Act
            var result = mockEvaluationService.FindEvaluationByIdWithException(id);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(evaluation, result);
        }

        [TestMethod]
        public void FindEvaluationById_ShouldReturnEvaluation()
        {
            // Arrange
            int id = 1; // random number
            mockEvaluationRepository.Setup(repo => repo.FindById(id)).Returns(evaluation);
            // Act
            var result = mockEvaluationService.FindEvaluationById(id);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(evaluation, result);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void FindEvaluationById_ShouldThrowKeyNotFoundException_WhenEvaluationNotFound()
        {
            // Arrange
            int id = 1; // random number
            mockEvaluationRepository.Setup(repo => repo.FindById(id)).Returns((Evaluation)null);
            // Act & Assert
            mockEvaluationService.FindEvaluationByIdWithException(id);
        }

        [TestMethod]
        public void FindAllEvaluation_ShouldReturnListOfEvaluations()
        {
            // Arrange
            mockEvaluationRepository.Setup(repo => repo.FindAll()).Returns(evaluationList);
            // Act
            var result = mockEvaluationService.FindAllEvaluations();
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(evaluationList, result);
        }

        [TestMethod]
        public void AddEvaluation_ShouldAddNewEvaluation()
        {
            // Arrange
            int id = 1; // random number
            mockEvaluationRepository.Setup(repo => repo.FindById(id)).Returns((Evaluation)null);

            // Act
            mockEvaluationService.AddEvaluation(evaluation);

            // Assert
            mockEvaluationRepository.Verify(repo => repo.Add(evaluation), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddEvaluation_ShouldThrowException_WhenIdNotGiven()
        {
            Evaluation existingEvaluation = new Evaluation { };
            // Act & Assert
            mockEvaluationService.AddEvaluation(existingEvaluation);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateNameException))]
        public void AddEvaluation_ShouldThrowDuplicateNameException_WhenEvaluationWithSameIdExists()
        {
            // Arrange
            mockEvaluationRepository.Setup(repo => repo.FindById(evaluation.Id)).Returns(evaluation);

            // Act & Assert
            mockEvaluationService.AddEvaluation(evaluation);
        }

        [TestMethod]
        public void UpdateEvaluation_ShouldUpdateExistingEvaluation()
        {
            // Arrange
            Evaluation existingEvaluation = evaluation;
            evaluation.Comments = "This is a test"; // update it and use it
            Evaluation updatedEvaluation = evaluation;
            mockEvaluationRepository.Setup(repo => repo.FindById(existingEvaluation.Id)).Returns(existingEvaluation);

            // Act
            mockEvaluationService.UpdateEvaluation(updatedEvaluation);

            // Assert
            mockEvaluationRepository.Verify(repo => repo.Update(existingEvaluation, updatedEvaluation), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void UpdateEvaluation_ShouldThrowKeyNotFoundException_WhenEvaluationNotFound()
        {
            // Act & Assert
            mockEvaluationService.UpdateEvaluation(evaluation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateEvaluation_ShouldThrowException_WhenStoreIdNotGiven()
        {
            // Arrange
            Evaluation updatedEvaluation = new Evaluation { Id = 1, Detid = 2, StartDate = DateTime.Now };
            // Act & Assert
            mockEvaluationService.UpdateEvaluation(updatedEvaluation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateEvaluation_ShouldThrowException_WhenDetIdNotGiven()
        {
            // Arrange
            Evaluation updatedEvaluation = new Evaluation { Id = 1, Storeid = 1, StartDate = DateTime.Now };
            // Act & Assert
            mockEvaluationService.UpdateEvaluation(updatedEvaluation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateEvaluation_ShouldThrowException_WhenStartDateNotGiven()
        {
            // Arrange
            Evaluation updatedEvaluation = new Evaluation { Id = 1, Storeid = 1, Detid = 2 };
            // Act & Assert
            mockEvaluationService.UpdateEvaluation(updatedEvaluation);
        }

        [TestMethod]
        public void DeleteEvaluation_ShouldDeleteEvaluation()
        {
            // Arrange
            int id = 1;
            mockEvaluationRepository.Setup(repo => repo.FindById(id)).Returns(evaluation);
            // Act
            mockEvaluationService.DeleteEvaluation(id);
            // Assert
            mockEvaluationRepository.Verify(repo => repo.Delete(evaluation), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void DeleteEvaluation_ShouldThrowKeyNotFoundException_WhenEvaluationNotFound()
        {
            // Arrange
            int id = 1;
            mockEvaluationRepository.Setup(repo => repo.FindById(id)).Returns((Evaluation)null);
            // Act & Assert
            mockEvaluationService.DeleteEvaluation(id);
        }

        [TestMethod]
        public void FindEvaluationById_ShouldReturnEvaluation_WhenEvaluationExists()
        {
            // Arrange
            int evaluationId = 1;
            mockEvaluationRepository.Setup(repo => repo.FindById(evaluationId)).Returns(evaluation);

            // Act
            var result = mockEvaluationService.FindEvaluationById(evaluationId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(evaluation, result);
        }

        [TestMethod]
        public void FindEvaluationById_ShouldReturnNull_WhenEvaluationDoesNotExist()
        {
            // Arrange
            int nonExistingEvaluationId = 999;

            mockEvaluationRepository.Setup(repo => repo.FindById(nonExistingEvaluationId)).Returns((Evaluation)null);

            // Act
            var result = mockEvaluationService.FindEvaluationById(nonExistingEvaluationId);

            // Assert
            Assert.IsNull(result);
        }
    }
}