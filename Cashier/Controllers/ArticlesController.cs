using Cashier.Data;
using Cashier.Helpers;
using Cashier.Models.Articles;
using DataAccess.Data;
using Entities.Articles;
using InfrastructureSql.Concrete;
using InfrastructureSql.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cashier.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class ArticlesController : Controller
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IRepository<Article> _articleRepository;
        
        public ArticlesController(
            ILogger<ArticlesController> logger,
            IRepository<Article> articleRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
        }
        
        public IActionResult Index()
        {
            _logger.LogInformation("ArticlesController.Index");
            return View();
        }

        public async Task<IActionResult> GetAllArticles()
        {
            _logger.LogInformation("ArticlesController.GetAllArticles");
            var articles = await _articleRepository.GetAll();
            var articlesViewModelList = new List<ArticleViewModel>();
            foreach (var article in articles)
            {
                var articleViewModel = new ArticleViewModel
                {
                    Id = article.Id,
                    Country = article.Country,
                    Description = article.Description,
                    IsOnSale = article.IsOnSale,
                    Manufacturer = article.Manufacturer,
                    Name = article.Name,
                    Price = article.Price,
                    Stock = article.Stock
                };
                articlesViewModelList.Add(articleViewModel);
            }
            _logger.LogInformation($"ArticlesController.GetAllArticles return: {articlesViewModelList}");
            return Json(articlesViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewArticle([FromBody] ArticleViewModel article)
        {
            try
            {
                _logger.LogInformation($"ArticlesController.CreateNewArticle article: {article}");
                if (ModelState.IsValid)
                {
                    var newArticle = ArticleMapper.ToArticleEntity(article, User.Identity.Name);

                    await _articleRepository.Add(newArticle);
                    _logger.LogInformation($"ArticlesController.CreateNewArticle article id={newArticle.Id} created successfuly");
                    return Json(new { success = true, description = "Article created successfully" });
                }
                _logger.LogInformation($"ArticlesController.CreateNewArticle validation error");
                return Json(new { success = false, description = "Article not created" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"ArticlesController.CreateNewArticle exception", ex);
                return Json(new { success = false, description = ex.Message });
            }
        }

        public async Task<IActionResult> GetArticle(long id)
        {
            try
            {
                _logger.LogInformation($"ArticlesController.GetArticle id={id}");
                var article = await _articleRepository.GetById(id);
                var articleViewModel = new ArticleViewModel();
                if (article != null)
                {
                    articleViewModel = ArticleMapper.ToArticleViewModel(article);
                }
                return View("Article", articleViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ArticlesController.GetArticle exception", ex);
                throw;
            }
        }

        public async Task<IActionResult> EditArticle(long id)
        {
            try
            {
                _logger.LogInformation($"ArticlesController.EditArticle id={id}");
                var article = await _articleRepository.GetById(id);
                var articleViewModel = new ArticleViewModel();
                if (article != null)
                {
                    articleViewModel = ArticleMapper.ToArticleViewModel(article);
                }
                return View("Article", articleViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ArticlesController.EditArticle exception", ex);
                throw;
            }
        }
    }
}
