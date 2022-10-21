using Cashier.Helpers;
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
        private readonly ILogger<ArticlesController> _logger;
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IBlobRepository _blobRepository;
        private readonly ITableRepository _tableRepository;

        public ArticlesController(
            ILogger<ArticlesController> logger,
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
                    ImgSrc = _tableRepository.GetImageUri(article.Id)
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
                    articleViewModel.ImgSrc = _tableRepository.GetImageUri(article.Id);
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

                string imagePath = await _blobRepository.UploadBlob(articleImage);

                if (string.IsNullOrEmpty(_tableRepository.GetImageUri(articleId)))
                {
                   // return no duplicate images allowed
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
                articleViewModel.ImgSrc = _tableRepository.GetImageUri(articleEntity.Id);
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
