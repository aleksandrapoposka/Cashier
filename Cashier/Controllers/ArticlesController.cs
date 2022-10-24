using Cashier.Helpers;
using Cashier.Loggers;
using Cashier.Models.Articles;
using Entities;
using Entities.Articles;
using InfrastructureSql.Interfaces;
using InfrastructureStorageAccount.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cashier.Controllers
{
     //test push 
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class ArticlesController : Controller
    {
        private readonly IMetricsClient _logger;
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IBlobRepository _blobRepository;
        private readonly ITableRepository _tableRepository;

        public ArticlesController(
            IMetricsClient logger,
            IRepository<Article> articleRepository,
            IRepository<Country> countryRepository,
            IBlobRepository blobRepository,
            ITableRepository tableRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
            _countryRepository = countryRepository;
            _blobRepository = blobRepository;
            _tableRepository = tableRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var result = await _countryRepository.GetAll();
            var countries = result.OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString(), Selected = false })
                .ToList();
            ViewBag.Countries = countries;

            _logger.LogEvent("ArticlesController.Index Request");
            return View();
        }

        public async Task<IActionResult> GetAllArticles()
        {
            _logger.LogEvent("ArticlesController.GetAllArticles Request");
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
                    ImgSrc = _tableRepository.GetImageUri(article.Id)
                };
                articlesViewModelList.Add(articleViewModel);
            }
            _logger.LogEvent($"ArticlesController.GetAllArticles Response: {articlesViewModelList}");
            return Json(articlesViewModelList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewArticle([FromBody] ArticleViewModel article)
        {
            try
            {
                _logger.LogEvent($"ArticlesController.CreateNewArticle Request: {article}");
                if (ModelState.IsValid)
                {
                    var newArticle = ArticleMapper.ToArticleEntity(article, User.Identity.Name);

                    await _articleRepository.Add(newArticle);
                    _logger.LogEvent($"ArticlesController.CreateNewArticle Response : Article Id={newArticle.Id} created successfuly");
                    return Json(new { success = true, description = "Article created successfully" });
                }
                _logger.LogEvent($"ArticlesController.CreateNewArticle Validation Error");
                return Json(new { success = false, description = "Article not created" });
            }
            catch (Exception ex)
            {
                _logger.LogException($"ArticlesController.CreateNewArticle", ex);
                return Json(new { success = false, description = ex.Message });
            }
        }

        public async Task<IActionResult> GetArticle(long id)
        {
            try
            {
                _logger.LogEvent($"ArticlesController.GetArticle Request : Id={id}");
                var article = await _articleRepository.GetById(id);                

                var articleViewModel = new ArticleViewModel();
                if (article != null)
                {
                    articleViewModel = ArticleMapper.ToArticleViewModel(article);
                    articleViewModel.ImgSrc = _tableRepository.GetImageUri(article.Id);
                }

                return View("Article", articleViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogException($"ArticlesController.GetArticle", ex);
                throw;
            }
        }

        public async Task<IActionResult> AddArticleImage(long id)
        {
            _logger.LogEvent($"ArticlesController.AddArticleImage Request : Id={id}");
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
                _logger.LogEvent($"ArticlesController.AddArticleImage Request : Id={articleId}");
                var article = await _articleRepository.GetById(articleId);
                if (article == null)
                {
                    _logger.LogEvent($"Article don't excist");
                }
                //validate file size
                if (articleImage.Length > 16 * 1024 * 1024)
                {
                    _logger.LogEvent($"Image size is too big");
                }
                var allowedExtension = new List<string>() { "png", "jpg", "jpeg", "gif" };
                var extension = Path.GetExtension(articleImage.FileName).Substring(1);

                if (!allowedExtension.Contains(extension))
                {
                    _logger.LogEvent($"Image extension is not supported");
                }

                string imagePath = await _blobRepository.UploadBlob(articleImage);

                if (string.IsNullOrEmpty(_tableRepository.GetImageUri(articleId)))
                {
                    _logger.LogEvent($"No duplicate images allowed");                  
                }

                await _tableRepository.InsertArticleImage(new ArticleImage
                {
                    ArticleId = articleId,
                    Image = imagePath
                });


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogException($"ArticlesController.AddArticleImage", ex);
                throw ex;
            }
        }

        public async Task<IActionResult> EditArticle(long id)
        {
            try
            {
                _logger.LogEvent($"ArticlesController.EditArticle Request : Id={id}");
                var articleEntity = await _articleRepository.GetById(id);
                if (articleEntity == null)
                {
                    _logger.LogEvent($"Article don't excist");
                }
                var articleViewModel = ArticleMapper.ToArticleViewModel(articleEntity);
                articleViewModel.ImgSrc = _tableRepository.GetImageUri(articleEntity.Id);
                return View(articleViewModel);
                
            }
            catch (Exception ex)
            {
                _logger.LogException($"ArticlesController.EditArticle Id={id}",ex);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditArticle(ArticleViewModel articleViewModel)
        {
            try
            {
                _logger.LogEvent($"ArticlesController.EditArticle Request : Id={articleViewModel.Id}");
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
                _logger.LogException($"ArticlesController.EditArticle", ex);
                throw ex;
            }
        }

    }
}
