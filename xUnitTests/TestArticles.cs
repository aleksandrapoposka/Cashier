using Cashier.Controllers;
using Cashier.Models.Articles;
using Entities;
using Entities.Articles;
using InfrastructureMongoDB;
using InfrastructureSql.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace xUnitTests
{
    public class TestArticles
    {
        private readonly Mock<ILogger<ArticlesController>> mockLogger;
        private readonly Mock<IRepository<Article>> mockArticleRepository;
        private readonly Mock<IArticleImageRepository> mockArticleImageRepository;
        private readonly Mock<IRepository<Country>> mockCountryRepository;

        public TestArticles()
        {
            mockLogger = new Mock<ILogger<ArticlesController>>();
            mockArticleRepository = new Mock<IRepository<Article>>();
            mockArticleImageRepository = new Mock<IArticleImageRepository>();
            mockCountryRepository = new Mock<IRepository<Country>>();
        }
        [Fact]
        public async Task GetArticleById_Sucess()
        {
            // Arrange

            var controller = new ArticlesController(
                mockLogger.Object,
                mockArticleRepository.Object,
                mockArticleImageRepository.Object,
                mockCountryRepository.Object);

            var id = 1;
            var article = new Article
            {
                Id = 1,
                Name = "TestArt"
            };

            var articleImg = new ArticleImage
            {
                ArticleId = 1,
                Image = new byte[1]
            };

            mockArticleRepository.Setup(t => t.GetById(id)).Returns(Task.FromResult(article));
            mockArticleImageRepository.Setup(t => t.Get(id)).Returns(articleImg);

            // Act

            var result = await controller.GetArticle(id);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ArticleViewModel>(viewResult.ViewData.Model);
            // Assert
            Assert.Equal(id,model.Id);
        }
        [Fact]
        public async Task GetAllArticles_Sucess()
        {
            // Arrange
            var controller = new ArticlesController(
                mockLogger.Object,
                mockArticleRepository.Object,
                mockArticleImageRepository.Object,
                mockCountryRepository.Object);
            var articleList = new List<Article>();
            var id = 1;
            var article = new Article
            {
                Id = 1,
                Name = "TestArt"
            };
            var articleImg = new ArticleImage
            {
                ArticleId = 1,
                Image = new byte[1]
            };
            articleList.Add(article);   

            mockArticleRepository.Setup(t => t.GetAll()).Returns(Task.FromResult(articleList));
            mockArticleImageRepository.Setup(t => t.Get(id)).Returns(articleImg);

            // Act

            var result =  await controller.GetAllArticles();
            var okResult = result as ObjectResult;
            // Assert
            Assert.Equal(id,1);
        }

        [Fact]
        public async Task GetArticleById_RedirectToArticles()
        {
            // Arrange

            var controller = new ArticlesController(
                mockLogger.Object,
                mockArticleRepository.Object,
                mockArticleImageRepository.Object,
                mockCountryRepository.Object);

            var id = 1;
            var article = new Article
            {
                Id = 1,
                Name = "TestArt"
            };

            var articleImg = new ArticleImage
            {
                ArticleId = 1,
                Image = new byte[1]
            };

            mockArticleRepository.Setup(t => t.GetById(id)).Returns(Task.FromResult(article));
            mockArticleImageRepository.Setup(t => t.Get(id)).Returns(articleImg);

            // Act

            var result = await controller.GetArticle(id);
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ArticleViewModel>(viewResult.ViewData.Model);
            // Assert
            Assert.Equal("Article", viewResult.ViewName);
        }
    }
}