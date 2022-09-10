using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Vidly.Domain.Entities;
using Vidly.Domain.SearchCriterias;
using Vidly.Exceptions;
using Vidly.IBusinessLogic;
using Vidly.Models.In;
using Vidly.Models.Out;
using Vidly.WebApi.Controllers;

namespace Vidly.WebApi.Test;

[TestClass]
public class MovieControllerTests
{
    private MovieController _movieController; 
    private Mock<IMovieManager> _movieManagerMock;

    [TestInitialize]
    public void SetUp()
    {
        _movieManagerMock = new Mock<IMovieManager>(MockBehavior.Strict);
        _movieController = new MovieController(_movieManagerMock.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _movieManagerMock.VerifyAll();
    }

    // Arrange: Creamos los mocks y se lo pasamos a los objetos que queremos testear
    // Act: Probamos/ejecutamos el metodo
    // Assert: Comprobamos que el resultado es el que esperamos

    // Dummy: No tiene comportamiento, ej: Pasar por parametro algo que no vamos a usar
    // Fake: Objetos que funcionan de verdad pero con algun shortcut que los hace no viables en prod. Ej BD en memoria 
    // Stubs: Devuelven algo por default, ya estan pre programados y no se verifica comportamiento
    // Spies: Como el stub pero tambien registra informacion extra
    // Mocks: Objetos que programamos nosotros para el test y es el unico que verifica comportamiento
    [TestMethod]
    public void GetAllMovies_NoSearchCriteria_ReturnsAllMovies()
    {
        //Arrange
        var dummyMovies = GenerateDummyMovies();
        var dummyMovieModels = dummyMovies.Select(m => new MovieBasicModel(m)).ToList();
        var dummySearchCriteria = new MovieSearchCriteriaModel();
        _movieManagerMock.Setup(m => m.GetAllMovies(It.IsAny<MovieSearchCriteria>())).Returns(dummyMovies);

        //Act
        var response = _movieController.GetMovies(dummySearchCriteria);

        //Assert
        var okResponse = response as OkObjectResult;
        var retrievedMovies = okResponse.Value as IEnumerable<MovieBasicModel>;
        
        CollectionAssert.AreEqual(dummyMovieModels, retrievedMovies.ToList());
    }
    
    [TestMethod]
    public void GetMovieById_MovieNotExists_ReturnsNotFoundResponse()
    {
        //Arrange
        const int InexistentMovieId = 4;
        const string MovieNotFoundMessage = "Movie not found, sorry :)"; 
        _movieManagerMock
            .Setup(m => m.GetSpecificMovie(It.IsAny<int>()))
            .Throws(new ResourceNotFoundException(MovieNotFoundMessage));
        
        //Act
        var response = _movieController.GetMovie(InexistentMovieId);

        //Assert
        var notFoundResponse = response as ObjectResult;

        Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResponse.StatusCode);
        Assert.AreEqual(MovieNotFoundMessage, notFoundResponse.Value);
    }

    private List<Movie> GenerateDummyMovies() => new List<Movie>()
    {
        new Movie() { Id = 1, Title = "El conjuro", Description = "De terror" },
        new Movie() { Id = 2, Title = "Los minions", Description = "De comedia" }
    };

   
}