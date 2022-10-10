using Cashier.Helpers;
using Cashier.Models.Articles;
using Cashier.Models.Countries;
using Entities;
using Entities.Articles;
using InfrastructureMongoDB;
using InfrastructureRedis;
using InfrastructureSql.Concrete;
using InfrastructureSql.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Data;

namespace Cashier.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class CountriesController : Controller
    {
        private readonly ILogger<CountriesController> _logger;
        private readonly IRepository<Country> _countryRepository;
        private IDistributedCache _cacheProvider;

        public string countriesCacheKey = "Countries";


        public CountriesController(
           ILogger<CountriesController> logger,
           IRepository<Country> countryRepository,
           IDistributedCache cacheProvider)
        {
            _logger = logger;
            _countryRepository = countryRepository;
            _cacheProvider = cacheProvider;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("CountriesController.Index");
            return View();
        }

        public async Task<IActionResult> GetAllCountries()
        {
            try
            {
                _logger.LogInformation("CountriesController.GetAllCountries");

                List<Country>? countries =
                    await RedisHelper.GetRecordAsync<List<Country>>(_cacheProvider, countriesCacheKey);

                if (countries == null)
                {
                    countries = await _countryRepository.GetAll();

                    await RedisHelper.SetRecordAsync(
                        _cacheProvider,
                        countriesCacheKey,
                        countries);
                }

                var countriesViewModelList = new List<CountryViewModel>();
                foreach (var country in countries)
                {
                    var countryViewModel = new CountryViewModel
                    {
                        Id = country.Id,
                        Name = country.Name,
                        IsoNumericCode = country.IsoNumericCode,
                        Alpha2Code = country.Alpha2Code,
                        Alpha3Code = country.Alpha3Code,
                        ContinentCode = country.ContinentCode
                    };
                    countriesViewModelList.Add(countryViewModel);
                }
                _logger.LogInformation($"CountriesController.GetAllCountries return: {countriesViewModelList}");
                return Json(countriesViewModelList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCountry([FromBody] CountryViewModel country)
        {
            try
            {
                _logger.LogInformation($"CountriesController.CreateNewArticle country: {country}");
                if (ModelState.IsValid)
                {
                    var newCountry = CountryMapper.ToCountryEntity(country, User.Identity.Name);
                    await _countryRepository.Add(newCountry);
                    await RedisHelper.RemoveRecordAsync(_cacheProvider, countriesCacheKey);

                    _logger.LogInformation($"CountriesController.CreateNewCountry country id={newCountry.Id} created successfuly");
                    return Json(new { success = true, description = "Country created successfully" });
                }
                _logger.LogInformation($"CountriesController.CreateNewCountry validation error");
                return Json(new { success = false, description = "Country not created" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"CountriesController.CreateNewCountry exception", ex);
                return Json(new { success = false, description = ex.Message });
            }
        }

    }
}
