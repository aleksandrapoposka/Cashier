using Cashier.Helpers;
using Cashier.Models.Articles;
using DataAccess.Data;
using Entities;
using Entities.Articles;
using InfrastructureMongoDB;
using InfrastructureSql.Concrete;
using InfrastructureSql.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Cashier.Controllers
{
     //test push 
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class ArticlesController : Controller
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IRepository<Article> _articleRepository;
        private readonly IArticleImageRepository _articleImageRepository;
        private readonly IRepository<Country> _countryRepository;

        public ArticlesController(
            ILogger<ArticlesController> logger,
            IRepository<Article> articleRepository,
            IArticleImageRepository articleImageRepository,
            IRepository<Country> countryRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
            _articleImageRepository = articleImageRepository;
            _countryRepository = countryRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _countryRepository.GetAll();
            var countries = result.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = false })
                .ToList();
            ViewBag.Countries = countries;

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
                    Stock = article.Stock,
                    ImgSrc = "data:image;base64," + Convert.ToBase64String(_articleImageRepository.Get(article.Id).Image)
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
                var articleImg = _articleImageRepository.Get(id);

                var articleViewModel = new ArticleViewModel();
                if (article != null)
                {
                    articleViewModel = ArticleMapper.ToArticleViewModel(article);

                    if (articleImg != null)
                    { 
                        articleViewModel.ImgSrc = Convert.ToBase64String(articleImg.Image); 
                    }


                }

                return View("Article", articleViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ArticlesController.GetArticle exception", ex);
                throw;
            }
        }

        public async Task<IActionResult> AddArticleImage(long id)
        {
            _logger.LogInformation($"ArticlesController.AddArticleImage id={id}");
            var article = await _articleRepository.GetById(id);
            var articleViewModel = new ArticleViewModel();
            if (article != null)
            {
                articleViewModel = ArticleMapper.ToArticleViewModel(article);
            }
            return View(articleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticleImage(
            long articleId,
            IFormFile articleImage)
        {
            try
            {
                _logger.LogInformation($"ArticlesController.AddArticleImage id={articleId}");
                var article = await _articleRepository.GetById(articleId);
                if (article == null)
                {
                    //return error
                }
                //validate file size
                if (articleImage.Length > 16 * 1024 * 1024)
                {
                    //return error for file size
                }
                var allowedExtension = new List<string>() { "png", "jpg", "jpeg", "gif" };
                var extension = Path.GetExtension(articleImage.FileName).Substring(1);
                if (!allowedExtension.Contains(extension))
                {
                    //return extension error here
                }

                var articleImg = _articleImageRepository.Get(articleId);
                if (articleImg != null)
                {
                    //return no duplicate images allowed
                }
                Stream stream = articleImage.OpenReadStream();
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    var articleImageArray = memoryStream.ToArray();
                    _articleImageRepository.Create(new ArticleImage
                    {
                        ArticleId = articleId,
                        Image = articleImageArray,
                        ModifiedBy = User.Identity.Name,
                        ModifiedOn = DateTime.UtcNow
                    });
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"ArticlesController.AddArticleImage exception", ex);
                throw ex;
            }
        }

        public async Task<IActionResult> EditArticle(long id)
        {
            try
            {
                _logger.LogInformation($"ArticlesController.EditArticle id={id}");
                var articleEntity = await _articleRepository.GetById(id);
                if (articleEntity == null)
                {
                    //return error
                }
                var articleViewModel = ArticleMapper.ToArticleViewModel(articleEntity);
                articleViewModel.ImgSrc = "data:image;base64," + Convert.ToBase64String(_articleImageRepository.Get(id).Image);
                return View(articleViewModel);
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"ArticlesController.EditArticle id={id}");
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditArticle(ArticleViewModel articleViewModel)
        {
            try
            {
                _logger.LogInformation($"ArticlesController.EditArticle id={articleViewModel.Id}");
                if (!ModelState.IsValid)
                {
                    return View(articleViewModel);
                }
                
                var articleEntity = ArticleMapper.ToArticleEntity(articleViewModel, User.Identity.Name);
                await _articleRepository.Update(articleEntity);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"ArticlesController.EditArticle exception", ex);
                throw ex;
            }
        }

    }
}
